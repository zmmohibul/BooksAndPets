namespace API.Dtos.OrderDtoAggregate;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string PictureUrl { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string MeasureType { get; set; }
    public string MeasureOption { get; set; }
}