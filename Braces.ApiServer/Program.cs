using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Braces.ApiServer
{
    public class Program
    {
        public static void Main( string[] args )
        {
            Console.WriteLine( "Starting the ApiServer..." );
            CreateHostBuilder( args ).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder( args )
                .ConfigureWebHostDefaults( webBuilder =>
                {
                    webBuilder.UseUrls(new string[] { "http://0.0.0.0:5000/", "https://0.0.0.0:5001/", "http://localhost:5000/", "https://localhost:5001/" });
                    webBuilder.UseStartup<Startup>();
                } );
    }
}
