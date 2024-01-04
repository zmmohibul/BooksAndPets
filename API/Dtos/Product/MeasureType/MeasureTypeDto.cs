using API.Dtos.Product.MeasureOption;

namespace API.Dtos.Product.MeasureType;

public class MeasureTypeDto
{
    public MeasureTypeDto(int id, string type, string desciption)
    {
        Id = id;
        Type = type;
        Desciption = desciption;
    }

    public MeasureTypeDto(int id, string type, string desciption, IEnumerable<MeasureOptionDto> measureOptions)
    {
        Id = id;
        Type = type;
        Desciption = desciption;
        MeasureOptions = measureOptions;
    }

    public int Id { get; set; }    
    public string Type { get; set; }
    public string Desciption { get; set; }
    public IEnumerable<MeasureOptionDto> MeasureOptions { get; set; } = new List<MeasureOptionDto>();
}