namespace API.Dtos.Identity;

public class UserDetailsDto
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<AddressDto> Addresses { get; set; } = new List<AddressDto>();
}