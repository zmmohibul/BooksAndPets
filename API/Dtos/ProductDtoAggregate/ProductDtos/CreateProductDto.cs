using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.ProductDtos;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }

    [Required]
    public int DepartmentId { get; set; }
    
    [Required]
    public ICollection<int> CategoryIds { get; set; }
    
    [Required]
    public ICollection<CreatePriceDto> PriceList { get; set; }
}