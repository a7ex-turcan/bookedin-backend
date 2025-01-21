using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Auth;

public interface ITokenService
{
    string GenerateToken(User user);
}