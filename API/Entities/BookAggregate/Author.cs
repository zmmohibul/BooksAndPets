using API.Entities.BaseEntities;
namespace API.Entities.BookAggregate;

public class Author : BaseEntityWithName
{
    public string Bio { get; set; }
    
    public int? AuthorPictureId { get; set; }
    public AuthorPicture? AuthorPicture { get; set; }
    
    public ICollection<Book> Books { get; set; }
}