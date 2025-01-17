using Microsoft.AspNetCore.Mvc;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search;
using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<ActionResult<List<Book>>> SearchBooks(string query)
    {
        var books = await bookService.SearchBooksAsync(query);
        return Ok(books);
    }
    
    [HttpGet("cover/{coverId}")]
    public IActionResult GetCoverImage(int coverId, [FromQuery] string size = "L")
    {
        var imageUrl = bookService.GetCoverImageUrl(coverId, size);
        return Redirect(imageUrl);
    }
}