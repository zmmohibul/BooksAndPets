using API.Dtos.Product.Category;
using API.Entities.ProductAggregate;
using API.Interfaces;
using API.Interfaces.RepositoryInterfaces.Product;
using API.Utils;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Product;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;

    public CategoryRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<ICollection<CategoryDetailsDto>>> GetAllCategories()
    {
        var categories = await _context.ProductCategories
            .Where(pc => pc.ParentId == null)
            .OrderBy(pc => pc.Name)
            .Select(pc => new CategoryDetailsDto(pc.Id, pc.Name))
            .ToListAsync();

        return new Result<ICollection<CategoryDetailsDto>>(200, categories);
    }

    public async Task<Result<CategoryDetailsWithSubCategoryDto>> GetCategoryById(int id)
    {
        var category = await _context.ProductCategories
            .Where(pc => pc.Id == id)
            .Include(pc => pc.Children)
            .Select(pc => new CategoryDetailsWithSubCategoryDto(
                new CategoryDetailsDto(pc.Id, pc.Name),
                pc.Children.OrderBy(pc => pc.Name).Select(child => new CategoryDetailsDto(child.Id, child.Name))
            ))
            .FirstOrDefaultAsync();

        return category == null 
            ? new Result<CategoryDetailsWithSubCategoryDto>(404, "No category with given id exist") 
            : new Result<CategoryDetailsWithSubCategoryDto>(200, category);
    }

    public async Task<Result<CategoryDetailsDto>> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        createCategoryDto.Name = createCategoryDto.Name.ToLower();
        if (await _context.ProductCategories.AnyAsync(pc => pc.Name.Equals(createCategoryDto.Name)))
        {
            return new Result<CategoryDetailsDto>(400, "A category with given name already exist");
        }
        
        var category = new Category
        {
            Name = createCategoryDto.Name
        };

        if (createCategoryDto.ParentId != null)
        {
            var parentCategory =
                await _context.ProductCategories.SingleOrDefaultAsync(pc => pc.Id == createCategoryDto.ParentId);
            if (parentCategory == null)
            {
                return new Result<CategoryDetailsDto>(404, "Parent category with given parentId not found.");
            }
            
            category.Parent = parentCategory;
        }

        _context.ProductCategories.Add(category);
        
        return await _context.SaveChangesAsync() > 0 
            ? new Result<CategoryDetailsDto>(201, new CategoryDetailsDto(category.Id, category.Name)) 
            : new Result<CategoryDetailsDto>(400, "Failed to create category");
    }

    public async Task<Result<CategoryDetailsDto>> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _context.ProductCategories.SingleOrDefaultAsync(pc => pc.Id == id);
        if (category == null)
        {
            return new Result<CategoryDetailsDto>(404, "No category with given id exists");
        }

        category.Name = updateCategoryDto.Name;
        await _context.SaveChangesAsync();

        return new Result<CategoryDetailsDto>(200, new CategoryDetailsDto(category.Id, category.Name));
    }

    public async Task<Result<bool>> DeleteCategory(int id)
    {
        var category = await _context.ProductCategories
            .Include(pc => pc.Children)
            .SingleOrDefaultAsync(pc => pc.Id == id);
        
        if (category == null)
        {
            return new Result<bool>(404, "No category with given id exists");
        }
        
        RemoveAllSubCategories(category);
        _context.Remove(category);

        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204, "Category deleted")
            : new Result<bool>(400, "Failed to delete category");
    }

    private async void RemoveAllSubCategories(Category category)
    {
        foreach (var categoryChild in category.Children)
        {
            var categoryChildDetail =
                await _context.ProductCategories
                    .Include(pc => pc.Children)
                    .SingleOrDefaultAsync(pc => pc.Id == categoryChild.Id);
            RemoveAllSubCategories(categoryChildDetail);
            
            _context.Remove(categoryChild);
        }
    }
}