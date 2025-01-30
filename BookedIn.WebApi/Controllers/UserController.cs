using Microsoft.AspNetCore.Mvc;
using BookedIn.WebApi.Users;
using BookedIn.WebApi.Auth;

namespace BookedIn.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    IUserService userService,
    ICurrentUserService currentUserService
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
        {
            return NotFound("User not found");
        }

        return Ok(user);
    }
}