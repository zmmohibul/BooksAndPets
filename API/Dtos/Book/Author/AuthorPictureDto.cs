namespace API.Dtos.Book.Author;

public class AuthorPictureDto
{
    public AuthorPictureDto(string pictureUrl)
    {
        PictureUrl = pictureUrl;
    }

    public string PictureUrl { get; set; }
}