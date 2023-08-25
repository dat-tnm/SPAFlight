using BookingFlightApp.Data;
using BookingFlightApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(options =>
{
    options.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7233"
    });

    options.CustomOperationIds(operation => operation.ActionDescriptor.RouteValues["action"] + operation.ActionDescriptor.RouteValues["controller"]);
});

builder.Services.AddDbContext<Entities>(options =>
{
    options.UseInMemoryDatabase("Flight");
}, ServiceLifetime.Singleton);

var app = builder.Build();

app.UseCors(builder => builder.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseSwagger().UseSwaggerUI();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();
var flights = new Flight[]
    {
                    new Flight(
                        Guid.NewGuid(),
                        "VNAirline",
                        (new Random()).Next(90, 500).ToString(),
                        new TimePlace("Hue", DateTime.Now),
                        new TimePlace("SaiGon", DateTime.Now),
                        40
                    ),
                    new Flight(
                        Guid.NewGuid(),
                        "VietJet",
                        (new Random()).Next(90, 500).ToString(),
                        new TimePlace("Hue", DateTime.Now),
                        new TimePlace("SaiGon", DateTime.Now),
                        20
                    ),
                    new Flight(
                        Guid.NewGuid(),
                        "Bamboo",
                        (new Random()).Next(90, 500).ToString(),
                        new TimePlace("Hue", DateTime.Now),
                        new TimePlace("SaiGon", DateTime.Now),
                        15
                    ),
    };
entities.Flights.AddRange(flights);
entities.SaveChanges();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
