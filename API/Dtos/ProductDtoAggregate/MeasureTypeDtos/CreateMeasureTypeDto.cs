using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.MeasureTypeDtos;

public class CreateMeasureTypeDto
{
    [Required] 
    public string Type { get; set; }
    
    [Required] 
    public string Description { get; set; }
}