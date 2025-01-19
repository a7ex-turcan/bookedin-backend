
namespace BookedIn.WebApi.Domain;

public record BookDetails(
    List<Author> Authors,
    string Title,
    int CoverId,
    string WorkId,
    string Description,
    List<string> Subjects
);