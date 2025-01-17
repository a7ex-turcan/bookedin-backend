using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Services;

public interface IUserBookFavouriteService
{
    Task<List<UserBookFavourite>> GetAsync();
    Task<UserBookFavourite?> GetAsync(string id);
    Task CreateAsync(UserBookFavourite newFavourite);
    Task UpdateAsync(string id, UserBookFavourite updatedFavourite);
    Task RemoveAsync(string id);
    Task<List<UserBookFavourite>> GetByUserEmailAsync(string email);
}