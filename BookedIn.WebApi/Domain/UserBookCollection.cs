namespace BookedIn.WebApi.Domain;

public record UserBookCollection(
    string CollectionName,
    List<Book> Books,
    User User
)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime Created { get; init; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;

    public void AddBook(Book book)
    {
        if (Books.Any(b => b.WorkId == book.WorkId)) return;

        Books.Add(book);
        LastUpdated = DateTime.UtcNow;
    }

    public void RemoveBook(string workId)
    {
        var book = Books.FirstOrDefault(b => b.WorkId == workId);
        if (book != null)
        {
            Books.Remove(book);
            LastUpdated = DateTime.UtcNow;
        }
    }
}