using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Book.Publisher;

public class CreatePublisherDto
{
    [Required]
    public string Name { get; set; }
}