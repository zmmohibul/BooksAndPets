using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string PhoneNumber { get; set; }
}