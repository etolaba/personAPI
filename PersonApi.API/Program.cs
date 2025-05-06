using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Serilog;
using FluentValidation;
using PersonApi.Application.Commands.AddPerson;
using PersonApi.Domain.Interfaces;
using PersonApi.Infrastructure.Repositories;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using PersonApi.Application.Commands.RecordBirth;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.WriteTo.Console());

// Add services
builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddMediatR(typeof(AddPersonCommand).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<AddPersonCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RecordBirthCommandValidator>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddSingleton<IPersonRepository, JsonPersonRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.MapControllers();

app.Run();
