namespace BookedIn.WebApi.Domain;

public record UserBookFavourite(
    string Id,
    User User,
    Book Book,
    DateTime DateAdded
);