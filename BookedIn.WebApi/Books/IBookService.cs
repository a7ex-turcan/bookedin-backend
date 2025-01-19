using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Domain.OpenLibraryExtensions;
using BookedIn.WebApi.OpenLibrary;

namespace BookedIn.WebApi.Books;

public interface IBookService
{
    Task<BookDetails?> GetBookDetailsByIdAsync(string id);
    string GetCoverImageUrl(int coverId, string size);
}

public class BookService(HttpClient httpClient) : IBookService
{
    public async Task<BookDetails?> GetBookDetailsByIdAsync(string id)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/works/{id}.json");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var openLibraryBookDetails = JsonSerializer.Deserialize<OpenLibraryBookDetails>(jsonResponse);

        return openLibraryBookDetails?.ToBookDetails();
    }

    public string GetCoverImageUrl(int coverId, string size)
    {
        return $"https://covers.openlibrary.org/b/id/{coverId}-{size.ToUpper()}.jpg";
    }
}