using API.Dtos.Product.Department;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Product;

public interface IDepartmentRepository
{
    public Task<Result<ICollection<DepartmentDto>>> GetAllDepartmentDtos();
    public Task<Result<DepartmentDto>> GetDepartmentDtoById(int id);
    public Task<Result<DepartmentDto>> CreateDepartment(CreateDepartmentDto createDepartmentDto);
    public Task<Result<DepartmentDto>> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto);
    public Task<Result<bool>> DeleteDepartment(int id);
}