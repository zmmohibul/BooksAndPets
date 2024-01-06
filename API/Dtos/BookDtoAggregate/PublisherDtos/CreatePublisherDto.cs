using System.ComponentModel.DataAnnotations;

namespace API.Dtos.BookDtoAggregate.PublisherDtos;

public class CreatePublisherDto
{
    [Required]
    public string Name { get; set; }
}