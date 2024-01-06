using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Entities.BookAggregate;
using API.Interfaces.RepositoryInterfaces;
using API.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories;

public class LanguageRepository : ILanguageRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public LanguageRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<ICollection<LanguageDto>>> GetAllLanguageDtos()
    {
        var languages = await _context.Languages
            .AsNoTracking()
            .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new Result<ICollection<LanguageDto>>(200, languages);
    }

    public async Task<Result<LanguageDto>> GetLanguageDtoById(int id)
    {
        var language = await _context.Languages
            .AsNoTracking()
            .Where(language => language.Id == id)
            .ProjectTo<LanguageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return language == null 
            ? new Result<LanguageDto>(404, "No Language with given id exist.") 
            : new Result<LanguageDto>(200, language);
    }

    public async Task<Result<LanguageDto>> CreateLanguage(CreateLanguageDto createLanguageDto)
    {
        createLanguageDto.Language = createLanguageDto.Language.ToLower();
        
        var language = _mapper.Map<Language>(createLanguageDto);
        _context.Languages.Add(language);

        return await _context.SaveChangesAsync() > 0
            ? new Result<LanguageDto>(201, _mapper.Map<LanguageDto>(language))
            : new Result<LanguageDto>(400, "Failed to create language. Try again later.");
    }

    public async Task<Result<LanguageDto>> UpdateLanguage(int id, UpdateLanguageDto updateLanguageDto)
    {
        var language = await _context.Languages
            .FirstOrDefaultAsync(language => language.Id == id);
        if (language == null)
        {
            return new Result<LanguageDto>(404, "No Language with given id found.");
        }

        if (!string.IsNullOrEmpty(updateLanguageDto.Language))
        {
            language.Name = updateLanguageDto.Language.ToLower();
        }

        await _context.SaveChangesAsync();

        return new Result<LanguageDto>(200, _mapper.Map<LanguageDto>(language));
    }

    public async Task<Result<bool>> DeleteLanguage(int id)
    {
        var language = await _context.Languages
            .FirstOrDefaultAsync(language => language.Id == id);
        if (language == null)
        {
            return new Result<bool>(404, "No Language with given id exist.");
        }

        _context.Languages.Remove(language);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete Language. Try again later.");
    }
}