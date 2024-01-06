namespace API.Dtos.ProductDtoAggregate.ProductDtos;

public class PriceDto
{
    public decimal UnitPrice { get; set; }
    public string MeasureType { get; set; }
    public string MeasureOption { get; set; }
    public int QuantityInStock { get; set; }
}