using API.Dtos.BookDtoAggregate.BookDtos;
using API.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
public class BooksController : BaseApiController
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        return HandleResult(await _bookRepository.GetAllBookDtos());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        return HandleResult(await _bookRepository.GetBookDtoById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookDto createBookDto)
    {
        var result = await _bookRepository.CreateBook(createBookDto);
        
        return result.StatusCode == 201
            ? CreatedAtAction(nameof(GetBookById), new { id = result.Data.Id }, result.Data)
            : HandleResult(result);
    }
}