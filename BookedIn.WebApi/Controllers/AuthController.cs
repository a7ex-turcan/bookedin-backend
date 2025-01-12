using BookedIn.WebApi.Data;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Models;
using BookedIn.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public AuthController(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return BadRequest("Email is already in use.");
        }

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc),
            PasswordHash = hashedPassword
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }
}