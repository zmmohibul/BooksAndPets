using System.Security.Claims;
using API.Data;
using API.Dtos.Identity;
using API.Entities.Identity;
using API.Interfaces;
using API.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
public class AuthenticationController : BaseApiController
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AuthenticationController(DataContext context, UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users-with-roles-admin")]
    public IActionResult Secret()
    {
        return Ok("Admin Secret...");
    }
    
    [Authorize]
    [HttpGet("users-with-roles-user")]
    public IActionResult NotSecret()
    {
        return Ok("Authorized Secret...");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .SingleOrDefaultAsync(user => user.UserName.Equals(loginDto.UserName));

        if (user == null)
        {
            return Unauthorized(new ApiErrorResponse{ StatusCode = 401, ErrorMessage = "Invalid Username" });
        }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result)
        {
            return Unauthorized(new ApiErrorResponse{ StatusCode = 401, ErrorMessage = "Invalid Password" });
        }
        
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new UserDto
        {
            UserName = user.UserName,
            Role = roles[0],
            Token = await _tokenService.CreateToken(user)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if ((await _userManager.FindByNameAsync(registerDto.UserName)) != null)
        {
            return BadRequest(new ApiErrorResponse { StatusCode = 400, ErrorMessage = "UserName is taken." });
        }
        
        var user = new User
        {
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            PhoneNumber = registerDto.PhoneNumber
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new ApiErrorResponse{ StatusCode = 400, ErrorMessage = "Failed to create user."});
        }
        
        await _userManager.AddToRoleAsync(user, UserRole.User.ToString());

        var userDto = new UserDto
        {
            UserName = user.UserName,
            Token = await _tokenService.CreateToken(user),
            Role = UserRole.User.ToString()
        };
        
        return Ok(userDto);
    }

    [Authorize(Roles = "User")]
    [HttpPost("address")]
    public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
    {
        var address = _mapper.Map<Address>(createAddressDto);
        
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));
        user.Addresses.Add(address);

        await _context.SaveChangesAsync();
        return Ok(_mapper.Map<UserDetailsDto>(user));
    }
    
    [Authorize]
    [HttpGet("user-detail")]
    public async Task<IActionResult> GetCurrentUser()
    
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        
        var user = await _userManager.Users
            .Include(u => u.Addresses)
            .ProjectTo<UserDetailsDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(user => user.UserName.Equals(username));

        return Ok(user);
    }
}