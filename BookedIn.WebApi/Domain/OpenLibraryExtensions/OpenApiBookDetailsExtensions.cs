using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Domain.OpenLibraryExtensions;

public static class OpenApiBookDetailsExtensions
{
    
    public static BookDetails ToBookDetails(this OpenLibraryBookDetails openLibraryBookDetails)
    {
        return new BookDetails(
            Author: string.Join(", ", openLibraryBookDetails.Authors.Select(a => a.Author.Key)),
            Title: openLibraryBookDetails.Title,
            CoverId: openLibraryBookDetails.Covers.FirstOrDefault(),
            WorkId: openLibraryBookDetails.Key.Replace("/works/", ""),
            Description: openLibraryBookDetails.Description?.Value ?? string.Empty,
            Subjects: openLibraryBookDetails.Subjects
        );
    }
}