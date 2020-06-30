using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace APIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
//.ConfigureLogging((hostingContext,logging)=> 
//{
//    logging.ClearProviders();
//    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//    logging.AddDebug();
//    logging.AddConsole();
//    logging.AddNLog();

//})
.ConfigureWebHostDefaults(webBuilder =>
{
    //webBuilder.UseKestrel();
    //webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
    //webBuilder.UseIISIntegration();
    webBuilder.UseStartup<Startup>();
});
        }
    }
}
