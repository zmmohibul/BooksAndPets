using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.BaseEntities;

namespace API.Entities.ProductAggregate;

public class Price : BaseEntity
{
    // { "unitPrice": 720, "measureType": "book", "measureOption": "Paperback", "quantityInStock": 22 }
    // { "unitPrice": 1600, "measureType": "weight", "measureOption": "3 Kg",  "quantityInStock": 13 }
    // { "unitPrice": 1780, "measureType": "size", "measureOption": "L",  "quantityInStock": 9 }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }
    
    public int MeasureTypeId { get; set; }
    public MeasureType MeasureType { get; set; }

    public int MeasureOptionId { get; set; }
    public MeasureOption MeasureOption { get; set; }
    
    public int QuantityInStock { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }
}