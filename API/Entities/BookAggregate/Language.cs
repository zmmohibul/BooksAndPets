using API.Entities.BaseEntities;

namespace API.Entities.BookAggregate;

public class Language : BaseEntityWithName
{
    public ICollection<Book> Books { get; set; }
}