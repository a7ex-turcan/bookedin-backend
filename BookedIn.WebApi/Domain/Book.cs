namespace BookedIn.WebApi.Domain;

public record Book(
    List<string> Authors, 
    string Title,
    int CoverId,
    string WorkId
);