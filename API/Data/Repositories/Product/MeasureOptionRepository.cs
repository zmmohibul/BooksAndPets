using API.Dtos.Product.MeasureOption;
using API.Dtos.Product.MeasureType;
using API.Entities.ProductAggregate;
using API.Interfaces.RepositoryInterfaces.Product;
using API.Utils;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.Product;

public class MeasureOptionRepository : IMeasureOptionRepository
{
    private readonly DataContext _context;
    private readonly IMeasureTypeRepository _measureTypeRepository;

    public MeasureOptionRepository(DataContext context, IMeasureTypeRepository measureTypeRepository)
    {
        _context = context;
        _measureTypeRepository = measureTypeRepository;
    }

    public async Task<Result<ICollection<MeasureOptionDto>>> GetAllMeasureOptionDtos(int measureTypeId)
    {
        var measureType = (await _measureTypeRepository.GetMeasureTypeDtoById(measureTypeId)).Data;
        return measureType == null 
            ? new Result<ICollection<MeasureOptionDto>>(404, "No Measure Type with given measureTypeId found.") 
            : new Result<ICollection<MeasureOptionDto>>(200, measureType.MeasureOptions.ToList());
    }

    public async Task<Result<MeasureOptionDto>> GetMeasureOptionDtoById(int measureTypeId, int measureOptionId)
    {
        var measureType = (await _measureTypeRepository.GetMeasureTypeDtoById(measureTypeId)).Data;
        if (measureType == null)
        {
            return new Result<MeasureOptionDto>(404, "No Measure Type with given measureTypeId found.");
        }

        var measureOption = measureType.MeasureOptions.FirstOrDefault(mo => mo.Id == measureOptionId);

        return measureOption == null
            ? new Result<MeasureOptionDto>(404, "No Measure Option with given id exist.")
            : new Result<MeasureOptionDto>(200, measureOption);
    }

    public async Task<Result<MeasureOptionDto>> CreateMeasureOption(int measureTypeId, CreateMeasureOptionDto createMeasureOptionDto)
    {
        createMeasureOptionDto.Option = createMeasureOptionDto.Option.ToLower();
        createMeasureOptionDto.Description = createMeasureOptionDto.Description.ToLower();
        
        if (await _context.ProductMeasureOptions.AnyAsync(pmo => pmo.Option.Equals(createMeasureOptionDto.Option)))
        {
            return new Result<MeasureOptionDto>(400, "Measure Option with given Option already exist.");
        }

        var measureType = await _context.ProductMeasureTypes.FirstOrDefaultAsync(pmt => pmt.Id == measureTypeId);
        if (measureType == null)
        {
            return new Result<MeasureOptionDto>(404, "No Measure Type with given measureTypeId found.");
        }
        
        var measureOption = new MeasureOption
        {
            Option = createMeasureOptionDto.Option,
            Description = createMeasureOptionDto.Description,
            MeasureType = measureType
        };
        
        measureType.MeasureOptions.Add(measureOption);

        return await _context.SaveChangesAsync() > 0
            ? new Result<MeasureOptionDto>(201, new MeasureOptionDto(measureOption.Id, measureOption.Option, measureOption.Description))
            : new Result<MeasureOptionDto>(400, $"Failed to create Measure Option {createMeasureOptionDto.Option}");
    }

    public async Task<Result<MeasureOptionDto>> UpdateMeasureOption(int measureTypeId, int measureOptionId, UpdateMeasureOptionDto updateMeasureOptionDto)
    {
        var measureOption = await GetMeasureOptionQueryable(measureTypeId, measureOptionId).FirstOrDefaultAsync();
        if (measureOption == null)
        {
            return new Result<MeasureOptionDto>(404, "No Measure Option with given id found.");
        }

        if (!string.IsNullOrEmpty(updateMeasureOptionDto.Option))
        {
            measureOption.Option = updateMeasureOptionDto.Option.ToLower();
        }
        
        if (!string.IsNullOrEmpty(updateMeasureOptionDto.Description))
        {
            measureOption.Description = updateMeasureOptionDto.Description.ToLower();
        }
        await _context.SaveChangesAsync();
        
        return new Result<MeasureOptionDto>(200, measureOption);
    }

    public async Task<Result<bool>> DeleteMeasureOption(int measureTypeId, int measureOptionId)
    {
        var measureType = await _context.ProductMeasureTypes
            .Include(pmt => pmt.MeasureOptions)
            .FirstOrDefaultAsync(pmt => pmt.Id == measureTypeId);
        if (measureType == null)
        {
            return new Result<bool>(404, "No Measure Type with given id found.");
        }

        var measureOption = measureType.MeasureOptions.FirstOrDefault(mo => mo.Id == measureOptionId);
        if (measureOption == null)
        {
            return new Result<bool>(404, "No Measure Option with given id found.");
        }

        measureType.MeasureOptions.Remove(measureOption);
        return await _context.SaveChangesAsync() > 0
            ? new Result<bool>(204)
            : new Result<bool>(400, "Failed to delete Measure Option");
    }

    private IQueryable<MeasureOptionDto> GetMeasureOptionQueryable(int measureTypeId, int? measureOptionId = null, bool asNoTracking = false)
    {
        var query = _context.ProductMeasureOptions
            .AsQueryable()
            .Where(pmo => pmo.MeasureTypeId == measureTypeId);
        
        if (asNoTracking)
        {
            query = query.AsNoTracking();
        }
        
        if (measureOptionId != null)
        {
            query = query.Where(pmo => pmo.Id == measureOptionId);
        }

        return query.Select(pmo => new MeasureOptionDto(pmo.Id, pmo.Option, pmo.Description));
    }
}