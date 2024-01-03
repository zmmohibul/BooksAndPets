namespace API.Dtos.Product.MeasureOption;

public class MeasureOptionDto
{
    public MeasureOptionDto(int id, string option)
    {
        Id = id;
        Option = option;
    }

    public int Id { get; set; }
    public string Option { get; set; }
}