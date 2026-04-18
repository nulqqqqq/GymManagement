using GymManagement.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        var token = _tokenService.CreateToken("admin_user", "Admin");
            
        return Ok(new { Token = token });
    }
}