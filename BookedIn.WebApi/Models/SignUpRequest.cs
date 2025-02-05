using System.ComponentModel.DataAnnotations;

namespace BookedIn.WebApi.Models;

public record SignUpRequest(
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    string Email,

    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    string FullName,

    [StringLength(50, ErrorMessage = "Nickname cannot exceed 50 characters")]
    string? Nickname,

    [Required(ErrorMessage = "Date of birth is required")]
    DateTime DateOfBirth,

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
    string Password
);