using HappyCompany.WebApp;
using HappyCompany.WebApp.Services;
using HappyCompany.WebApp.Services.Helper;
using HappyCompany.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7296/") });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWareHouseService, WareHouseService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAlertService, AlertService>();
await builder.Build().RunAsync();
