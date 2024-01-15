using API.Dtos.BookDtoAggregate.AuthorDtos;
using API.Utils;
using API.Utils.QueryParameters;

namespace API.Interfaces.RepositoryInterfaces;

public interface IAuthorRepository
{
    public Task<Result<PaginatedList<AuthorDto>>> GetAllAuthorDtos(QueryParameter queryParameter);
    public Task<Result<AuthorDetailsDto>> GetAuthorDtoById(int authorId);
    
    public Task<Result<AuthorDetailsDto>> CreateAuthor(CreateAuthorDto createAuthorDto);
    public Task<Result<AuthorPictureDto>> CreateAuthorPicture(int authorId, IFormFile pictureFile);

    public Task<Result<AuthorDetailsDto>> UpdateAuthor(int authorId, UpdateAuthorDto updateAuthorDto);
    public Task<Result<AuthorPictureDto>> UpdateAuthorPicture(int authorId, IFormFile pictureFile);

    public Task<Result<bool>> DeleteAuthor(int authorId);
    public Task<Result<bool>> DeleteAuthorPicture(int authorId);

}