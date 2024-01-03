using API.Dtos.Product.MeasureOption;
using API.Dtos.Product.MeasureType;
using API.Entities.ProductAggregate;
using API.Interfaces.RepositoryInterfaces.Product;
using API.Utils;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Product;

public class MeasureTypeRepository : IMeasureTypeRepository
{
    private readonly DataContext _context;

    public MeasureTypeRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Result<ICollection<MeasureTypeDto>>> GetAllMeasureTypes()
    {
        var measureTypes = await GetMeasureTypeQueryable().ToListAsync();

        return new Result<ICollection<MeasureTypeDto>>(200, measureTypes);
    }

    public async Task<Result<MeasureTypeDto>> GetMeasureTypeById(int id)
    {
        var measureType = await GetMeasureTypeQueryable(id).FirstOrDefaultAsync();

        return measureType == null 
            ? new Result<MeasureTypeDto>(404, "No Measure Type found with the given id.") 
            : new Result<MeasureTypeDto>(200, measureType);
    }

    public async Task<Result<MeasureTypeDto>> CreateMeasureType(CreateMeasureTypeDto createMeasureTypeDto)
    {
        var measureType = new MeasureType
        {
            Type = createMeasureTypeDto.Type
        };
        _context.ProductMeasureTypes.Add(measureType);

        return await _context.SaveChangesAsync() > 0
            ? new Result<MeasureTypeDto>(200, new MeasureTypeDto(measureType.Id, measureType.Type))
            : new Result<MeasureTypeDto>(400, "Failed to create Measure Type");
    }

    public async Task<Result<MeasureTypeDto>> UpdateMeasureType(int id, UpdateMeasureTypeDto updateMeasureTypeDto)
    {
        var measureType = await GetMeasureTypeQueryable(id).FirstOrDefaultAsync();
        if (measureType == null)
        {
            return new Result<MeasureTypeDto>(404, "No Measure Type found with the given id.");
        }
        
        return await _context.SaveChangesAsync() > 0
            ? new Result<MeasureTypeDto>(201, measureType)
            : new Result<MeasureTypeDto>(400, "Failed to create Measure Type");
    }

    public async Task<Result<bool>> DeleteMeasureType(int id)
    {
        var measureType = await _context.ProductMeasureTypes.FirstOrDefaultAsync(pmt => pmt.Id == id);
        if (measureType == null)
        {
            return new Result<bool>(404, "No Measure Type found with the given id");
        }

        _context.ProductMeasureTypes.Remove(measureType);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete Measure Type");
    }

    private IQueryable<MeasureTypeDto> GetMeasureTypeQueryable(int? id = null)
    {
        var query = _context.ProductMeasureTypes.AsQueryable();
        if (id != null)
        {
            query = query.Where(q => q.Id == id);
        }
        
        return query.Include(pmt => pmt.MeasureOptions)
            .Select(pmt => new MeasureTypeDto(
                pmt.Id,
                pmt.Type,
                pmt.MeasureOptions.Select(mo => new MeasureOptionDto(mo.Id, mo.Option))
            ));
    }
}