namespace API.Entities.Identity;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string Area { get; set; }
    public string ZipCode { get; set; }
    public bool IsMain { get; set; }

    public string UserId { get; set; }
    public User User { get; set; }
}