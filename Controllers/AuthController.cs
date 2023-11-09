using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Services;

namespace SaginEmployees.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    
    private readonly JwtTokenHandlerService _jwtTokenHandlerService;

    public AuthController(JwtTokenHandlerService jwtTokenHandlerService) =>
        _jwtTokenHandlerService = jwtTokenHandlerService;

    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok(_jwtTokenHandlerService.GenerateJwtToken(new ClaimsIdentity(new Claim[]
            {
                new Claim("username", "Sagin"),
            }),
            DateTime.UtcNow.AddHours(1)));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok("Register");
    }

}