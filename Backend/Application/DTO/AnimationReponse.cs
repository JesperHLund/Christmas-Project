using ChristmasBackend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChristmasBackend.API.Controllers;

[ApiController]
[Route("animations")]
[Authorize]
public class AnimationController : ControllerBase
{
    private readonly UserService _userService;

    public AnimationController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyAnimations()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var animations = await _userService.GetUserAnimationsAsync(userId);
        return Ok(animations);
    }
}
