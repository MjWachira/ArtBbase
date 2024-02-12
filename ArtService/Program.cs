
using ArtService.Data;
using ArtService.Extensions;
using ArtService.Services;
using ArtService.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace ArtService
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            //base url
            builder.Services.AddHttpClient("Category", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CategoryService")));
       
            //REG FOR DI
            builder.Services.AddScoped<IArt,ArtsService>();
            builder.Services.AddScoped<ICategory, CatService>();
            //Set cors policy
            builder.Services.AddCors(options => options.AddPolicy("artpolicy", build =>
            {
                //build.WithOrigins("https://localhost:7257");
                build.AllowAnyOrigin();
                build.AllowAnyHeader();
                build.AllowAnyMethod();
            }));

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

            app.UseCors("artpolicy");

            app.MapControllers();

            app.Run();
        }
    }
}
