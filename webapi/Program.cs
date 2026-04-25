using Microsoft.EntityFrameworkCore;
using webapi.Application.Interfaces;
using webapi.Application.Services;
using webapi.Data;

var builder = WebApplication.CreateBuilder(args);

/* El webappapplication es una clase de .NET
que representa la aplicación web que se está construyendo. 
Es el punto de entrada para configurar y ejecutar la aplicación. 
A través de esta clase, puedes configurar servicios, middleware y otras opciones para tu aplicación web. 
En este caso, se está utilizando para configurar servicios como controladores, OpenAPI y la conexión a la base de datos, así como para definir el pipeline de solicitudes HTTP.
*/

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Inyección de dependencias para los servicios
builder.Services.AddScoped<IPacienteService, PacienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
