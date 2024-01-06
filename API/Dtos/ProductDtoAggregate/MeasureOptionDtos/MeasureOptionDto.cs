namespace API.Dtos.ProductDtoAggregate.MeasureOptionDtos;

public class MeasureOptionDto
{
    public MeasureOptionDto(int id, string option, string description)
    {
        Id = id;
        Option = option;
        Description = description;
    }

    public int Id { get; set; }
    public string Option { get; set; }
    public string Description { get; set; }
}