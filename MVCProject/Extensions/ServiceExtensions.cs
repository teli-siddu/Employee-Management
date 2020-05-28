using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCProject.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient("EmpMGMTClient", x =>
             {
                 x.BaseAddress = new Uri("https://localhost:44320/");
             });

        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );

            });
        }

        public static void ConfigureSession(this IServiceCollection services) 
        {
            services.AddSession();
        }

    }
}
