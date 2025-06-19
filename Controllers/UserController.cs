using Microsoft.AspNetCore.Mvc;
using e_commerce.Data;
using e_commerce.Models.User;
using e_commerce.Services;
using Microsoft.AspNetCore.Authorization;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AuthService _authService;

    public UserController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
    {
        var token = await _authService.RegisterAsync(dto);
        if (token == null)
            return BadRequest("Email already exists");
        
        return Ok(new { token });
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized(new { message = "Invalid Credentials" });
        
        return Ok(new { token });
    }
}