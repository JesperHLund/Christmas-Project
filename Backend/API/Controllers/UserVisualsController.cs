using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChristmasBackend.Application.Services;
using System.Security.Claims;

namespace ChristmasBackend.API.Controllers;

[ApiController]
[Route("user-visuals")]
[Authorize]
public class UserVisualsController : ControllerBase
{
    private readonly UserService _userService;

    public UserVisualsController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUserVisuals()
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var visuals = await _userService.GetVisualSettingsForUser(userId);

        // visuals contains background color, animation file names, etc.
        return Ok(visuals);
    }
}
