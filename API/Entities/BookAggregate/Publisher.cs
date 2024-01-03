using API.Entities.BaseEntities;

namespace API.Entities.BookAggregate;

public class Publisher : BaseEntityWithName
{
    public ICollection<Book> Books { get; set; }
}