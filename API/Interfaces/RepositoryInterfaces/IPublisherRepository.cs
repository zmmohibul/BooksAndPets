using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces;

public interface IPublisherRepository
{
    public Task<Result<ICollection<PublisherDto>>> GetAllPublisherDtos();
    public Task<Result<PublisherDto>> GetPublisherDtoById(int id);
    public Task<Result<PublisherDto>> CreatePublisher(CreatePublisherDto createPublisherDto);
    public Task<Result<PublisherDto>> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto);
    public Task<Result<bool>> DeletePublisher(int id);
}