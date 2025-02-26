using DynamicFormApi.Api;
using DynamicFormApi.Aplication.Services;
using DynamicFormApi.Data;
using DynamicFormApi.Domain.Interfaces;
using DynamicFormApi.Infrastructure.Data;
using DynamicFormApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<FormService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Agregar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dynamic Form API", Version = "v1" });
});


var app = builder.Build();

app.UseCors("AllowReactApp");
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dynamic Form API v1"));
// Registrar endpoints
app.MapFormEndpoints();

// Datos iniciales
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var service = scope.ServiceProvider.GetRequiredService<FormService>();
    db.Database.EnsureCreated();

    await DataSeeder.SeedInitialDataAsync(service);
}

app.Run();
