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
                 route.MapHub<GlobalHub>( "/global" );
                 route.MapHub<MainWindowHub>( "/main-window" );
                 route.MapHub<TextEditorHub>( "/text-editor" );
                 route.MapHub<ConfigurationHub>( "/configuration" );
             } );

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
