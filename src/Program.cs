using System.Text;
using CodeBattles_Backend.Domain;
using CodeBattles_Backend.Domain.DTOs;
using CodeBattles_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CodeBattles_Backend;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var jwtKey = builder.Configuration["JwtConfig:Key"] ?? "";
        var jwtIssuer = builder.Configuration["JwtConfig:Issuer"];
        var jwtAudience = builder.Configuration["JwtConfig:Audience"];

        builder.Services.AddOpenApi();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("localhost", builder =>
            {
                builder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

        builder.Services.AddAuthorization();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();

        builder.Services.AddScoped<IPasswordHasher<UserJwtDTO>, PasswordHasher<UserJwtDTO>>();
        builder.Services.AddScoped<JWTService>();
        builder.Services.AddScoped<PasswordService>();
        builder.Services.AddScoped<AuthService>();

        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection"))
        );

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "My API v1");
            });
        }

        app.UseCors("localhost");

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapGet("/", () => "hello world");
        app.Run();
    }
}