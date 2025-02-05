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

        var user = new User
        {
            Email = request.Email.Trim().ToLower(),
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

internal record ValidationResult
{
    public bool IsValid => !Errors.Any();
    public IReadOnlyList<string> Errors { get; }

    private ValidationResult(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }

    public static ValidationResult Success() => new(Enumerable.Empty<string>());
    public static ValidationResult Failure(string error) => new(new[] { error });
    public static ValidationResult Failure(IEnumerable<string> errors) => new(errors);
}


internal class SignUpRequestValidator
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

public class Result<T>
{
    public bool IsSuccess => !Errors.Any();
    public IReadOnlyList<string> Errors { get; }
    public T? Value { get; }

    private Result(T value)
    {
        Errors = Array.Empty<string>();
        Value = value;
    }

    private Result(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
        Value = default;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(new[] { error });
    public static Result<T> Failure(IEnumerable<string> errors) => new(errors);
}
