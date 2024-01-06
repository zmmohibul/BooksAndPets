using API.Dtos.ProductDtoAggregate.CategoryDtos;
using API.Interfaces.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("products/departments/{departmentId}/[controller]")]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories(int departmentId)
    {
        return HandleResult(await _categoryRepository.GetAllCategories(departmentId));
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(int departmentId, int categoryId)
    {
        return HandleResult(await _categoryRepository.GetCategoryById(departmentId, categoryId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(int departmentId, [FromBody] CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryRepository.CreateCategory(departmentId, createCategoryDto);
        
        return result.StatusCode == 201 
            ? CreatedAtAction(nameof(GetCategoryById), new { departmentId = departmentId, categoryId = result.Data.Id }, result.Data) 
            : HandleResult(result);
    }
    
    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategory(int departmentId, int categoryId, UpdateCategoryDto updateCategoryDto)
    {
        return HandleResult(await _categoryRepository.UpdateCategory(departmentId, categoryId, updateCategoryDto));
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> DeleteCategory(int departmentId, int categoryId)
    {
        return HandleResult(await _categoryRepository.DeleteCategory(departmentId, categoryId));
    }
}