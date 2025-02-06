namespace BookedIn.WebApi.Users;

/// <summary>
/// Represents the result of a validation operation that can contain a list of errors.
/// </summary>
public record ValidationResult
{
    /// <summary>
    /// Gets a value indicating whether the validation was successful.
    /// </summary>
    public bool IsValid => !Errors.Any();

    /// <summary>
    /// Gets the list of validation errors if the validation failed.
    /// </summary>
    public IReadOnlyList<string> Errors { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationResult"/> class with the specified errors.
    /// </summary>
    /// <param name="errors">The collection of validation errors.</param>
    private ValidationResult(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }

    /// <summary>
    /// Creates a successful validation result with no errors.
    /// </summary>
    /// <returns>A new successful validation result.</returns>
    public static ValidationResult Success() => new([]);
    
    /// <summary>
    /// Creates a failed validation result with a single error message.
    /// </summary>
    /// <param name="error">The validation error message.</param>
    /// <returns>A new failed validation result.</returns>
    public static ValidationResult Failure(string error) => new([error]);
    
    /// <summary>
    /// Creates a failed validation result with multiple error messages.
    /// </summary>
    /// <param name="errors">The collection of validation error messages.</param>
    /// <returns>A new failed validation result.</returns>
    public static ValidationResult Failure(IEnumerable<string> errors) => new(errors);
}