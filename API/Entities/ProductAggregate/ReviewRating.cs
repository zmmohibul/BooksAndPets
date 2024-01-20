using API.Entities.BaseEntities;
using API.Entities.Identity;
using API.Entities.OrderAggregate;

namespace API.Entities.ProductAggregate;

public class ReviewRating : BaseEntity
{
    public int Rating { get; set; }
    public string Review { get; set; }
    public DateTime Date { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    
    public string UserId { get; set; }
    public User User { get; set; }
    
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }
}