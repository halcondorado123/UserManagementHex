using ApiMessage.Modules;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using CUN.DiploGrados.Services.WebApi.Modules.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMapper();
builder.Services.AddInjection(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioning();
builder.Services.AddSwaggerGen();
// Registrar el versionado de la API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api-Message", Version = "v1.0" });
    c.EnableAnnotations();
});


builder.Logging.ClearProviders();
builder.Logging.AddConsole(options =>
{
    options.FormatterName = "simple";
    options.IncludeScopes = true;
    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        // Configuración de Swagger
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api-Message v1");

        // Esto asegura que Swagger UI está en la raíz y no en /index.html
        c.RoutePrefix = string.Empty;
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Esto muestra detalles de los errores en el navegador
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { };