using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Books;

public interface IBookService
{
    Task<BookDetails?> GetBookDetailsByIdAsync(string id);
    string GetCoverImageUrl(int coverId, string size);
    Task<byte[]> GetCoverImageAsync(int coverId, string size);
}