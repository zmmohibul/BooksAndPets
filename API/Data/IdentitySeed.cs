using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class IdentitySeed
{
    public static async Task AddUserData(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (await userManager.Users.AnyAsync())
        {
            return;
        }

        var roles = new List<IdentityRole>()
        {
            new IdentityRole { Name = UserRole.User.ToString() },
            new IdentityRole { Name = UserRole.Admin.ToString() }
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        var users = new List<User>
        {
            new User
            {
                UserName = "mohib",
                FirstName = "Mohibul",
                LastName = "Islam",
                PhoneNumber = "01711223344",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Street = "852, East Shewrapara",
                        Area = "Kafrul",
                        City = "Dhaka",
                        ZipCode = "1216",
                        IsMain = true
                    }
                }
            },
            
            new User
            {
                UserName = "hamid",
                FirstName = "Hamidul",
                LastName = "Islam",
                PhoneNumber = "01711223344",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Street = "492, West Shewrapara",
                        Area = "Kafrul",
                        City = "Dhaka",
                        ZipCode = "1216",
                        IsMain = true
                    }
                }
            }
        };

        await userManager.CreateAsync(users[0], "Pa$$w0rd");
        await userManager.AddToRoleAsync(users[0], UserRole.User.ToString());
        
        await userManager.CreateAsync(users[1], "Pa$$w0rd");
        await userManager.AddToRoleAsync(users[1], UserRole.Admin.ToString());
    }
}