using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopOnline.Web;
using ShopOnline.Web.Services.Contracts;
using ShopOnline.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5259") });

await builder.Build().RunAsync();
