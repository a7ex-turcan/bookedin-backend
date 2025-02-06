namespace BookedIn.WebApi.Users;

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

    public static Result<T> Failure(string error) => new([error]);

    public static Result<T> Failure(IEnumerable<string> errors) => new(errors);
}