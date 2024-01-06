namespace API.Dtos.BookDtoAggregate.AuthorDtos;

public class UpdateAuthorDto
{
    public string? Name { get; set; }
    public string? Bio { get; set; }
    public IFormFile? PictureFile { get; set; }
}