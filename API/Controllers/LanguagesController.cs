using API.Dtos.BookDtoAggregate.LanguageDtos;
using API.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("books/[controller]")]
public class LanguagesController : BaseApiController
{
    private readonly ILanguageRepository _languageRepository;

    public LanguagesController(ILanguageRepository languageRepository)
    {
        _languageRepository = languageRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllLanguages()
    {
        return HandleResult(await _languageRepository.GetAllLanguageDtos());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLanguageById(int id)
    {
        return HandleResult(await _languageRepository.GetLanguageDtoById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateLanguage(CreateLanguageDto createLanguageDto)
    {
        var result = await _languageRepository.CreateLanguage(createLanguageDto);
        return result.StatusCode == 201
            ? CreatedAtAction(nameof(GetLanguageById), new { id = result.Data.Id }, result.Data)
            : HandleResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLanguage(int id, UpdateLanguageDto updateLanguageDto)
    {
        return HandleResult(await _languageRepository.UpdateLanguage(id, updateLanguageDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLanguage(int id)
    {
        return HandleResult(await _languageRepository.DeleteLanguage(id));
    }
}