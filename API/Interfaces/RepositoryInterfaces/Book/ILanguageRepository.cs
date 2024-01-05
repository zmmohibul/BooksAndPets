using API.Dtos.Book.Language;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Book;

public interface ILanguageRepository
{
    public Task<Result<ICollection<LanguageDto>>> GetAllLanguageDtos();
    public Task<Result<LanguageDto>> GetLanguageDtoById(int id);
    public Task<Result<LanguageDto>> CreateLanguage(CreateLanguageDto createLanguageDto);
    public Task<Result<LanguageDto>> UpdateLanguage(int id, UpdateLanguageDto updateLanguageDto);
    public Task<Result<bool>> DeleteLanguage(int id);
}