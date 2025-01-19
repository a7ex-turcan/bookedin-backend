using BookedIn.WebApi.Books;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search;
using Microsoft.AspNetCore.Mvc;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookSearchService bookSearchService, IBookService bookService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<ActionResult<List<Book>>> SearchBooks(
        string query,
        [FromQuery] int? limit = 10
    )
    {
        var books = await bookSearchService.SearchBooksAsync(query, limit, HttpContext.RequestAborted);
        return Ok(books);
    }

    [HttpGet("cover/{coverId}")]
    public IActionResult GetCoverImage(int coverId, [FromQuery] string size = "L")
    {
        var imageUrl = bookService.GetCoverImageUrl(coverId, size);
        return Redirect(imageUrl);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDetails>> GetBookDetails(string id)
    {
        var bookDetails = await bookService.GetBookDetailsByIdAsync(id);
        if (bookDetails == null)
        {
            return NotFound();
        }
        return Ok(bookDetails);
    }
}