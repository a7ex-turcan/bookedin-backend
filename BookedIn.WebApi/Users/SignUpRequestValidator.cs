using BookedIn.WebApi.Models;

namespace BookedIn.WebApi.Users;

public class SignUpRequestValidator
{
    public ValidationResult Validate(SignUpRequest request)
    {
        var errors = new List<string>();
        var minAge = 13;
        var maxAge = 120;
        var age = DateTime.UtcNow.Year - request.DateOfBirth.Year;

        if (request.DateOfBirth > DateTime.UtcNow)
        {
            errors.Add("Date of birth cannot be in the future");
        }

        if (age < minAge || age > maxAge)
        {
            errors.Add($"Age must be between {minAge} and {maxAge} years");
        }

        if (request.Password.Contains(request.Email, StringComparison.OrdinalIgnoreCase))
        {
            errors.Add("Password cannot contain email address");
        }

        return errors.Any() ? ValidationResult.Failure(errors) : ValidationResult.Success();
    }
}