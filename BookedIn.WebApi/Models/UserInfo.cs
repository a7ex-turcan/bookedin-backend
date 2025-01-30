namespace BookedIn.WebApi.Models;

public record UserInfo(
    string Email,
    string FullName,
    DateTime DateOfBirth,
    List<BookCollectionInfo> Collections
);

public record BookCollectionInfo(string CollectionName, List<string> WorkIds);