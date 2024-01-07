using API.Entities.ProductAggregate;

namespace API.Entities.BaseEntities;

public class BaseProduct : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
}