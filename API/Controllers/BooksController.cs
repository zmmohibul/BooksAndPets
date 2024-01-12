using API.Dtos.BookDtoAggregate.BookDtos;
using API.Interfaces.RepositoryInterfaces;
using API.Utils.QueryParameters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
public class BooksController : BaseApiController
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks([FromQuery] BookQueryParameters bookQueryParameters)
    {
        return HandleResult(await _bookRepository.GetAllBookDtos(bookQueryParameters));
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