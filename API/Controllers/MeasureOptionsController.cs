using API.Dtos.Product.MeasureOption;
using API.Interfaces.RepositoryInterfaces.Product;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("products/measureTypes/{measureTypeId}/[controller]")]
public class MeasureOptionsController : BaseApiController
{
    private readonly IMeasureOptionRepository _measureOptionRepository;

    public MeasureOptionsController(IMeasureOptionRepository measureOptionRepository)
    {
        _measureOptionRepository = measureOptionRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMeasureOptions(int measureTypeId)
    {
        return HandleResult(await _measureOptionRepository.GetAllMeasureOptionDtos(measureTypeId));
    }
    
    [HttpGet("{measureOptionId}")]
    public async Task<IActionResult> GetMeasureOptionById(int measureTypeId, int measureOptionId)
    {
        return HandleResult(await _measureOptionRepository.GetMeasureOptionDtoById(measureTypeId, measureOptionId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeasureOption(int measureTypeId, CreateMeasureOptionDto createMeasureOptionDto)
    {
        var result = await _measureOptionRepository.CreateMeasureOption(measureTypeId, createMeasureOptionDto);
        
        return result.StatusCode == 201 
            ? CreatedAtAction(nameof(GetMeasureOptionById), new { measureTypeId = measureTypeId, measureOptionId = result.Data.Id }, result.Data) 
            : HandleResult(result);
    }
    
    [HttpPut("{measureOptionId}")]
    public async Task<IActionResult> UpdateMeasureOption(int measureTypeId, int measureOptionId, UpdateMeasureOptionDto updateMeasureOptionDto)
    {
        var result = await _measureOptionRepository.UpdateMeasureOption(measureTypeId, measureOptionId, updateMeasureOptionDto);
        
        return HandleResult(result);
    }
    
    [HttpDelete("{measureOptionId}")]
    public async Task<IActionResult> DeleteMeasureOption(int measureTypeId, int measureOptionId)
    {
        var result = await _measureOptionRepository.DeleteMeasureOption(measureTypeId, measureOptionId);
        
        return HandleResult(result);
    }
}