using System.ComponentModel.DataAnnotations;

namespace API.Dtos.OrderDtoAggregate;

public class CreateOrderDto
{
    public int AddressId { get; set; }
    [Required]
    public ICollection<CreateOrderItemDto> OrderItems { get; set; }
}