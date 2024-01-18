using API.Entities.BaseEntities;
using API.Entities.OrderAggregate;

namespace API.Entities.ProductAggregate;

public class Product : BaseEntityWithNameAndDescription
{
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Picture> Pictures { get; set; }
    public ICollection<Price> PriceList { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}