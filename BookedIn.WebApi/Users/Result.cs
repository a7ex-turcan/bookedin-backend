namespace BookedIn.WebApi.Users;

/// <summary>
/// Represents a result that can contain either a success value or a list of errors.
/// </summary>
/// <typeparam name="T">The type of the success value.</typeparam>
public class Result<T>
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess => !Errors.Any();
    
    /// <summary>
    /// Gets the list of errors if the operation failed.
    /// </summary>
    public IReadOnlyList<string> Errors { get; }
    
    /// <summary>
    /// Gets the success value if the operation succeeded.
    /// </summary>
    public T? Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with a success value.
    /// </summary>
    /// <param name="value">The success value.</param>
    private Result(T value)
    {
        Errors = Array.Empty<string>();
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class with errors.
    /// </summary>
    /// <param name="errors">The collection of errors.</param>
    private Result(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
        Value = default;
    }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The success value.</param>
    /// <returns>A new successful result.</returns>
    public static Result<T> Success(T value) => new(value);

    /// <summary>
    /// Creates a failed result with a single error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A new failed result.</returns>
    public static Result<T> Failure(string error) => new([error]);

    /// <summary>
    /// Creates a failed result with multiple error messages.
    /// </summary>
    /// <param name="errors">The collection of error messages.</param>
    /// <returns>A new failed result.</returns>
    public static Result<T> Failure(IEnumerable<string> errors) => new(errors);
}