using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Entities.BookAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public PublisherRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ICollection<PublisherDto>>> GetAllPublisherDtos()
    {
        var publishers = await _context.Publishers
            .AsNoTracking()
            .OrderBy(pub => pub.Name)
            .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new Result<ICollection<PublisherDto>>(200, publishers);
    }

    public async Task<Result<PublisherDto>> GetPublisherDtoById(int id)
    {
        var publisher = await _context.Publishers
            .AsNoTracking()
            .Where(publisher => publisher.Id == id)
            .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return publisher == null 
            ? new Result<PublisherDto>(404, "No Publisher with given id exist.") 
            : new Result<PublisherDto>(200, publisher);
    }

    public async Task<Result<PublisherDto>> CreatePublisher(CreatePublisherDto createPublisherDto)
    {
        createPublisherDto.Name = createPublisherDto.Name.ToLower();
        
        var publisher = _mapper.Map<Publisher>(createPublisherDto);
        _context.Publishers.Add(publisher);

        return await _context.SaveChangesAsync() > 0
            ? new Result<PublisherDto>(201, _mapper.Map<PublisherDto>(publisher))
            : new Result<PublisherDto>(400, "Failed to create publisher. Try again later.");
    }

    public async Task<Result<PublisherDto>> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == id);
        if (publisher == null)
        {
            return new Result<PublisherDto>(404, "No Publisher with given id found.");
        }

        if (!string.IsNullOrEmpty(updatePublisherDto.Name))
        {
            publisher.Name = updatePublisherDto.Name.ToLower();
        }

        await _context.SaveChangesAsync();

        return new Result<PublisherDto>(200, _mapper.Map<PublisherDto>(publisher));
    }

    public async Task<Result<bool>> DeletePublisher(int id)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(publisher => publisher.Id == id);
        if (publisher == null)
        {
            return new Result<bool>(404, "No Publisher with given id exist.");
        }

        _context.Publishers.Remove(publisher);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete Publisher. Try again later.");
    }
}