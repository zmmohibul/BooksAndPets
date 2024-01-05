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

    public async Task<Result<ICollection<CategoryDetailsDto>>> GetAllCategories(int departmentId)
    {
        var department = await _context.ProductDepartments.FirstOrDefaultAsync(d => d.Id == departmentId);
        if (department == null)
        {
            return new Result<ICollection<CategoryDetailsDto>>(404, "No department with given id exists.");
        }
        
        var categories = await _context.ProductCategories
            .AsNoTracking()
            .Where(pc => pc.ParentId == null && pc.DepartmentId == departmentId)
            .OrderBy(pc => pc.Name)
            .Select(pc => new CategoryDetailsDto(pc.Id, pc.Name))
            .ToListAsync();

        return new Result<ICollection<CategoryDetailsDto>>(200, categories);
    }

    public async Task<Result<CategoryDetailsWithSubCategoryDto>> GetCategoryById(int departmentId, int categoryId)
    {
        var category = await _context.ProductCategories
            .AsNoTracking()
            .Where(pc => pc.Id == categoryId && pc.DepartmentId == departmentId)
            .Include(pc => pc.Department)
            .Include(pc => pc.Children)
            .Select(pc => new CategoryDetailsWithSubCategoryDto(
                new CategoryDetailsDto(pc.Id, pc.Name),
                pc.Children.OrderBy(pc => pc.Name).Select(child => new CategoryDetailsDto(child.Id, child.Name))
            ))
            .FirstOrDefaultAsync();
        
        return category == null 
            ? new Result<CategoryDetailsWithSubCategoryDto>(404, $"Invalid department or category id.") 
            : new Result<CategoryDetailsWithSubCategoryDto>(200, category);
    }

    public async Task<Result<CategoryDetailsDto>> CreateCategory(int departmentId, CreateCategoryDto createCategoryDto)
    {
        var department = await _context.ProductDepartments.Include(pd => pd.Categories).FirstOrDefaultAsync(pd => pd.Id == departmentId);
        if (department == null)
        {
            return new Result<CategoryDetailsDto>(404, "No department with given id exists.");
        }
        
        createCategoryDto.Name = createCategoryDto.Name.ToLower();
        if (await _context.ProductCategories.AnyAsync(pc => pc.Name.Equals(createCategoryDto.Name)))
        {
            return new Result<CategoryDetailsDto>(400, "A category with given name already exist");
        }
        
        var category = new Category
        {
            Name = createCategoryDto.Name.ToLower(),
            Department = department
        };
        department.Categories.Add(category);

        if (createCategoryDto.ParentId != null)
        {
            var parentCategory =
                await _context.ProductCategories.SingleOrDefaultAsync(pc => pc.Id == createCategoryDto.ParentId);
            if (parentCategory == null)
            {
                return new Result<CategoryDetailsDto>(404, "Parent category with given id does not exist.");
            }

            if (!department.Categories.Any(dc => dc.Id == parentCategory.Id))
            {
                return new Result<CategoryDetailsDto>(400,
                    $"parentId:{parentCategory.Id} does not belong to department id:{departmentId}.");
            }
            category.Parent = parentCategory;
        }

        _context.ProductCategories.Add(category);
        
        return await _context.SaveChangesAsync() > 0 
            ? new Result<CategoryDetailsDto>(201, new CategoryDetailsDto(category.Id, category.Name)) 
            : new Result<CategoryDetailsDto>(400, "Failed to create category");
    }

    public async Task<Result<CategoryDetailsDto>> UpdateCategory(int departmentId, int categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _context.ProductCategories
            .Include(pc => pc.Department)
            .SingleOrDefaultAsync(pc => pc.Id == categoryId);
        if (category == null)
        {
            return new Result<CategoryDetailsDto>(404, "No category with given id exists");
        }

        if (category.Department.Id != departmentId)
        {
            return new Result<CategoryDetailsDto>(400,
                $"Category id: {categoryId} does not belong to department id: {departmentId}");
        }

        category.Name = updateCategoryDto.Name.ToLower();
        await _context.SaveChangesAsync();

        return new Result<CategoryDetailsDto>(200, new CategoryDetailsDto(category.Id, category.Name));
    }

    public async Task<Result<bool>> DeleteCategory(int departmentId, int categoryId)
    {
        var category = await _context.ProductCategories
            .Include(pc => pc.Children)
            .Include(pc => pc.Department)
            .SingleOrDefaultAsync(pc => pc.Id == categoryId);
        
        if (category == null)
        {
            return new Result<bool>(404, "No category with given id exists");
        }
        
        if (category.Department.Id != departmentId)
        {
            return new Result<bool>(400,
                $"Category id: {categoryId} does not belong to department id: {departmentId}");
        }
        
        RemoveAllSubCategories(category);
        _context.Remove(category);

        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204, "Category deleted")
            : new Result<bool>(400, "Failed to delete category");
    }

    public async void RemoveAllSubCategories(Category category)
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