using ArtBbaseFrontend;
using ArtBbaseFrontend.Services.Auth;
using ArtBbaseFrontend.Services.AuthProvider;
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


            await builder.Build().RunAsync();
        }
    }
}
