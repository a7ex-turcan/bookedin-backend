namespace BookedIn.WebApi.Domain;

public record Book(
    string Author, 
    string Title, 
    string Isbn,
    int CoverId,
    string WorkId // New property to hold the work ID
);