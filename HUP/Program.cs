using HUP.Application.Services.Caching;
using HUP.Application.Services.Implementations;
using HUP.Application.Services.Interfaces;
using HUP.Common.Extensions;
using HUP.Core.Interfaces;
using HUP.Data;
using HUP.Repositories.Implementations;
using HUP.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using StackExchange.Redis;
using System.Text;

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

// ---
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<ICourseScheduleRepository, CourseScheduleRepository>();

builder.Services.AddScoped<IStudentAcademicService, StudentAcademicService>();
builder.Services.AddScoped<ICourseScheduleService, CourseScheduleService>();
builder.Services.AddScoped<IStudentService, StudentService>();
// ---

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
