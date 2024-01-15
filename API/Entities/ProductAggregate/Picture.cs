using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class Picture : BaseEntity
{
    public string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}