using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIWithControllers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APIWithControllers.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string GenerateToken(User user)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]!);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<User?> ValidateUser(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        if (user == null)
            return null;

        // Aqui você deve implementar a verificação da senha
        // Por enquanto, vamos apenas comparar as strings
        // Em produção, você deve usar hash de senha
        if (user.Password != password)
            return null;

        return user;
    }

    public async Task<(bool success, string token)> Login(string email, string password)
    {
        var user = await ValidateUser(email, password);
        
        if (user == null)
            return (false, string.Empty);

        var token = GenerateToken(user);
        
        // Atualiza o token no banco de dados
        user.Token = token;
        await _context.SaveChangesAsync();

        return (true, token);
    }
}
