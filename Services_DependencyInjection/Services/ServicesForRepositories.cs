using InfraData.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories.Repositories;
using Microsoft.EntityFrameworkCore;
using ContextDb.ContextDB;
using Microsoft.Extensions.Configuration;
using InfraData.Domain.Dto_s____Mappings;
using Repositories.PasswordHashMethods;
using Repositories.LoginUser_Authorize;
using Repositories.JWT_TokenGenerator;

namespace Services_DependencyInjection.Services
{
    public static class ServicesForRepositories
    {
        public static IServiceCollection ServicesRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            //DbContext services
            var Conection = configuration.GetConnectionString("DefaultString");

            services.AddDbContext<Context_Db>(opt => opt.UseMySql(Conection, ServerVersion.AutoDetect(Conection)));

            /*Caso vá usar Migrations, 
            x => x.MigrationsAssembly(typeof(Context_Db).Assembly.FullName)));*/



            /*services.AddScoped<IUsuario, UsuarioRepository>();
               services.AddScoped<ICreditCard,UsuarioRepository>();
                services.AddScoped<IConta, UsuarioRepository>();

               Metodo funcional porém,no caso de uma classe herddar varias interfaces, utilize o método abaixo;
                           Link's de consulta: 
                           metodo usando AddScoped:
                                    ex:
                services.AddScoped<UsuarioRepository>();
                   services.AddScoped<IUsuario>(x=> x.GetRequiredService<UsuarioRepository>());
                       services.AddScoped<IConta>(x=> x.GetRequiredService<UsuarioRepository>());
                               services.AddScoped<ICreditCard>(x => x.GetRequiredService<UsuarioRepository>());
             https://stackoverflow.com/questions/62262906/net-core-addscoped-class-that-implements-many-interfaces-and-get-them-by-inte
                                       Metodo usando AddSingleton:
             https://andrewlock.net/how-to-register-a-service-with-multiple-interfaces-for-in-asp-net-core-di/
             */

            //Repositories

            services.AddScoped<UsuarioRepository>();
            services.AddScoped<IUsuario,UsuarioRepository>();
            services.AddScoped<IConta,UsuarioRepository>();
            services.AddScoped<ICreditCard,UsuarioRepository>();

            services.AddScoped<IPasswordUserHash, PasswordHash>();
            services.AddScoped<IDtoUser, DtoUserS_Repositorycs>();
            services.AddScoped<LoginAuthorize>();
            services.AddScoped<TokenGenerator_JWT>();
            //AutoMapper

            services.AddAutoMapper(typeof(AutoMapperDtoUser));
            /*
            var UR= provider.GetService<UsuarioRepository>();
            var Iu= provider.GetService<IUsuario>();
            var ICc= provider.GetService<ICreditCard>();
            var IC_D= provider.GetService<IConta>();*/

            


            return services;
        }
    }
}
