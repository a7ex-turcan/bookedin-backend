using BookedIn.WebApi.Domain;

namespace BookedIn.WebApi.Books;

public interface IUserBookCollectionService
{
    Task<List<UserBookCollection>> GetAsync();
    
    Task<UserBookCollection?> GetAsync(string id);
    
    Task CreateAsync(UserBookCollection newCollection);
    
    Task UpdateAsync(string id, UserBookCollection updatedCollection);
    
    Task RemoveAsync(string id);
    
}