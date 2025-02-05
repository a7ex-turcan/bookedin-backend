namespace BookedIn.WebApi.Models;

public record SignUpRequest(
    string Email,
    string FullName,
    string? Nickname,
    DateTime DateOfBirth,
    string Password
);