using System;
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
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_2 );
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

            app.UseSignalR( route =>
             {
                 // THIS IS ALL ONLY AN EXPERIENCE.
                 route.MapHub<GlobalHub>( "/ws/global" );
                 route.MapHub<MainWindowHub>( "/ws/main-window" );
                 route.MapHub<TextEditorHub>( "/ws/text-editor" );
                 route.MapHub<ConfigurationHub>( "/ws/configuration" );
             } );

            app.UseMvc();
            app.Run( async req => await req.Response.WriteAsync( "The Braces ApiServer successfully started." ) );

            StartPluginHost();
        }

        private void StartPluginHost()
        {
            Console.WriteLine( "Starting the PluginHost..." );
            // Hardcoded for now.
            Process.Start( "C:\\Users\\jpedrone\\DEV\\Braces\\Braces.PluginHost\\bin\\Debug\\net471\\Braces.PluginHost.exe" );
        }
    }
}
