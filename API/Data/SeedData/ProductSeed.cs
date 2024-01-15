using API.Entities.ProductAggregate;

namespace API.Data.SeedData;

public class ProductSeed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DepartmentId { get; set; }
    public List<int> CategoryIds { get; set; } = new ();
    public List<Price> PriceList { get; set; } = new ();
    public List<Picture> Pictures { get; set; } = new ();
}