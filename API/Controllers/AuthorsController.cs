using API.Dtos.Book.Author;
using API.Interfaces.RepositoryInterfaces.Book;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("books/[controller]")]
public class AuthorsController : BaseApiController
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorsController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        return HandleResult(await _authorRepository.GetAllAuthorDtos());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        return HandleResult(await _authorRepository.GetAuthorDtoById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
    {
        var result = await _authorRepository.CreateAuthor(createAuthorDto);

        return result.StatusCode == 201
            ? CreatedAtAction(nameof(GetAuthorById), new { id = result.Data.Id }, result.Data)
            : HandleResult(result);
    }
    
    [HttpPost("{authorId}/authorPicture")]
    public async Task<IActionResult> CreateAuthorPicture(int authorId, IFormFile file)
    {
        var result = await _authorRepository.CreateAuthorPicture(authorId, file);

        return result.StatusCode == 201
            ? Created($"{result.Data.PictureUrl}", result.Data)
            : HandleResult(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorDto updateAuthorDto)
    {
        return HandleResult(await _authorRepository.UpdateAuthor(id, updateAuthorDto));
    }
    
    [HttpPut("{authorId}/authorPicture")]
    public async Task<IActionResult> UpdateAuthorPicture(int authorId, IFormFile file)
    {
        return HandleResult(await _authorRepository.UpdateAuthorPicture(authorId, file));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        return HandleResult(await _authorRepository.DeleteAuthor(id));
    }
    
    [HttpDelete("{authorId}/authorPicture")]
    public async Task<IActionResult> DeleteAuthorPicture(int authorId)
    {
        return HandleResult(await _authorRepository.DeleteAuthorPicture(authorId));
    }
}