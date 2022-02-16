global using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Client;
using Client.Services;
using Serilog.Extensions.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IHttpService, HttpService>();
builder.Services.AddScoped<IEntityService, EntityService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();

public partial class Program
{
    private static async Task DebugDelayAsync()
    {
# if DEBUG
        await Task.Delay(15000);
        Console.WriteLine("Launching in debug mode.");
# endif
    }
    public static async Task Main(string[] args)
    {
        await DebugDelayAsync();
    }
}
