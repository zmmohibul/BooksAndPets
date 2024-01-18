using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.ProductAggregate;

namespace API.Entities.OrderAggregate;

public class OrderItem
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string MeasureType { get; set; }
    public string MeasureOption { get; set; }
}