using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavouritesController(IUserBookFavouriteService userBookFavouriteService) : ControllerBase
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

    [HttpPost]
    public async Task<ActionResult> Create(UserBookFavourite newFavourite)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        if (newFavourite.User.Email != email)
        {
            return BadRequest("You can only add favourites for your own account.");
        }

        await userBookFavouriteService.CreateAsync(newFavourite);
        return CreatedAtAction(nameof(Get), new { id = newFavourite.Id }, newFavourite);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UserBookFavourite updatedFavourite)
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

        if (updatedFavourite.User.Email != email)
        {
            return BadRequest("You can only update favourites for your own account.");
        }

        await userBookFavouriteService.UpdateAsync(id, updatedFavourite);
        return NoContent();
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
}