using System.Threading;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Search;

public interface IBookSearchService
{
    Task<List<Book>> SearchBooksAsync(string query, int? limit, CancellationToken cancellationToken);
  
}