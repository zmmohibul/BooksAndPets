using API.Dtos.Product.MeasureOption;
using API.Utils;

namespace API.Interfaces.RepositoryInterfaces.Product;

public interface IMeasureOptionRepository
{
    public Task<Result<ICollection<MeasureOptionDto>>> GetAllMeasureOptionDtos(int measureTypeId);
    public Task<Result<MeasureOptionDto>> GetMeasureOptionDtoById(int measureTypeId, int measureOptionId);

    public Task<Result<MeasureOptionDto>> CreateMeasureOption(int measureTypeId, CreateMeasureOptionDto createMeasureOptionDto);
    public Task<Result<MeasureOptionDto>> UpdateMeasureOption(int measureTypeId, int measureOptionId, UpdateMeasureOptionDto updateMeasureOptionDto);
    public Task<Result<bool>> DeleteMeasureOption(int measureTypeId, int measureOptionId);
}