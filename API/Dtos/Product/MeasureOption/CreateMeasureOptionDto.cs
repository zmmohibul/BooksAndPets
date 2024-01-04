using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product.MeasureOption;

public class CreateMeasureOptionDto
{
    [Required]
    public string Option { get; set; }
    [Required] 
    public string Description { get; set; }
}