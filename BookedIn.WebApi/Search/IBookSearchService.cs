// File: BookedIn.WebApi/Services/IBookSearchService.cs

using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Search;

public interface IBookSearchService
{
    Task<List<Book>> SearchBooksAsync(string query);

    Task<BookDetails?> GetBookDetailsByIdAsync(string id);
}