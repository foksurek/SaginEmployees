using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SaginEmployees.Dto;
using SaginEmployees.Services;
using SaginEmployees.ViewModels;

namespace SaginEmployees.Controllers;


[Route("[controller]")]
public class AuthController : Controller
{
    
    private readonly JwtTokenHandlerService _jwtTokenHandlerService;
    private readonly MongoDbService _mongoDb;

    public AuthController(JwtTokenHandlerService jwtTokenHandlerService, MongoDbService mongoDb)
    {
        _jwtTokenHandlerService = jwtTokenHandlerService;
        _mongoDb = mongoDb;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest("User need to provide email and password");
        
        var user = await _mongoDb.GetEmployerByEmail(model.Email);
        if (user == null) return NotFound("Wrong username or password");

        if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password)) return NotFound("Wrong username or password");

            return Ok(_jwtTokenHandlerService.GenerateJwtToken(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", user.Id),
            }),
            DateTime.UtcNow.AddHours(24)));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
    {
        var email = model.Email;
        var password = BCrypt.Net.BCrypt.HashPassword(model.Password);

        var user = new EmployerDto()
        {
            Id = "",
            Email = email,
            Password = password,
        };
        
        await _mongoDb.CreateEmployer(user);
        
        return Ok("Register");
    }
}