using API.Entities.Identity;

namespace API.Entities.OrderAggregate;

public class Order
{
    public int Id { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    
    public DateTime OrderDate { get; set; } = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
    public OrderStatus OrderStatus { get; set; } = OrderStatus.OrderReceived;

    public ICollection<OrderItem> OrderItems { get; set; }
}