using BookedIn.WebApi.Books;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Users;
using Microsoft.AspNetCore.Mvc;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserBookCollectionController(
    IUserBookCollectionService userBookCollectionService,
    IBookService bookService,
    IUserService userService
)
    : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost]
    public async Task<IActionResult> CreateCollection([FromBody] UserBookCollection newCollection)
    {
        await userBookCollectionService.CreateAsync(newCollection);
        return CreatedAtAction(nameof(GetCollection), new { id = newCollection.Id }, newCollection);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCollection(string id)
    {
        var collection = await userBookCollectionService.GetAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        await userBookCollectionService.RemoveAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/books")]
    public async Task<IActionResult> AddBookToCollection(string id, [FromBody] string workId)
    {
        var collection = await userBookCollectionService.GetAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        var bookDetails = await bookService.GetBookDetailsByIdAsync(workId);
        if (bookDetails == null)
        {
            return NotFound();
        }

        var book = new Book(
            bookDetails.Authors.Select(a => a.Name).ToList(),
            bookDetails.Title,
            bookDetails.CoverId,
            bookDetails.WorkId,
            false
        );
        collection.AddBook(book);

        await userBookCollectionService.UpdateAsync(id, collection);
        return NoContent();
    }

    [HttpDelete("{id}/books/{workId}")]
    public async Task<IActionResult> RemoveBookFromCollection(string id, string workId)
    {
        var collection = await userBookCollectionService.GetAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        collection.RemoveBook(workId);
        await userBookCollectionService.UpdateAsync(id, collection);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCollection(string id)
    {
        var collection = await userBookCollectionService.GetAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        return Ok(collection);
    }
}