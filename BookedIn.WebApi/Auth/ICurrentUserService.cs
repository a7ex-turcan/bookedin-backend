namespace BookedIn.WebApi.Auth;

public interface ICurrentUserService
{
    string? GetUserEmail();
}