using BookedIn.WebApi.Books;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Users;
using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/collections")]
public class UserBookCollectionController(
    IUserBookCollectionService userBookCollectionService,
    IBookService bookService,
    IUserService userService,
    ICurrentUserService currentUserService
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCollection([FromBody] CreateUserCollectionRequest newUserCollectionRequest)
    {
        var email = currentUserService.GetUserEmail();
        if (email == null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var books = new List<Book>();
        foreach (var workId in newUserCollectionRequest.WorkIds)
        {
            var bookDetails = await bookService.GetBookDetailsByIdAsync(workId);
            if (bookDetails == null) continue;

            var book = new Book(
                bookDetails.Authors.Select(a => a.Name).ToList(),
                bookDetails.Title,
                bookDetails.CoverId,
                bookDetails.WorkId,
                false
            );
            books.Add(book);
        }

        var newCollection = new UserBookCollection(
            newUserCollectionRequest.CollectionName,
            books,
            user
        );

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

        var currentUserEmail = currentUserService.GetUserEmail();
        if (collection.User.Email != currentUserEmail)
        {
            return Forbid();
        }

        await userBookCollectionService.RemoveAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/books")]
    public async Task<IActionResult> AddBookToCollection(string id, [FromBody] AddBookToCollectionRequest request)
    {
        var collection = await userBookCollectionService.GetAsync(id);
        if (collection == null)
        {
            return NotFound();
        }

        var currentUserEmail = currentUserService.GetUserEmail();
        if (collection.User.Email != currentUserEmail)
        {
            return Forbid();
        }

        var bookDetails = await bookService.GetBookDetailsByIdAsync(request.WorkId);
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

        var currentUserEmail = currentUserService.GetUserEmail();
        if (collection.User.Email != currentUserEmail)
        {
            return Forbid();
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

    [HttpGet("user/{email}")]
    public async Task<IActionResult> GetUserCollections(string email)
    {
        var user = await userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }

        var collections = await userBookCollectionService.GetAsync();
        var userCollections = collections.Where(c => c.User.Email == email).ToList();

        return Ok(userCollections);
    }
}