using BidService.Data;
using BidService.Extensions;
using BidService.Services;
using BidService.Services.IServices;
using Microsoft.EntityFrameworkCore;

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

//Set cors policy
builder.Services.AddCors(options => options.AddPolicy("bidpolicy", build =>
{
    //build.WithOrigins("https://localhost:7257");
    build.AllowAnyOrigin();
    build.AllowAnyHeader();
    build.AllowAnyMethod();
}));


//base url
builder.Services.AddHttpClient("Art", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:ArtService")));

//REG FOR DI
builder.Services.AddScoped<IBid,  BidsService>();
builder.Services.AddScoped<IArt, ArtsService>();


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

app.UseCors("bidpolicy");

app.MapControllers();

app.Run();
