using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Identity;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
    
}