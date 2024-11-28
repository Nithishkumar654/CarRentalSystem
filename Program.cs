using System.Text;
using System;
using CarRental_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CarRental_System.Services;
using CarRental_System.Middlewares;
using CarRental_System.Repository;
using CarRentalSystem.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.

var jwtval = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtval["key"]);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); 

// Authentication
builder.Services.AddAuthentication(i =>
{
    i.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    i.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(i =>
{
    i.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtval["Issuer"],
        ValidAudience = jwtval["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Authorization
builder.Services.AddAuthorization(i =>
{
    i.AddPolicy("AdminOnly", j => j.RequireRole("Admin"));
    i.AddPolicy("All", j => j.RequireRole("User", "Admin"));
});

// Jwt Service
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarRentalService, CarRentalService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtValidationMiddleware>();

app.UseRoleMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
