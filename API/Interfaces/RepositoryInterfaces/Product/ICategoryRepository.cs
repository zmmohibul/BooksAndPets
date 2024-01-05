using API.Dtos.Product.Category;
using API.Entities.ProductAggregate;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Product;

public interface ICategoryRepository
{
    public Task<Result<ICollection<CategoryDetailsDto>>> GetAllCategories(int departmentId);
    public Task<Result<CategoryDetailsWithSubCategoryDto>> GetCategoryById(int departmentId, int categoryId);
    public Task<Result<CategoryDetailsDto>> CreateCategory(int departmentId, CreateCategoryDto createCategoryDto);
    public Task<Result<CategoryDetailsDto>> UpdateCategory(int departmentId, int categoryId, UpdateCategoryDto updateCategoryDto);
    public Task<Result<bool>> DeleteCategory(int departmentId, int categoryId);
    public void RemoveAllSubCategories(Category category);
}