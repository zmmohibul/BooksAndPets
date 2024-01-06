using API.Dtos.BookDtoAggregate.BookDtos;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces;

public interface IBookRepository
{
    public Task<Result<ICollection<BookDto>>> GetAllBookDtos();
    public Task<Result<BookDto>> GetBookDtoById(int id);
    public Task<Result<BookDto>> CreateBook(CreateBookDto createBookDto);
    public Task<Result<BookDto>> UpdateBook(int id, UpdateBookDto updateBookDto);
    public Task<Result<bool>> DeleteBook(int id);
}