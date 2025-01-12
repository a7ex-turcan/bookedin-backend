namespace BookedIn.WebApi.Models;

public record SignUpRequest(
    string Email,
    string FullName,
    DateTime DateOfBirth,
    string Password
);