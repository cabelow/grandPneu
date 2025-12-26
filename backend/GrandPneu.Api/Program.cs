using GrandPneu.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// =======================
// Load .env
// =======================
Env.Load();

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = $"Host={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_NAME")};Username={Env.GetString("DB_USER")};Password={Env.GetString("DB_PASS")}";
    options.UseNpgsql(connectionString);
});

// JWT
var jwtKey = Env.GetString("JWT_KEY") ?? throw new InvalidOperationException("JWT_KEY não encontrado");
var jwtIssuer = Env.GetString("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER não encontrado");
var jwtAudience = Env.GetString("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE não encontrado");

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// **Adicione autorização**
builder.Services.AddAuthorization();

var app = builder.Build();

// =======================
// Pipeline
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
