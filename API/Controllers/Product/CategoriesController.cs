using API.Dtos.Product.Category;
using API.Interfaces.RepositoryInterfaces.Product;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Product;

[Route("products/[controller]")]
public class CategoriesController : BaseApiController
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        return HandleResult(await _categoryRepository.GetAllCategories());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        return HandleResult(await _categoryRepository.GetCategoryById(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        var result = await _categoryRepository.CreateCategory(createCategoryDto);
        
        return result.StatusCode == 201 
            ? CreatedAtAction(nameof(GetCategoryById), new { id = result.Data.Id }, result.Data) 
            : HandleResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        return HandleResult(await _categoryRepository.UpdateCategory(id, updateCategoryDto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        return HandleResult(await _categoryRepository.DeleteCategory(id));
    }
}