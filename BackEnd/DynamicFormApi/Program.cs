using DynamicFormApi.Api;
using DynamicFormApi.Aplication.Services;
using DynamicFormApi.Application.Services;
using DynamicFormApi.Domain.Interfaces;
using DynamicFormApi.Infrastructure;
using DynamicFormApi.Infrastructure.Data;
using DynamicFormApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IFormRepository, FormRepository>();
builder.Services.AddScoped<FormService>();
builder.Services.AddScoped<IFormResponseRepository, FormResponseRepository>();
builder.Services.AddScoped<FormResponseService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", builder =>
    {
        builder.WithOrigins("http://localhost:5173")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dynamic Form API", Version = "v1" });
});


var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            context.Response.ContentType = "application/json";
            if (exception is ArgumentException argEx)
            {
                context.Response.StatusCode = exception.Message.Contains("does not exist") ? 404 : 400;
                await context.Response.WriteAsync($@"{{""error"": ""{argEx.Message}""}}");
            }
            else
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($@"{{""error"": ""An unexpected error occurred""}}");
            }
        }
    });
});

app.UseCors("AllowReactApp");
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dynamic Form API v1"));

app.MapFormEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var service = scope.ServiceProvider.GetRequiredService<FormService>();
    db.Database.EnsureCreated();
    await DataSeeder.SeedInitialDataAsync(service);
}

app.Run();
