namespace API.Dtos.BookDtoAggregate.AuthorDtos;

public class AuthorDto
{
    public AuthorDto() { }

    public AuthorDto(int id, string name, string bio, string? pictureUrl)
    {
        Id = id;
        Name = name;
        Bio = bio;
        if (string.IsNullOrEmpty(pictureUrl))
        {
            pictureUrl = string.Empty;
        }
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string? PictureUrl { get; set; } = String.Empty;
}