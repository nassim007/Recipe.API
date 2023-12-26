using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipe.API.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbContext") ?? throw new InvalidOperationException("Connection string 'MyDbContext' not found.")));

// Add services to the container.

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

app.UseHttpsRedirection();

app.UseAuthorization();

// Voeg seeding logica toe
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    SeedDataInitializer.Initialize(serviceProvider);
}

app.MapControllers();

app.Run();