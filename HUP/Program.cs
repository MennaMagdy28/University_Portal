using System.Text;
using HUP.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using HUP.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;
using HUP.Core.Interfaces;
using HUP.Application.Services.Caching;
using HUP.Application.Services.Implementations;
using HUP.Application.Services.Interfaces;
using HUP.Common.Extensions;
using HUP.Repositories.Implementations;
using HUP.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis");


builder.Services.AddDbContext<HupDbContext>(options =>
    options.UseSqlServer(connectionString)
);
// redis connection
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));
//cache service (singleton)
builder.Services.AddSingleton<ICacheService, CacheService>();
//tracks all services and repositories (DI)
builder.Services.AddApplicationServices();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
