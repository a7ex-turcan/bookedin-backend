using System.ComponentModel.DataAnnotations;
using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Users;

internal class UserService(
    ApplicationDbContext context,
    IPasswordHasher passwordHasher,
    SignUpRequestValidator validator
) : IUserService
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> CreateUserAsync(SignUpRequest request)
    {
        validator.Validate(request);

        var user = new User
        {
            Email = request.Email.Trim().ToLower(),
            FullName = request.FullName.Trim(),
            Nickname = (request.Nickname?.Trim() ?? request.FullName.Trim()),
            DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc),
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return true;
    }
}

internal class SignUpRequestValidator
{
     public void Validate(SignUpRequest request)
    {
        var minAge = 13;
        var maxAge = 120;
        var age = DateTime.UtcNow.Year - request.DateOfBirth.Year;

        if (request.DateOfBirth > DateTime.UtcNow)
        {
            throw new ValidationException("Date of birth cannot be in the future");
        }

        if (age < minAge || age > maxAge)
        {
            throw new ValidationException($"Age must be between {minAge} and {maxAge} years");
        }

        if (request.Password.Contains(request.Email, StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException("Password cannot contain email address");
        }
    }
}