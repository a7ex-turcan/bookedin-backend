using Microsoft.AspNetCore.Mvc;
using BookedIn.WebApi.Users;
using BookedIn.WebApi.Auth;
using BookedIn.WebApi.Books;
using BookedIn.WebApi.Models;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    ICurrentUserService currentUserService,
    IUserBookCollectionService userBookCollectionService
) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var email = currentUserService.GetUserEmail();
        if (email == null)
            return Unauthorized();

        var user = await userService.GetUserByEmailAsync(email);
        if (user == null)
            return NotFound("User not found");

        var collections = await userBookCollectionService.GetAsync();
        var userCollections = collections
            .Where(c => c.User.Email == email)
            .Select(
                c => new BookCollectionInfo(
                    c.CollectionName,
                    c.Books.Select(b => b.WorkId).ToList()
                )
            )
            .ToList();

        var userDto = new UserInfo(
            user.Email,
            user.FullName,
            user.DateOfBirth,
            userCollections
        );

        return Ok(userDto);
    }
}