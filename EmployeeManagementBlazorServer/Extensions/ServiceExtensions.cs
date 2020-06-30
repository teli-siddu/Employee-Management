using Contracts;
using EmployeeManagementBlazorServer.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository;
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

        public static void ConfigureDependenceyResolver(this IServiceCollection services) 
        {
          

        }

        public static void ConfigureServices(this IServiceCollection services) 
        {
            services.AddSingleton<IEmployeeService, EmployeeService>();
            services.AddSingleton<IDropdownsService, DropdownsService>();
            services.AddSingleton<IDepartmentsService, DepartmentsService>();
            services.AddSingleton<IRolesService, RolesService>();

        }

    }
}
