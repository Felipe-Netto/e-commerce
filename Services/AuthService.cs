using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using e_commerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using e_commerce.Models.User;
using BCryptNet =  BCrypt.Net.BCrypt;

namespace e_commerce.Services;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public AuthService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string?> RegisterAsync(UserRegisterDTO dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return null;

        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = BCryptNet.HashPassword(dto.Password),
            Role = dto.Role,
            Phone = dto.Phone,
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var token = GenerateToken(user);

        return token;
    }

    public async Task<string?> LoginAsync(UserLoginDTO dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCryptNet.Verify(dto.Password, user.Password))
            return null;

        return GenerateToken(user);
    }
    
    private string GenerateToken(User user)
    {
        var jwt    = _configuration.GetSection("Jwt");
        var key    = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["key"]!));
        var creds  = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.UtcNow.AddMinutes(double.Parse(jwt["ExpiryInMinutes"]!));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer:             jwt["Issuer"],
            audience:           jwt["Audience"],
            claims:             claims,
            expires:            expiry,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
