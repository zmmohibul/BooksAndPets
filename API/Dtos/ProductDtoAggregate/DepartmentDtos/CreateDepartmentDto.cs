using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.DepartmentDtos;

public class CreateDepartmentDto
{
    [Required]
    public string Department { get; set; }
    
    [Required]
    public string Description { get; set; }
}