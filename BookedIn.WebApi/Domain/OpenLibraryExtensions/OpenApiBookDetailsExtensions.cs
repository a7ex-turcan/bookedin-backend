using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Domain.OpenLibraryExtensions;

public static class OpenApiBookDetailsExtensions
{
    public static BookDetails ToBookDetails(this OpenLibraryBookDetails openLibraryBookDetails) =>
        new(
            Authors: openLibraryBookDetails.Authors.Select(a => a.Author.Key).ToList(),
            Title: openLibraryBookDetails.Title,
            CoverId: openLibraryBookDetails.Covers.FirstOrDefault(),
            WorkId: openLibraryBookDetails.Key.Replace("/works/", ""),
            Description: openLibraryBookDetails.Description?.Value ?? string.Empty,
            // Description: openLibraryBookDetails.DescriptionRaw,
            Subjects: openLibraryBookDetails.Subjects
        );
}