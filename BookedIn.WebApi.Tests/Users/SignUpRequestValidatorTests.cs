using System.ComponentModel;
using BookedIn.WebApi.Models;
using BookedIn.WebApi.Users;
using Xunit;
using Xunit.Abstractions;

namespace BookedIn.WebApi.Tests.Users;

public class SignUpRequestValidatorTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly SignUpRequestValidator _validator;

    public SignUpRequestValidatorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _validator = new SignUpRequestValidator();
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator accepts request when all fields are valid")]
    public void Validate_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var request = new SignUpRequest("test@example.com", "ValidPass123!", "Test User", DateTime.UtcNow.AddYears(-20), "password");

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator rejects birth dates that are in the future")]
    public void Validate_WithFutureDateOfBirth_ReturnsFailure()
    {
        // Arrange
       var request = new SignUpRequest(
           "test@example.com",
           "ValidPass123!",
           "Test User",
           DateTime.UtcNow.AddDays(1),
           "password"
       );

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Date of birth cannot be in the future", result.Errors[0]);
    }

    [Theory]
    [Trait("Category", "Validation")]
    [DisplayName("Validator enforces age restrictions (13-120 years)")]
    [InlineData(12, "Too young")]
    [InlineData(121, "Too old")]
    public void Validate_WithInvalidAge_ReturnsFailure(int age, string scenario)
    {
        _testOutputHelper.WriteLine($"Scenario: {scenario}");
        
        // Arrange
        var request = new SignUpRequest(
            "test@example.com",
            "ValidPass123!",
            "Test User",
            DateTime.UtcNow.AddYears(-age),
            "password"
        );;

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Age must be between 13 and 120 years", result.Errors[0]);
    }

    [Fact]
    [Trait("Category", "Security")]
    [DisplayName("Validator prevents using email address within password")]
    public void Validate_WithEmailInPassword_ReturnsFailure()
    {
        // Arrange
       var request = new SignUpRequest(
           "test@example.com",
           "MyTest@Example.comPassword",  // Email embedded within password with different casing
           "Test User",
           DateTime.UtcNow.AddYears(-20),
           "password"
       );

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Password cannot contain email address", result.Errors[0]);
    }

    [Fact]
    [Trait("Category", "Validation")]
    [DisplayName("Validator collects multiple validation errors")]
    public void Validate_WithMultipleInvalidFields_ReturnsAllErrors()
    {
        // Arrange
        var request = new SignUpRequest(
            "test@example.com",
            "test@example.com123", // Contains email
            "Test User",
            DateTime.UtcNow.AddYears(-121),
            "password"
        );

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal(2, result.Errors.Count);
        Assert.Contains("Age must be between 13 and 120 years", result.Errors[0]);
        Assert.Contains("Password cannot contain email address", result.Errors[0]);
    }
}