using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using e_commerce.Data;
using e_commerce.Services;

var builder = WebApplication.CreateBuilder(args);

// 1) Registre o esquema de autenticação JWT logo após AddDbContext:
var jwtConfig = builder.Configuration.GetSection("Jwt");
var chave = Encoding.UTF8.GetBytes(jwtConfig["key"]!);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(chave)
        };
    });

// (Opcional) se precisar de autorização por [Authorize(Role = "...")]:
builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();  // <-- executa o JWT Bearer
app.UseAuthorization();   // <-- checa políticas de [Authorize]

app.MapControllers();

app.MapGet("/public", () => "Qualquer um pode acessar!");

app.MapGet("/private", (HttpContext http) =>
    {
        var userId = http.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
        return $"Você está autenticado! seu ID é:  {userId}";
    })
    .RequireAuthorization();

app.Run();