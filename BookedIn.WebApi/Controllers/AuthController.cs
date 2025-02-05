using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenService tokenService
) : ControllerBase
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        if (await EmailExists(request.Email))
        {
            return BadRequest("Email is already in use.");
        }

        var user = CreateUser(request);
        await SaveUser(user);

        return Ok("User registered successfully.");
    }

    private async Task<bool> EmailExists(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    private User CreateUser(SignUpRequest request)
    {
        return new User
        {
            Email = request.Email,
            FullName = request.FullName,
            Nickname = request.Nickname ?? request.FullName,
            DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc),
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };
    }

    private async Task SaveUser(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
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