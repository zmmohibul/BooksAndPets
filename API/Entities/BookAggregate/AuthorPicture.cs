using API.Entities.BaseEntities;

namespace API.Entities.BookAggregate;

public class AuthorPicture : BaseEntity
{
    public string Url { get; set; }
    public string PublicId { get; set; }
}