using API.Dtos.OrderDtoAggregate;
using API.Entities.Identity;
using API.Entities.OrderAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using API.Utils.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public OrderRepository(DataContext context, UserManager<User> userManager,  IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<OrderDto>>> GetAllOrdersForUser(string username, QueryParameter queryParameters)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName.Equals(username));
        if (user == null)
        {
            return new Result<PaginatedList<OrderDto>>(400, "Invalid username.");
        }

        var query = _context.Orders
            .AsNoTracking()
            .Where(o => o.UserId.Equals(user.Id))
            .Include(o => o.OrderItems)
            .OrderByDescending(o => o.Id)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

        var data = await PaginatedList<OrderDto>
            .CreatePaginatedListAsync(query, queryParameters.PageNumber, queryParameters.PageSize);

        return new Result<PaginatedList<OrderDto>>(200, data);
    }

    public async Task<Result<OrderDto>> CreateOrder(string username, CreateOrderDto createOrderDto)
    {
        var user = await _context.Users
            .Include(usr => usr.Addresses)
            .SingleOrDefaultAsync(user => user.UserName.Equals(username));
        if (user == null)
        {
            return new Result<OrderDto>(400, "Invalid username.");
        }

        if (createOrderDto.OrderItems.Count == 0)
        {
            return new Result<OrderDto>(400, "There are no items in cart.");
        }

        var addedItems = new Dictionary<string, OrderItem>();
        var orderItems = new List<OrderItem>();
        foreach (var item in createOrderDto.OrderItems)
        {
            var product = await _context.Products
                .Include(p => p.PriceList)
                .Include(p => p.Pictures)
                .FirstOrDefaultAsync(p => p.Id == item.ProductId);
            if (product == null)
            {
                return new Result<OrderDto>(400, $"Product id:{item.ProductId} does not exist.");
            }

            var measureType = await _context.ProductMeasureTypes.FirstOrDefaultAsync(mt => mt.Id == item.MeasureTypeId);
            if (measureType == null)
            {
                return new Result<OrderDto>(400, $"MeasureType id:{item.MeasureTypeId} does not exist.");
            }
            
            var measureOption = await _context.ProductMeasureOptions.FirstOrDefaultAsync(mo => mo.Id == item.MeasureOptionId);
            if (measureOption == null)
            {
                return new Result<OrderDto>(400, $"MeasureOption id:{item.MeasureTypeId} does not exist.");
            }

            if (measureOption.MeasureTypeId != measureType.Id)
            {
                return new Result<OrderDto>(400, $"MeasureOption id:{item.MeasureTypeId} does not belong to MeasureType id:{measureType.Id}.");
            }

            var price = product.PriceList.FirstOrDefault(p => p.MeasureOptionId == measureOption.Id);
            if (price.QuantityInStock == 0)
            {
                continue;
            }
            
            var quantityToAdd = price.QuantityInStock < item.Quantity ? price.QuantityInStock : item.Quantity;
            price.QuantityInStock -= quantityToAdd;
            
            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Price = price.UnitPrice,
                Quantity = quantityToAdd,
                MeasureType = measureType.Type,
                MeasureOption = measureOption.Option
            };

            var itemStr = $"{product.Name}{measureType.Type}{measureOption.Option}";
            if (addedItems.ContainsKey(itemStr))
            {
                addedItems[itemStr].Quantity += quantityToAdd;
            }
            else
            {
                addedItems[itemStr] = orderItem;
                orderItems.Add(orderItem);
            }
        }

        if (orderItems.Count == 0)
        {
            return new Result<OrderDto>(400, "Items were out of stock");
        }

        var address = user.Addresses.FirstOrDefault(ua => ua.Id == createOrderDto.AddressId);
        if (address == null)
        {
            return new Result<OrderDto>(400, "Invalid address id");
        }

        address.IsMain = true;

        var order = new Order
        {
            User = user,
            Address = address,
            OrderItems = orderItems,
            OrderStatus = OrderStatus.Received
        };

        _context.Orders.Add(order);
        return await _context.SaveChangesAsync() > 0
            ? new Result<OrderDto>(201, _mapper.Map<OrderDto>(order))
            : new Result<OrderDto>(400, "Failed to place order. Try again later.");
    }

    public async Task<Result<bool>> CancelOrder(string username, int orderId)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(user => user.UserName.Equals(username));

        if (user == null)
        {
            return new Result<bool>(401, "Please log in to continue.");
        }

        var order = await _context.Orders
            .Include(o => o.User)
            .FirstOrDefaultAsync(o => o.Id == orderId);
        if (order == null)
        {
            return new Result<bool>(400, "Order not found");
        }

        
        var roles = await _userManager.GetRolesAsync(user);
        if (user.Id != order.UserId && !roles.Contains(UserRole.Admin.ToString()))
        {
            return new Result<bool>(403, "This is not your order!");
        }

        if (order.OrderStatus == OrderStatus.Shipped)
        {
            return new Result<bool>(400, "You cannot cancel an order once it's shipped.");
        }

        if (order.OrderStatus == OrderStatus.Delivered)
        {
            return new Result<bool>(400, "Order is already delivered.");
        }

        order.OrderStatus = OrderStatus.Cancelled;
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(200, "Order has been cancelled")
            : new Result<bool>(400, "Failed to cancel order. Try again later");
    }   
}