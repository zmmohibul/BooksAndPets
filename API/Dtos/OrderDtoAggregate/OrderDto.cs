using API.Entities.OrderAggregate;

namespace API.Dtos.OrderDtoAggregate;

public class OrderDto
{
    public int Id { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public int AddressId { get; set; }
}