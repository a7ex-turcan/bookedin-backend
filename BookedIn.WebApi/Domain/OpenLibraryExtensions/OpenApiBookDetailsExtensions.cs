using BookedIn.WebApi.OpenLibrary;

namespace BookedIn.WebApi.Domain.OpenLibraryExtensions;

public static class OpenApiBookDetailsExtensions
{
    public static BookDetails ToBookDetails(
        this OpenLibraryBookDetails openLibraryBookDetails, 
        IEnumerable<OpenLibraryAuthor> openLibraryAuthors,
        bool isFavourite
    ) =>
        new(
            Authors: openLibraryAuthors.Select(a => a.ToAuthor()).ToList(),
            Title: openLibraryBookDetails.Title,
            CoverId: openLibraryBookDetails.Covers.FirstOrDefault(),
            WorkId: openLibraryBookDetails.Key.Replace("/works/", ""),
            Description: openLibraryBookDetails.Description?.Value ?? string.Empty,
            Subjects: openLibraryBookDetails.Subjects,
            IsFavorite:isFavourite
        );
    
    public static Author ToAuthor(this OpenLibraryAuthor openLibraryAuthor) =>
        new(
            Name: openLibraryAuthor.Name,
            Key: openLibraryAuthor.Key.Replace("/authors/", "")
        );
}