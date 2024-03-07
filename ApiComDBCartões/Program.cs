using ApiComDBCart�es.Interfaces;
using ApiComDBCart�es.Models;
using ApiComDBCart�es.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEntityFrameworkMySql();//N�o Faz nada atualmente


//Linhas de conex�o
var conex�o = builder.Configuration.GetConnectionString("DefaultString");
builder.Services.AddDbContext<Context>(Options => Options.UseMySql(conex�o, ServerVersion.AutoDetect(conex�o)));

//Linhas essenciais:
builder.Services.AddScoped<IDebito, DebitoRepositorio>();
builder.Services.AddScoped<ICredito, CreditoRepositorio>();
builder.Services.AddScoped<IVerifica��o_CadastroDeUsuario, Verifica��o_CadastroDeUsuarioRepositorio>();

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

app.MapControllers();

app.Run();
