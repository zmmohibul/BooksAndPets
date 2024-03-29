using API.Dtos.ProductDtoAggregate.MeasureTypeDtos;
using API.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/products/[controller]")]
public class MeasureTypesController : BaseApiController
{
    private readonly IMeasureTypeRepository _measureTypeRepository;

    public MeasureTypesController(IMeasureTypeRepository measureTypeRepository)
    {
        _measureTypeRepository = measureTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMeasureTypes()
    {
        return HandleResult(await _measureTypeRepository.GetAllMeasureTypeDtos());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMeasureTypeById(int id)
    {
        return HandleResult(await _measureTypeRepository.GetMeasureTypeDtoById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeasureType(CreateMeasureTypeDto createMeasureTypeDto)
    {
        var result = await _measureTypeRepository.CreateMeasureType(createMeasureTypeDto);
        
        return result.StatusCode == 201 
            ? CreatedAtAction(nameof(GetMeasureTypeById), new { id = result.Data.Id }, result.Data) 
            : HandleResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMeasureType(int id, UpdateMeasureTypeDto updateMeasureTypeDto)
    {
        return HandleResult(await _measureTypeRepository.UpdateMeasureType(id, updateMeasureTypeDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMeasureType(int id)
    {
        return HandleResult(await _measureTypeRepository.DeleteMeasureType(id));
    }
}