namespace BookedIn.WebApi.Models;

public record CreateUserCollectionRequest(
    string CollectionName,
    List<string> WorkIds
);