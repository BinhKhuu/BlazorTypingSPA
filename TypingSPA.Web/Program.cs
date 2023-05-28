using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TypingSPA.Web;
using TypingSPA.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Mud Blazor Setup
builder.Services.AddMudServices();

// Web Services
builder.Services.AddScoped<ThemeService>();
builder.Services.AddScoped<LocalStorageService>();

await builder.Build().RunAsync();
