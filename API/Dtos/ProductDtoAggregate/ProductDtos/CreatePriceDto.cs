using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.ProductDtos;

public class CreatePriceDto
{
    [Required]
    public decimal UnitPrice { get; set; }
    
    [Required]
    public int MeasureTypeId { get; set; }
    
    [Required]
    public int MeasureOptionId { get; set; }
    
    [Required]
    public int QuantityInStock { get; set; }
}