using BlogPost.Server.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using BlogPost.Server.Interface.Interface;
using BlogPost.Server.Interface.Implimentaion;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger for API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure the DbContext with SQL Server.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextConnection"));
});

// Configure JWT Authentication.
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
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),
        ClockSkew = TimeSpan.Zero // Optional: Set clock skew to zero to ensure token expiration is accurate.
    };
});

// Add Redis caching.
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
    options.InstanceName = "RedisCache_";
});

// Add scoped dependency injection for repositories.
builder.Services.AddScoped<IcategoryReposetory, CategoryRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogpostRepository>();
builder.Services.AddScoped<IUserService, UserService>();


// Configure CORS to allow specific origins (for Angular app).
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Add configuration as a singleton.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Middleware for serving static files.
app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger configuration.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable authentication and authorization.
app.UseAuthentication();
app.UseAuthorization();

// Enable CORS.
app.UseCors("AllowAngularApp");

// Enable HTTPS redirection.
app.UseHttpsRedirection();

// Map controllers.
app.MapControllers();

// Fallback for Angular routing.
app.MapFallbackToFile("/index.html");

app.Run();
