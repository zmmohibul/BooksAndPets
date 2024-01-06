namespace API.Dtos.BookDtoAggregate.AuthorDtos;

public class AuthorPictureDto
{
    public AuthorPictureDto(string pictureUrl)
    {
        PictureUrl = pictureUrl;
    }

    public string PictureUrl { get; set; }
}