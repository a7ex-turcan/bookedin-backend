using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Users;

public class UserService(
    ApplicationDbContext context,
    IPasswordHasher passwordHasher
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
        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            Nickname = request.Nickname ?? request.FullName,
            DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc),
            PasswordHash = passwordHasher.HashPassword(request.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();
        return true;
    }
}