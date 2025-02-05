using BookedIn.WebApi.Models;
using BookedIn.WebApi.Users;
using Xunit;

namespace BookedIn.WebApi.Tests.Users;

public class SignUpRequestValidatorTests
{
    private readonly SignUpRequestValidator _validator;

    public SignUpRequestValidatorTests()
    {
        _validator = new SignUpRequestValidator();
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator accepts request when all fields are valid")]
    public void Validate_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = "test@example.com",
            Password = "ValidPass123!",
            FullName = "Test User",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Null(result.Error);
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator rejects birth dates that are in the future")]
    public void Validate_WithFutureDateOfBirth_ReturnsFailure()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = "test@example.com",
            Password = "ValidPass123!",
            FullName = "Test User",
            DateOfBirth = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Date of birth cannot be in the future", result.Error);
    }

    [Theory]
    [Trait("Category", "Validation")]
    [DisplayName("Validator enforces age restrictions (13-120 years)")]
    [InlineData(12, "Too young")]
    [InlineData(121, "Too old")]
    public void Validate_WithInvalidAge_ReturnsFailure(int age, string scenario)
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = "test@example.com",
            Password = "ValidPass123!",
            FullName = "Test User",
            DateOfBirth = DateTime.UtcNow.AddYears(-age)
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Age must be between 13 and 120 years", result.Error);
    }

    [Fact]
    [Trait("Category", "Security")]
    [DisplayName("Validator prevents using email address within password")]
    public void Validate_WithEmailInPassword_ReturnsFailure()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = "test@example.com",
            Password = "MyTest@Example.comPassword",  // Email embedded within password with different casing
            FullName = "Test User",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Password cannot contain email address", result.Error);
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator collects multiple validation errors")]
    public void Validate_WithMultipleInvalidFields_ReturnsAllErrors()
    {
        // Arrange
        var request = new SignUpRequest
        {
            Email = "test@example.com",
            Password = "test@example.com123", // Contains email
            FullName = "Test User",
            DateOfBirth = DateTime.UtcNow.AddYears(-121) // Too old
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains("Age must be between 13 and 120 years", result.Errors);
        Assert.Contains("Password cannot contain email address", result.Errors);
    }
}