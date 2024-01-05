using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Book.Author;

public class CreateAuthorDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
}