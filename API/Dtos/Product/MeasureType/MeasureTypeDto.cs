using API.Dtos.Product.MeasureOption;

namespace API.Dtos.Product.MeasureType;

public class MeasureTypeDto
{
    public MeasureTypeDto(int id, string type)
    {
        Id = id;
        Type = type;
    }

    public MeasureTypeDto(int id, string type, IEnumerable<MeasureOptionDto> measureOptions)
    {
        Id = id;
        Type = type;
        MeasureOptions = measureOptions;
    }

    public int Id { get; set; }    
    public string Type { get; set; }
    public IEnumerable<MeasureOptionDto> MeasureOptions { get; set; } = new List<MeasureOptionDto>();
}