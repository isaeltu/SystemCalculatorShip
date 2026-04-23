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
builder.Services.AddOpenApi(); // Instead of AddSwaggerGen()

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
var connectionString =
    Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=(localdb)\\MSSQLLocalDB;Database=SystemCalculatorShipDb;Trusted_Connection=True;TrustServerCertificate=True;";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

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

// Ensure database schema is created/updated on startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  // Instead of UseSwagger() and UseSwaggerUI()
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");

// Add error handling middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
