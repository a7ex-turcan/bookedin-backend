using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookedIn.WebApi.Books;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search;
using BookedIn.WebApi.Users;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavouritesController(
    IUserBookFavouriteService userBookFavouriteService,
    IUserService userService,
    IBookService bookService
) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UserBookFavourite>>> Get()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        var favourites = await userBookFavouriteService.GetByUserEmailAsync(email);
        return Ok(favourites);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserBookFavourite>> Get(string id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        var favourite = await userBookFavouriteService.GetAsync(id);
        if (favourite == null || favourite.User.Email != email)
        {
            return NotFound();
        }

        return Ok(favourite);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        var favourite = await userBookFavouriteService.GetAsync(id);
        if (favourite == null || favourite.User.Email != email)
        {
            return NotFound();
        }

        await userBookFavouriteService.RemoveAsync(id);
        return NoContent();
    }

    [HttpGet("user/{email}")]
    public async Task<ActionResult<List<UserBookFavourite>>> GetByUserEmail(string email)
    {
        var favourites = await userBookFavouriteService.GetByUserEmailAsync(email);
        return Ok(favourites);
    }

    [HttpPost]
    public async Task<IActionResult> AddFavourite([FromBody] string workId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        var user = await userService.GetUserByEmailAsync(email);
        if (user == null)
        {
            return NotFound("User not found");
        }

        var bookDetails = await bookService.GetBookDetailsByIdAsync(workId);
        if (bookDetails == null)
        {
            return NotFound("Book not found");
        }

        var newFavourite = new UserBookFavourite(
            Id: Guid.NewGuid().ToString(),
            User: user,
            Book: new Book(
                Author: string.Join(", ", bookDetails.Authors),
                Title: bookDetails.Title,
                CoverId: bookDetails.Covers.FirstOrDefault(),
                WorkId: workId
            ),
            DateAdded: DateTime.UtcNow
        );

        await userBookFavouriteService.CreateAsync(newFavourite);
        return CreatedAtAction(nameof(Get), new { id = newFavourite.Id }, newFavourite);
    }
}