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

    public async Task<Result<bool>> CreateUserAsync(SignUpRequest request)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result<bool>.Failure(validationResult.Errors);
        }

        var normalizedEmail = request.Email.Trim().ToLower();
        if (await EmailExistsAsync(normalizedEmail))
        {
            return Result<bool>.Failure("Email is already in use.");
        }

        var user = new User
        {
            Email = normalizedEmail,
            FullName = request.FullName.Trim(),
            Nickname = (request.Nickname?.Trim() ?? request.FullName.Trim()),
            DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc),
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Result<bool>.Success(true);
    }
}