using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Braces.ApiServer.Hubs;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Braces.ApiServer
{
    public class Startup
    {
        public const string HOST_PORT = "5002";
        public const string CONTAINER_PORT = "69";

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSignalR();

            services.AddMvc( options => options.EnableEndpointRouting = false )
                .AddNewtonsoftJson()
                .SetCompatibilityVersion( CompatibilityVersion.Version_3_0 );

            services.AddControllers( options => options.EnableEndpointRouting = false )
                .AddNewtonsoftJson()
                .SetCompatibilityVersion( CompatibilityVersion.Version_3_0 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints( endpoints =>
            {
                endpoints.MapHub<TextEditorHub>( "/ws/text-editor" );
                endpoints.MapHub<GlobalHub>( "/ws/global" );
                endpoints.MapHub<MainWindowHub>( "/ws/main-window" );
                endpoints.MapHub<ConfigurationHub>( "/ws/configuration" );

                endpoints.MapControllerRoute( "default", "{controller=Home}/{action=Index}/{id?}" );
            } );

            app.Run( async req => await req.Response.WriteAsync( "The Braces ApiServer successfully started." ) );

            StartPluginHost();
        }

        private void StartPluginHost()
        {
            Console.WriteLine( "Starting the PluginHost..." );
            // Hardcoded for now.
            Process.Start("docker", $"run --rm --network=\"nat\" -p {HOST_PORT}:{CONTAINER_PORT} --name braces.plugin-host braces.plugin-host");
            //Process.Start( "C:\\Users\\jpedrone\\DEV\\Braces\\Braces.PluginHost\\bin\\Debug\\netcoreapp3.0\\Braces.PluginHost.exe" );
        }
    }
}
