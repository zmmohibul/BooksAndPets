using System.ComponentModel.DataAnnotations;

namespace API.Dtos.BookDtoAggregate.AuthorDtos;

public class CreateAuthorDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
}