using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class Category : BaseEntityWithName
{
    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<Category> Children { get; set; }
    
    public ICollection<Product> Products { get; set; }

    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}