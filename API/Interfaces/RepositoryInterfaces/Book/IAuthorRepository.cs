using API.Dtos.Book.Author;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces.Book;

public interface IAuthorRepository
{
    public Task<Result<PaginatedList<AuthorDto>>> GetAllAuthorDtos(QueryParameter queryParameter);
    public Task<Result<AuthorDto>> GetAuthorDtoById(int authorId);
    
    public Task<Result<AuthorDto>> CreateAuthor(CreateAuthorDto createAuthorDto);
    public Task<Result<AuthorPictureDto>> CreateAuthorPicture(int authorId, IFormFile pictureFile);

    public Task<Result<AuthorDto>> UpdateAuthor(int authorId, UpdateAuthorDto updateAuthorDto);
    public Task<Result<AuthorPictureDto>> UpdateAuthorPicture(int authorId, IFormFile pictureFile);

    public Task<Result<bool>> DeleteAuthor(int authorId);
    public Task<Result<bool>> DeleteAuthorPicture(int authorId);

}