using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity;

public class CreateAddressDto
{
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Area { get; set; }
    [Required]
    public string ZipCode { get; set; }

    [Required] public bool IsMain { get; set; } = true;
}