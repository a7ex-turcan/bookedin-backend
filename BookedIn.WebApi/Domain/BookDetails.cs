namespace BookedIn.WebApi.Domain;

public record BookDetails(
    string Author,
    string Title,
    int CoverId,
    string WorkId,
    string Description,
    List<string> Subjects
) : Book(Author, Title, CoverId, WorkId);