namespace API.Dtos.ProductDtoAggregate.ProductDtos;

public class PriceDto
{
    public decimal UnitPrice { get; set; }
    public int MeasureTypeId { get; set; }
    public string MeasureType { get; set; }
    public int MeasureOptionId { get; set; }
    public string MeasureOption { get; set; }
    public int QuantityInStock { get; set; }
}