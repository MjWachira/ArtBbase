using CategoryService.Extensions;
using Microsoft.EntityFrameworkCore;
using CategoryService.Data;
using CategoryService.Services.IServices;
using CategoryService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//from extensions
builder.AddSwaggenGenExtension();
builder.AddAuth();

//db connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});
//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//DI
builder.Services.AddScoped<ICategory , CategoryServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMigrations();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
