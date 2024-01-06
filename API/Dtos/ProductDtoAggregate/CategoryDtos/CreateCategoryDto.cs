using System.ComponentModel.DataAnnotations;

namespace API.Dtos.ProductDtoAggregate.CategoryDtos;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }
    public int? ParentId { get; set; }
}