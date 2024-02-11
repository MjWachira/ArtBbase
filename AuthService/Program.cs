using AuthService.Data;
using AuthService.Extensions;
using AuthService.Models;
using AuthService.Services;
using AuthService.Services.IServices;
using AuthService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service for connectiong to DB

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

//configure Identity Framework

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();


//Set cors policy
builder.Services.AddCors(options => options.AddPolicy("policy", build =>
{
    //build.WithOrigins("https://localhost:7257");
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));
//Add Auto Mapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//our Services
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IJwt,JwtService>();

//configure JWTOptions Class
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.AddAuth();
builder.AddSwaggenGenExtension();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (!app.Environment.IsDevelopment())
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AUTH API");
        c.RoutePrefix = string.Empty;
    }
});

app.UseMigrations();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("policy");

app.MapControllers();

app.Run();
