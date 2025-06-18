using Microsoft.EntityFrameworkCore;
using APIWithControllers.Data;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do controller
builder.Services.AddControllers();

// Configura o DbContext com PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Constrói o app
var app = builder.Build();

// Middleware padrão
app.UseHttpsRedirection();

app.MapControllers();

app.Run();