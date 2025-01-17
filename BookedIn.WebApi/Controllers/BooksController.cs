using Microsoft.AspNetCore.Mvc;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IBookSearchService bookSearchService) : ControllerBase
{
    [HttpGet("search")]
    public async Task<ActionResult<List<Book>>> SearchBooks(string query)
    {
        var books = await bookSearchService.SearchBooksAsync(query);
        return Ok(books);
    }
}