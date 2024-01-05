
using API.Dtos.Product.Department;
using API.Entities.BookAggregate;
using API.Entities.ProductAggregate;
using API.Interfaces.RepositoryInterfaces.Book;
using API.Interfaces.RepositoryInterfaces.Product;
using API.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Product;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly DataContext _context;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public DepartmentRepository(DataContext context, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _context = context;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<ICollection<DepartmentDto>>> GetAllDepartmentDtos()
    {
        var departments = await _context.ProductDepartments
            .AsNoTracking()
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return new Result<ICollection<DepartmentDto>>(200, departments);
    }

    public async Task<Result<DepartmentDto>> GetDepartmentDtoById(int id)
    {
        var department = await _context.ProductDepartments
            .AsNoTracking()
            .Where(department => department.Id == id)
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return department == null 
            ? new Result<DepartmentDto>(404, "No Department with given id exist.") 
            : new Result<DepartmentDto>(200, department);
    }

    public async Task<Result<DepartmentDto>> CreateDepartment(CreateDepartmentDto createDepartmentDto)
    {
        createDepartmentDto.Department = createDepartmentDto.Department.ToLower();
        if (await _context.ProductDepartments.AnyAsync(pd => pd.Name.Equals(createDepartmentDto.Department)))
        {
            return new Result<DepartmentDto>(400, "A department with given name already exists.");
        }
        
        var department = _mapper.Map<Department>(createDepartmentDto);
        _context.ProductDepartments.Add(department);

        return await _context.SaveChangesAsync() > 0
            ? new Result<DepartmentDto>(201, _mapper.Map<DepartmentDto>(department))
            : new Result<DepartmentDto>(400, "Failed to create department. Try again later.");
    }

    public async Task<Result<DepartmentDto>> UpdateDepartment(int id, UpdateDepartmentDto updateDepartmentDto)
    {
        var department = await _context.ProductDepartments
            .FirstOrDefaultAsync(department => department.Id == id);
        if (department == null)
        {
            return new Result<DepartmentDto>(404, "No Department with given id found.");
        }
        
        if (!string.IsNullOrEmpty(updateDepartmentDto.Description))
        {
            department.Description = updateDepartmentDto.Description;
        }

        await _context.SaveChangesAsync();

        return new Result<DepartmentDto>(200, _mapper.Map<DepartmentDto>(department));
    }

    public async Task<Result<bool>> DeleteDepartment(int id)
    {
        var department = await _context.ProductDepartments
            .Include(department => department.Categories)
            .Where(department => department.Categories.Any(cat => cat.Parent == null))
            .FirstOrDefaultAsync(department => department.Id == id);
        if (department == null)
        {
            return new Result<bool>(404, "No Department with given id exist.");
        }

        foreach (var category in department.Categories)
        {
            _categoryRepository.RemoveAllSubCategories(category);
            _context.Remove(category);
        }

        _context.ProductDepartments.Remove(department);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete Department. Try again later.");
    }
}