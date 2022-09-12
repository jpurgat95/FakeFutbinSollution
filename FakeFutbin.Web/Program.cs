using Blazored.LocalStorage;
using FakeFutbin.Web;
using FakeFutbin.Web.Services;
using FakeFutbin.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Local Host addres pasted
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7242") });
//DI registrations
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IScoutService, ScoutService>();

//Local Storage
builder.Services.AddBlazoredLocalStorage();

//DI registrations
builder.Services.AddScoped<IManagePlayersLocalStorageService, ManagePlayersLocalStorageService>();
builder.Services.AddScoped<IManageScoutPlayersLocalStorageService, ManageScoutPlayersLocalStorageService>();

await builder.Build().RunAsync();
