using API.Dtos.OrderDtoAggregate;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IOrderRepository
{
    public Task<Result<PaginatedList<OrderDto>>> GetAllOrdersForUser(string username, QueryParameter queryParameters);
    public Task<Result<OrderDto>> CreateOrder(string username, CreateOrderDto createOrderDto);
    public Task<Result<bool>> CancelOrder(string username, int orderId);
}