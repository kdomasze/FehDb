using FehDb.API.Contexts;
using FehDb.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FehDb.API.Infrustructure
{
    public class Installer
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FehContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Main"));
            });

            services.AddTransient<IWeaponService, WeaponService>();
        }
    }
}
