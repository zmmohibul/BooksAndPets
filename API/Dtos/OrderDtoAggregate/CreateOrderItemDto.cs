using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Dtos.OrderDtoAggregate;

public class CreateOrderItemDto
{
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    public int MeasureTypeId { get; set; }
    [Required]
    public int MeasureOptionId { get; set; }
}