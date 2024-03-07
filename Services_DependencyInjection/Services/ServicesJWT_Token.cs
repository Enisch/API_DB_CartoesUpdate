using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_DependencyInjection.Services
{
    public static class ServicesJWT_Token
    {
        public static IServiceCollection JWTConfig(this  IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                //Default schemes for JWT_Token
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    //Parameter's
                    ValidIssuer = configuration["JwtTokenConfig:Issuer"],
                    ValidAudience = configuration["JwtTokenConfig:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenConfig:SecretKey"]!)),
                    ClockSkew = TimeSpan.Zero,

                    //Configuration for the parameters
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false
   
                };

            });
            
            return services;
        }
 
    }
}
