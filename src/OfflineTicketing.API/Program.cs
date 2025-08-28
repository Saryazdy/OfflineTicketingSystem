using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfflineTicketing.Application.Common.DependencyInjection;
using OfflineTicketing.Application.Common.Interfaces;
using OfflineTicketing.Application.Common.Mapper;
using OfflineTicketing.Application.Common.Behaviours;
using OfflineTicketing.Infrastructure.Persistence;
using OfflineTicketing.Infrastructure.Repositories;
using OfflineTicketing.Infrastructure.Services;
using System.Reflection;
using System.Text;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
// DbContext
builder.Services.AddInfrastructureServices(builder.Configuration);

// Repositories
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// MediatR + FluentValidation

// JWT Authentication
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// JWT Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Initialize(db);
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();