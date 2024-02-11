using ArtBbaseFrontend;
using ArtBbaseFrontend.Services.Art;
using ArtBbaseFrontend.Services.Auth;
using ArtBbaseFrontend.Services.AuthProvider;
using ArtBbaseFrontend.Services.Bid;
using ArtBbaseFrontend.Services.Category;
using ArtBbaseFrontend.Services.Coupon;
using ArtBbaseFrontend.Services.Order;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ArtBbaseFrontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            //Register localStaorage
            builder.Services.AddBlazoredLocalStorage();

            //Services
            builder.Services.AddScoped<IAuth, AuthService>();

            //Configuration for AuthProvider
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvideService>();


            ///Services
            ///
            builder.Services.AddScoped<IAuth, AuthService>();
            builder.Services.AddScoped<IArt, ArtService>();
            builder.Services.AddScoped<ICategory, CategoryService>();
            builder.Services.AddScoped<ICoupon, CouponService>();
            builder.Services.AddScoped<IBid, BidService>();
            builder.Services.AddScoped<IOrder, OrderService>();


            //Configuration for AuthProvider
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvideService>();


            await builder.Build().RunAsync();
        }
    }
}
