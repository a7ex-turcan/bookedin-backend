// File: BookedIn.WebApi/Services/IBookSearchService.cs

using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Services;

public interface IBookSearchService
{
    Task<List<Book>> SearchBooksAsync(string query);
}