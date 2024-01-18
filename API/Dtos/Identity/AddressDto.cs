namespace API.Dtos.Identity;

public class AddressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string ZipCode { get; set; }
    public bool IsMain { get; set; }
}