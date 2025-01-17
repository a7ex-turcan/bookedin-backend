// File: BookedIn.WebApi/Services/IUserService.cs

using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Users;

public interface IUserService
{
    Task<User?> GetUserByEmailAsync(string email);
}
// File: BookedIn.WebApi/Services/UserService.cs

// File: BookedIn.WebApi/Users/UserService.cs