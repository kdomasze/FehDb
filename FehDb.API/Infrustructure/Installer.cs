using FehDb.API.Contexts;
using FehDb.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure
{
    public class Installer
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
               jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("Jwt").GetValue<string>("Secret"))),
                   ValidateIssuer = true,
                   ValidIssuer = configuration.GetSection("Jwt").GetValue<string>("Issuer"),
                   ValidateAudience = true,
                   ValidAudience = configuration.GetSection("Jwt").GetValue<string>("Audience"),
                   ValidateLifetime = true,
                   ClockSkew = TimeSpan.FromMinutes(5)
               };
            });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "FehDb API",
                    Version = "v1",
                    Description = "Database modeling all the characters and related data from Fire Emblem: Heroes."
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "FehDb.API.xml");
                c.IncludeXmlComments(xmlPath);
            });

            // Database
            services.AddDbContext<FehContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Main"));
            });

            // Services
            services.AddTransient<IWeaponService, WeaponService>();
        }
    }
}
