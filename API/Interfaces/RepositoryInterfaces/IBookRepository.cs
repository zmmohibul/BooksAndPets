using API.Dtos.BookDtoAggregate.BookDtos;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IBookRepository
{
    public Task<Result<BookDtoList>> GetAllBookDtos(BookQueryParameters bookQueryParameters);
    public Task<Result<BookDto>> GetBookDtoById(int id);
    public Task<Result<BookDto>> CreateBook(CreateBookDto createBookDto);
    public Task<Result<BookDto>> UpdateBook(int id, CreateBookDto createBookDto);
    public Task<Result<bool>> DeleteBook(int id);
}