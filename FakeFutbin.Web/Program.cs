global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.Toast;
global using Radzen;
global using CsvHelper;
global using FakeFutbin.Models.Dto;
global using FakeFutbin.Web.Services.Contracts;
global using Microsoft.AspNetCore.Components;
global using Blazored.Toast.Services;
global using System.Net.Http.Json;
using FakeFutbin.Web;
using FakeFutbin.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//Local Host addres pasted
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7242") });

//DI registrations
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPositionService, PositionService>();

//Local Storage
builder.Services.AddBlazoredLocalStorage();

//DI registrations
builder.Services.AddScoped<IUserIdService, UserIdService>();
builder.Services.AddScoped<IManagePlayersLocalStorageService, ManagePlayersLocalStorageService>();
builder.Services.AddScoped<IManageUserPlayersLocalStorageService, ManageUserPlayersLocalStorageService>();
builder.Services.AddScoped<IManageUserLocalStorageService, ManageUserLocalStorageService>();

//DI registrations
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

//Toast Notification
builder.Services.AddBlazoredToast();

//Radzen DI registrations
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();