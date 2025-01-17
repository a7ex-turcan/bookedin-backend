using BookedIn.WebApi.Data;
using BookedIn.WebApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Users;

public class UserService(ApplicationDbContext context) : IUserService
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}