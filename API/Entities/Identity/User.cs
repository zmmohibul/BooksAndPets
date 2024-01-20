using API.Entities.ProductAggregate;
using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity;

public class User : IdentityUser
{
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<ReviewRating> ReviewRatings { get; set; }
}