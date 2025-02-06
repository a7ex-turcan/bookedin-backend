namespace BookedIn.WebApi.Users;

public record ValidationResult
{
    public bool IsValid => !Errors.Any();
    public IReadOnlyList<string> Errors { get; }

    private ValidationResult(IEnumerable<string> errors)
    {
        Errors = errors.ToList();
    }

    public static ValidationResult Success() => new([]);
    
    public static ValidationResult Failure(string error) => new([error]);
    
    public static ValidationResult Failure(IEnumerable<string> errors) => new(errors);
}