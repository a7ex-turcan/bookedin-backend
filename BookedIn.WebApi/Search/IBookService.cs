using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Search;

public interface IBookService
{
    Task<List<Book>> SearchBooksAsync(string query, int? limit);
    Task<OpenApiBookDetails?> GetBookDetailsByIdAsync(string id);
    string GetCoverImageUrl(int coverId, string size);
}