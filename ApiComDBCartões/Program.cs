using ApiComDBCartões.Interfaces;
using ApiComDBCartões.Models;
using ApiComDBCartões.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEntityFrameworkMySql();//Não Faz nada atualmente


//Linhas de conexão
var conexão = builder.Configuration.GetConnectionString("DefaultString");
builder.Services.AddDbContext<Context>(Options => Options.UseMySql(conexão, ServerVersion.AutoDetect(conexão)));

//Linhas essenciais:
builder.Services.AddScoped<IDebito, DebitoRepositorio>();
builder.Services.AddScoped<ICredito, CreditoRepositorio>();
builder.Services.AddScoped<IVerificação_CadastroDeUsuario, Verificação_CadastroDeUsuarioRepositorio>();

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
