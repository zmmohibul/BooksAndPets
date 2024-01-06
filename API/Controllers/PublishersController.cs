using API.Dtos.BookDtoAggregate.PublisherDtos;
using API.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("books/[controller]")]
public class PublishersController : BaseApiController
{
    private readonly IPublisherRepository _publisherRepository;

    public PublishersController(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPublishers()
    {
        return HandleResult(await _publisherRepository.GetAllPublisherDtos());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublisherById(int id)
    {
        return HandleResult(await _publisherRepository.GetPublisherDtoById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePublisher(CreatePublisherDto createPublisherDto)
    {
        var result = await _publisherRepository.CreatePublisher(createPublisherDto);
        return result.StatusCode == 201
            ? CreatedAtAction(nameof(GetPublisherById), new { id = result.Data.Id }, result.Data)
            : HandleResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto)
    {
        return HandleResult(await _publisherRepository.UpdatePublisher(id, updatePublisherDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        return HandleResult(await _publisherRepository.DeletePublisher(id));
    }
}