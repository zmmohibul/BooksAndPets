using API.Dtos.Book.Publisher;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Book;

public interface IPublisherRepository
{
    public Task<Result<ICollection<PublisherDto>>> GetAllPublisherDtos();
    public Task<Result<PublisherDto>> GetPublisherDtoById(int id);
    public Task<Result<PublisherDto>> CreatePublisher(CreatePublisherDto createPublisherDto);
    public Task<Result<PublisherDto>> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto);
    public Task<Result<bool>> DeletePublisher(int id);
}