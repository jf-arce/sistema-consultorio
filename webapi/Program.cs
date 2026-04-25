using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Application.Interfaces;
using webapi.Application.Services;
using webapi.Data;
using webapi.Exceptions;
using webapi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// Inyección de dependencias para los servicios
builder.Services.AddScoped<IPacienteService, PacienteService>();

// Fluent Validation
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
        .SelectMany(x =>
        {
            if (x.Value == null) return [];

              return x.Value.Errors.Select(e => new
                {
                    field = x.Key,
                    message = e.ErrorMessage
                });
        });

        var ex = new CustomException(errors);

        return new BadRequestObjectResult(ex.ToResponse());
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
