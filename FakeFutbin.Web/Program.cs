global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.Toast;
global using Radzen;
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
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IUserIdService, UserIdService>();
builder.Services.AddAuthorizationCore();

//Local Storage
builder.Services.AddBlazoredLocalStorage();

//Toast Notification
builder.Services.AddBlazoredToast();

//DI registrations
builder.Services.AddScoped<IManagePlayersLocalStorageService, ManagePlayersLocalStorageService>();
builder.Services.AddScoped<IManageUserPlayersLocalStorageService, ManageUserPlayersLocalStorageService>();
builder.Services.AddScoped<IManageUserLocalStorageService, ManageUserLocalStorageService>();

//Radzen DI registrations
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
