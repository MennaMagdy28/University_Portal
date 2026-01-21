//using HUP.API.Data;
using HUP.Application.Services.Caching;
using HUP.Common.Extensions;
using HUP.Common.Helpers;
using HUP.Core.Entities.Identity;
using HUP.Core.Interfaces;
using HUP.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Text;

// Add configuration
var builder = WebApplication.CreateBuilder(args);

// Add services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

builder.Services.AddDbContext<HupDbContext>(options =>
    options.UseSqlServer(connectionString)
);
// redis connection
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
//cache service (singleton)
builder.Services.AddSingleton<ICacheService, CacheService>();

// This registers Hasher, UserManager, SignInManager, etc.
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>(); 
//tracks all services and repositories (DI)
builder.Services.AddApplicationServices();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(); 

builder.Services.AddSwaggerWithAuth();
//builder.Services.AddSwaggerGen(); 
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
    logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HUP API v1");
        c.RoutePrefix = "swagger"; 
        c.DocumentTitle = "HUP University Portal API";
        c.DefaultModelsExpandDepth(-1); 
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
    });
}
app.UseStaticFiles();

app.UseCors("AllowReactApp");

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var context = services.GetRequiredService<HupDbContext>();
//        var passwordHasher = services.GetRequiredService<IPasswordHasher<User>>();
//        var logger = services.GetRequiredService<ILogger<Program>>();

//        logger.LogInformation("Starting database migration and seeding");

//        await context.Database.MigrateAsync();
//        logger.LogInformation("Database migrations applied successfully");

//        await DataSeeder.SeedAsync(context, passwordHasher, logger);

//        logger.LogInformation("Application startup completed successfully");
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred while migrating or seeding the database");

//        if (app.Environment.IsDevelopment())
//        {
//            throw;
//        }
//    }
//}

app.Run();
