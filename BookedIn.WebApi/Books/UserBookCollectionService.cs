using BookedIn.WebApi.Domain;
using MongoDB.Driver;

namespace BookedIn.WebApi.Books;

public class UserBookCollectionService : IUserBookCollectionService
{
    private readonly IMongoCollection<UserBookCollection> _userBookCollections;

    public UserBookCollectionService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("bookedin");
        _userBookCollections = database.GetCollection<UserBookCollection>("UserBookCollections");
    }

    public async Task<List<UserBookCollection>> GetAsync() =>
        await _userBookCollections.Find(collection => true).ToListAsync();

    public async Task<UserBookCollection?> GetAsync(string id) =>
        await _userBookCollections.Find(collection => collection.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(UserBookCollection newCollection) =>
        await _userBookCollections.InsertOneAsync(newCollection);

    public async Task UpdateAsync(string id, UserBookCollection updatedCollection) =>
        await _userBookCollections.ReplaceOneAsync(collection => collection.Id == id, updatedCollection);

    public async Task RemoveAsync(string id) =>
        await _userBookCollections.DeleteOneAsync(collection => collection.Id == id);
}