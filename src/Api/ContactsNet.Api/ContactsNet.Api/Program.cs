using ContactsNet.Core;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IValidator>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCore(builder.Configuration);

var app = builder.Build();
app.UseCore();
app.MapControllers();
app.MapGet("/", () => "ContactsNet API!");


await app.RunAsync();