using API.Dtos.Product.Category;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Product;

public interface ICategoryRepository
{
    public Task<Result<ICollection<CategoryDetailsDto>>> GetAllCategories();
    public Task<Result<CategoryDetailsWithSubCategoryDto>> GetCategoryById(int id);
    public Task<Result<CategoryDetailsDto>> CreateCategory(CreateCategoryDto createCategoryDto);
    public Task<Result<CategoryDetailsDto>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);
    public Task<Result<bool>> DeleteCategory(int id);
}