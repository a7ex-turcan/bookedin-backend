using MongoDB.Driver;
using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Services;

public class UserBookFavouriteService : IUserBookFavouriteService
{
    private readonly IMongoCollection<UserBookFavourite> _userBookFavourites;

    public UserBookFavouriteService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("bookedin");
        _userBookFavourites = database.GetCollection<UserBookFavourite>("UserBookFavourites");
    }

    public async Task<List<UserBookFavourite>> GetAsync() =>
        await _userBookFavourites.Find(fav => true).ToListAsync();

    public async Task<UserBookFavourite?> GetAsync(string id) =>
        await _userBookFavourites.Find(fav => fav.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserBookFavourite newFavourite) =>
        await _userBookFavourites.InsertOneAsync(newFavourite);

    public async Task UpdateAsync(string id, UserBookFavourite updatedFavourite) =>
        await _userBookFavourites.ReplaceOneAsync(fav => fav.Id == id, updatedFavourite);

    public async Task RemoveAsync(string id) =>
        await _userBookFavourites.DeleteOneAsync(fav => fav.Id == id);

    public async Task<List<UserBookFavourite>> GetByUserEmailAsync(string email) =>
        await _userBookFavourites.Find(fav => fav.User.Email == email).ToListAsync();
}