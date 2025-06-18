using Microsoft.AspNetCore.Mvc;
using APIWithControllers.Data;

namespace APIWithControllers.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        return Ok("Login realizado com sucesso");
    }
    
}