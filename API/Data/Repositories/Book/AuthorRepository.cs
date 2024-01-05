using API.Dtos.Book.Author;
using API.Entities.BookAggregate;
using API.Interfaces;
using API.Interfaces.RepositoryInterfaces.Book;
using API.Utils;
using API.Utils.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace API.Data.Repositories.Book;

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _context;
    private readonly IPictureUploadService _pictureUploadService;
    private readonly IMapper _mapper;

    public AuthorRepository(DataContext context, IPictureUploadService pictureUploadService, IMapper mapper)
    {
        _context = context;
        _pictureUploadService = pictureUploadService;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<AuthorDto>>> GetAllAuthorDtos(QueryParameter queryParameter)
    {
        var query = _context.Authors
            .AsNoTracking()
            .Include(author => author.AuthorPicture)
            .OrderBy(author => author.Name)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider);

        return new Result<PaginatedList<AuthorDto>>(200, await PaginatedList<AuthorDto>.CreatePaginatedListAsync(query, queryParameter.PageNumber, queryParameter.PageSize));
    }

    public async Task<Result<AuthorDto>> GetAuthorDtoById(int authorId)
    {
        var author = await _context.Authors
            .AsNoTracking()
            .Where(author => author.Id == authorId)
            .Include(author => author.AuthorPicture)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return author != null
            ? new Result<AuthorDto>(200, author)
            : new Result<AuthorDto>(404, "No author with given id found.");
    }

    public async Task<Result<AuthorDto>> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var author = new Author
        {
            Name = createAuthorDto.Name.ToLower(),
            Bio = createAuthorDto.Bio,
        };

        _context.Authors.Add(author);
        return await _context.SaveChangesAsync() > 0
            ? new Result<AuthorDto>(200, new AuthorDto(author.Id, author.Name, author.Bio, author.AuthorPicture?.Url))
            : new Result<AuthorDto>(400, "Failed to create author.");
    }

    public async Task<Result<AuthorPictureDto>> CreateAuthorPicture(int authorId, IFormFile pictureFile)
    {
        var author = await _context.Authors
            .Include(author => author.AuthorPicture)
            .FirstOrDefaultAsync(author => author.Id == authorId);
        if (author == null)
        {
            return new Result<AuthorPictureDto>(404, "No author with given id found.");
        }

        if (author.AuthorPicture != null)
        {
            return new Result<AuthorPictureDto>(400, "Picture already exist for this author. Try updating/deleting.");
        }
        
        var pictureUploadResult = await _pictureUploadService.AddPictureAsync(pictureFile);
        if (pictureUploadResult.Error != null)
        {
            return new Result<AuthorPictureDto>(400, "Failed to upload author picture");
        }

        var authorPicture = new AuthorPicture
        {
            Url = pictureUploadResult.SecureUrl.AbsoluteUri,
            PublicId = pictureUploadResult.PublicId
        };

        author.AuthorPicture = authorPicture;
        return await _context.SaveChangesAsync() > 0
            ? new Result<AuthorPictureDto>(201, new AuthorPictureDto(authorPicture.Url))
            : new Result<AuthorPictureDto>(400, "Failed to create author picture");
    }

    public async Task<Result<AuthorDto>> UpdateAuthor(int authorId, UpdateAuthorDto updateAuthorDto)
    {
        var author = await _context.Authors
            .Include(author => author.AuthorPicture)
            .FirstOrDefaultAsync(author => author.Id == authorId);

        if (author == null)
        {
            return new Result<AuthorDto>(404, "No author with given id found.");
        }

        if (!string.IsNullOrEmpty(updateAuthorDto.Name))
        {
            author.Name = updateAuthorDto.Name.ToLower();
        }
        
        if (!string.IsNullOrEmpty(updateAuthorDto.Bio))
        {
            author.Bio = updateAuthorDto.Bio;
        }

        await _context.SaveChangesAsync();

        return new Result<AuthorDto>(200, new AuthorDto(author.Id, author.Name, author.Bio, author.AuthorPicture?.Url));
    }

    public async Task<Result<AuthorPictureDto>> UpdateAuthorPicture(int authorId, IFormFile pictureFile)
    {
        var author = await _context.Authors
            .Include(author => author.AuthorPicture)
            .FirstOrDefaultAsync(author => author.Id == authorId);
        
        var picDeletionResult = await _pictureUploadService.DeletePhotoAsync(author.AuthorPicture.PublicId);
        if (picDeletionResult.Error != null)
        {
            return new Result<AuthorPictureDto>(400, "Failed to upload author picture. Try again later.");
        }
            
        var pictureUploadResult = await _pictureUploadService.AddPictureAsync(pictureFile);
        if (pictureUploadResult.Error != null)
        {
            return new Result<AuthorPictureDto>(400, "Failed to update author picture");
        }

        author.AuthorPicture = new AuthorPicture
        {
            Url = pictureUploadResult.SecureUrl.AbsoluteUri,
            PublicId = pictureUploadResult.PublicId
        };
        
        return await _context.SaveChangesAsync() > 0
            ? new Result<AuthorPictureDto>(200, new AuthorPictureDto(author.AuthorPicture.Url))
            : new Result<AuthorPictureDto>(400, "Failed to update author picture");
    }

    public async Task<Result<bool>> DeleteAuthor(int authorId)
    {
        var author = await _context.Authors
            .Include(author => author.AuthorPicture)
            .FirstOrDefaultAsync(author => author.Id == authorId);
        
        if (author == null)
        {
            return new Result<bool>(404, "No author with given id found.");
        }

        if (!string.IsNullOrEmpty(author.AuthorPicture?.PublicId))
        {
            var picDeletionResult = await _pictureUploadService.DeletePhotoAsync(author.AuthorPicture.PublicId);
            if (picDeletionResult.Error != null)
            {
                return new Result<bool>(400, "Failed to delete author picture. Try again later.");
            }
        }

        _context.Authors.Remove(author);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete author. Try again later");
    }
    
    public async Task<Result<bool>> DeleteAuthorPicture(int authorId)
    {
        var author = await _context.Authors
            .Include(author => author.AuthorPicture)
            .FirstOrDefaultAsync(author => author.Id == authorId);
        
        if (author == null)
        {
            return new Result<bool>(404, "No author with given id found.");
        }

        if (!string.IsNullOrEmpty(author.AuthorPicture?.PublicId))
        {
            var picDeletionResult = await _pictureUploadService.DeletePhotoAsync(author.AuthorPicture.PublicId);
            if (picDeletionResult.Error != null)
            {
                return new Result<bool>(400, "Failed to delete author picture. Try again later.");
            }

            author.AuthorPicture = null;
        }
        
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete author picture. Try again later");
    }
}