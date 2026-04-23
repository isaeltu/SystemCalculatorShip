
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SystemCalculatorShip.Infrastructure.Persistence.Contexts;
using SystemCalculatorShip.Application.Interfaces;
using SystemCalculatorShip.Application.Services;
using SystemCalculatorShip.Domain.Validators;
using SystemCalculatorShip.Infrastructure.Persistence.Repositories;
using SystemCalculatorShip.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS for Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=(localdb)\\mssqllocaldb;Database=SystemCalculatorShipDb;Trusted_Connection=true;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure JWT Authentication
var secretKey = "your-secret-key-min-32-chars-long!";
var key = Encoding.ASCII.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Register Domain Validators
builder.Services.AddScoped<IWeightValidator, WeightValidator>();
builder.Services.AddScoped<ICountryValidator, CountryValidator>();
builder.Services.AddScoped<ITariffTypeValidator, TariffTypeValidator>();

// Register Infrastructure Repositories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ITariffRepository, TariffRepository>();

// Register Application Services
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITariffCalculationService, TariffCalculationService>();
builder.Services.AddScoped<ICountryManagementService, CountryManagementService>();
builder.Services.AddScoped<ITariffManagementService, TariffManagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
