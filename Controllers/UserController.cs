using Microsoft.AspNetCore.Mvc;
using APIWithControllers.Data;
using e_commerce.Models;

namespace e_commerce.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        // Validação básica (você pode melhorar depois)
        if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            return BadRequest("Email e senha são obrigatórios.");

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok(new { message = "Usuário registrado com sucesso", user.Id });
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return Ok("login");
    }
}