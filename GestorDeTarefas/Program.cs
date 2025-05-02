using FluentValidation;
using GestorDeTarefas.Application.Interfaces;
using GestorDeTarefas.Application.Mappings;
using GestorDeTarefas.Application.Services.Tarefas;
using GestorDeTarefas.Application.Services.Tarefas.Queries.Handlers;
using GestorDeTarefas.Application.Validators;
using GestorDeTarefas.Domain.Interfaces;
using GestorDeTarefas.Infrastructure.Context;
using GestorDeTarefas.Infrastructure.Data;
using GestorDeTarefas.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Registro do MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(GetAllTarefasQueryHandler).Assembly));

//Registro do DbContext
builder.Services.AddDbContext<TarefaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

//Registro do UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<ITarefaService, TarefaService>();
builder.Services.AddAutoMapper(typeof(TarefaProfile));
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<TarefaValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

