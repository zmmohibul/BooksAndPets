using API.Dtos.ProductDtoAggregate.MeasureTypeDtos;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces;

public interface IMeasureTypeRepository
{
    public Task<Result<ICollection<MeasureTypeDto>>> GetAllMeasureTypeDtos();
    public Task<Result<MeasureTypeDto>> GetMeasureTypeDtoById(int id);
    public Task<Result<MeasureTypeDto>> CreateMeasureType(CreateMeasureTypeDto createMeasureTypeDto);
    public Task<Result<MeasureTypeDto>> UpdateMeasureType(int id, UpdateMeasureTypeDto updateMeasureTypeDto);
    public Task<Result<bool>> DeleteMeasureType(int id);
}