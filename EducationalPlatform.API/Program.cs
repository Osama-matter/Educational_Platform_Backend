using EducationalPlatform.Application.DTOs.Courses;
using EducationalPlatform.Application.Interfaces;
using EducationalPlatform.Application.Interfaces.Security;
using EducationalPlatform.Infrastructure;
using EducationalPlatform.Infrastructure.Data;
using EducationalPlatform.Infrastructure.Security;
using EducationalPlatform.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------- CORS --------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// -------------------- Controllers --------------------
builder.Services.AddControllers();

// -------------------- Swagger --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Educational Platform API", Version = "v1" });

    // Add JWT Bearer support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token here. Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// -------------------- Dependency Injection --------------------
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddIdentityCore<EducationalPlatform.Domain.Entities.User>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
})
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<EducationalPlatform.Domain.Entities.User>>();

// -------------------- Database --------------------
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -------------------- Infrastructure --------------------
builder.Services.AddInfrastructure();

// -------------------- Authentication --------------------
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
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found."))
        )
    };
});

// -------------------- Build App --------------------
var app = builder.Build();

// -------------------- Middleware Pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication(); // Important: must be before Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
