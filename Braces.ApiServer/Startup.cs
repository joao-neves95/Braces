using System;
using System.IO;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Braces.ApiServer.Hubs;
using Microsoft.Extensions.Hosting;

namespace Braces.ApiServer
{
    public class Startup
    {
        /// <summary>
        /// The port from which the host connects to the Docker container service.
        /// </summary>
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
            services.AddSignalR()
                .AddNewtonsoftJsonProtocol();

            services.AddMvc( options => options.EnableEndpointRouting = false )
                .AddNewtonsoftJson()
                .SetCompatibilityVersion( CompatibilityVersion.Version_3_0 );

            services.AddControllers( options => options.EnableEndpointRouting = false )
                .AddNewtonsoftJson()
                .SetCompatibilityVersion( CompatibilityVersion.Version_3_0 );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime )
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

            applicationLifetime.ApplicationStopping.Register( this.StopPluginHost );
            applicationLifetime.ApplicationStopped.Register( this.StopPluginHost );

            StartPluginHost();
        }

        private void StartPluginHost()
        {
            Console.WriteLine( "Starting the PluginHost..." );
            Process.Start("docker", $"run --rm -it --sig-proxy=false -p {HOST_PORT}:{CONTAINER_PORT} --name braces.plugin-host braces.plugin-host");
        }

        private void StopPluginHost()
        {
            Console.WriteLine( "Stopping the PluginHost..." );
            Process.Start("docker", "stop braces.plugin-host");
        }
    }
}
