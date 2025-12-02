using HUP.Application.Services.Caching;
using HUP.Application.Services.Implementations;
using HUP.Application.Services.Interfaces;
using HUP.Common.Extensions;
using HUP.Core.Entities.Identity;
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

// ---
builder.Services.AddAutoMapper(typeof(Program));
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
//builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();
//builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
//builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
////builder.Services.AddScoped<IExamRepository, ExamRepository>();
//builder.Services.AddScoped<ICourseScheduleRepository, CourseScheduleRepository>();

////builder.Services.AddScoped<IStudentAcademicService, StudentAcademicService>();
//builder.Services.AddScoped<ICourseScheduleService, CourseScheduleService>();
//builder.Services.AddScoped<IStudentService, StudentService>();
//builder.Services.AddScoped<IExamService, ExamService>();
// ---

builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseCors("AllowReactApp");

//app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
