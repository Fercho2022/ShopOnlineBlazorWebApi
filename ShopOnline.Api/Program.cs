using MercadoPago.Client.CardToken;
using MercadoPago.Client.Customer;
using MercadoPago.Client.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ShopOnline.Api.Data;
using ShopOnline.Api.Interfaces;
using ShopOnline.Api.Repository;



var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();


// Registro de servicios de MercadoPago

//Entity Framework Core usa inyección de dependencias para configurar y manejar la
//instancia de DataContext. Cuando configuras el contexto en tu aplicación, usualmente
//lo haces en el archivo Startup.cs o en el program principal. Al usar este patrón de
//inyección de dependencias, necesitas un constructor que acepte DbContextOptions, ya
//que este es el mecanismo que usa EF Core para inyectar la configuración necesaria en
//el contexto.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:7257", "https://localhost:7257")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
