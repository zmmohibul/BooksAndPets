using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class Product : BaseEntityWithNameAndDescription
{
    public ICollection<Category> Categories { get; set; }
    public ICollection<Picture> Pictures { get; set; }
    public ICollection<Price> PriceList { get; set; }
}