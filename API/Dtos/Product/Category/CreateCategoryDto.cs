using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Product.Category;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }
    public int? ParentId { get; set; }
}