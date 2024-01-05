using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class Department : BaseEntityWithNameAndDescription
{
    public ICollection<Category> Categories { get; set; }
}