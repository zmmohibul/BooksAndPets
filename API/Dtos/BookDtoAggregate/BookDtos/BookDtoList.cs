using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Utils;

namespace API.Dtos.BookDtoAggregate.BookDtos;

public class BookDtoList
{
    public BookDtoList(PaginatedList<BookDto> books, ICollection<AuthorDto> authors, ICollection<PublisherDto> publishers)
    {
        Data = books;
        Authors = authors;
        Publishers = publishers;
    }

    public PaginatedList<BookDto> Data { get; set; }
    public ICollection<AuthorDto> Authors { get; set; }
    public ICollection<PublisherDto> Publishers { get; set; }
}