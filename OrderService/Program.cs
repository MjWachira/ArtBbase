using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Extensions;
using OrderService.Services;
using OrderService.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DB CONNECTION
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});

///FROM EXTENSIONS
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAuth();
builder.AddSwaggenGenExtension();

//
builder.Services.AddScoped<IArt, ArtsService>();
builder.Services.AddScoped<IBid, BidsService>();
builder.Services.AddScoped<IUser, UsersService>();
builder.Services.AddScoped<IOrder, OrdersService>();
builder.Services.AddScoped<ICoupon, CouponsService>();

//
builder.Services.AddHttpClient("User", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:AuthService")));
builder.Services.AddHttpClient("Coupon", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:CouponService")));
builder.Services.AddHttpClient("Art", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:ArtService")));
builder.Services.AddHttpClient("Bid", c => c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ServiceURl:BidService")));

var app = builder.Build();

//STRIPE
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:Key");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMigrations();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
