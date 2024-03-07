using Services_DependencyInjection.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Dependency Injection's From the folder Services_DependencyInjection;
builder.Services.ServicesRepositories(builder.Configuration);//DBContext too.
builder.Services.JWTConfig(builder.Configuration);//Classe que comporta as configurações do Token JWt

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SwaggerIndependency();//Gera um Swagger com o JWT Token

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
