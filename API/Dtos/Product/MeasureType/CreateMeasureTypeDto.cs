using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product.MeasureType;

public class CreateMeasureTypeDto
{
    [Required] 
    public string Type { get; set; }
}