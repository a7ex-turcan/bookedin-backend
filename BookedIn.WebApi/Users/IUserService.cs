// File: BookedIn.WebApi/Services/IUserService.cs

using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Models;

namespace BookedIn.WebApi.Users;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> CreateUserAsync(SignUpRequest request);
}
// File: BookedIn.WebApi/Services/UserService.cs

// File: BookedIn.WebApi/Users/UserService.cs