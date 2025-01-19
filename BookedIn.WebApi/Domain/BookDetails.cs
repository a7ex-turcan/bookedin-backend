namespace BookedIn.WebApi.Domain;

public record BookDetails(
    List<string> Authors,
    string Title,
    int CoverId,
    string WorkId,
    string Description,
    List<string> Subjects
) : Book(Authors, Title, CoverId, WorkId);