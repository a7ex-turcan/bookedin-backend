namespace BookedIn.WebApi.Domain;

public record Book(
    string Author, 
    string Title,
    int CoverId,
    string WorkId
);