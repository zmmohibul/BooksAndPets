using API.Dtos.Product.Department;
using API.Interfaces.RepositoryInterfaces.Product;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

[Route("products/[controller]")]
public class DepartmentsController : BaseApiController
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentsController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments()
    {
        return HandleResult(await _departmentRepository.GetAllDepartmentDtos());
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        return HandleResult(await _departmentRepository.GetDepartmentDtoById(id));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(CreateDepartmentDto createDepartmentDto)
    {
        var result = await _departmentRepository.CreateDepartment(createDepartmentDto);
        return result.StatusCode == 201
            ? CreatedAtAction(nameof(GetDepartmentById), new { id = result.Data.Id }, result.Data)
            : HandleResult(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
    {
        return HandleResult(await _departmentRepository.UpdateDepartment(id, updateDepartmentDto));
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartment(int id)
    {
        return HandleResult(await _departmentRepository.DeleteDepartment(id));
    }
}