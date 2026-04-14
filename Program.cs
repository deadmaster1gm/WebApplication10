using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using WebApplication10.Data;
using WebApplication10.Entities;
using WebApplication10.Infrastructure.Sorting;
using WebApplication10.Middleware;
using WebApplication10.Options;
using WebApplication10.Repositories;
using WebApplication10.Repositories.Interfaces;
using WebApplication10.Services;
using WebApplication10.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var seqUrl = builder.Configuration["Seq:Url"] ?? "http://seq:80";

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq(seqUrl)
            .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.SectionName));

var jwt = builder.Configuration
        .GetSection(JwtOptions.SectionName)
        .Get<JwtOptions>()!;

builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,

                ValidateAudience = true,
                ValidAudience = jwt.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
        });

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISortMap<User>, UserSortMap> ();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
