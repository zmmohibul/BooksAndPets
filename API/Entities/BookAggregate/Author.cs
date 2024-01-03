using API.Entities.BaseEntities;
namespace API.Entities.BookAggregate;

public class Author : BaseEntityWithNameAndDescription
{
    public ICollection<Book> Books { get; set; }
}