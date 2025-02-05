using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Models;
using BookedIn.WebApi.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    IUserService userService
) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        if (await userService.EmailExistsAsync(request.Email))
        {
            return BadRequest(new { errors = new[] { "Email is already in use." } });
        }

        var result = await userService.CreateUserAsync(request);
        if (!result.IsSuccess)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null || !passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            return Unauthorized("Invalid email or password.");
        }

        var token = tokenService.GenerateToken(user);

        return Ok(new LoginResponse { Token = token });
    }
}