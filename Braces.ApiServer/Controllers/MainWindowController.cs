using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Windows;
using Braces.ApiServer.Hubs;
using Braces.Core.Enums;

namespace Braces.ApiServer.Controllers
{
    // (EXPERIMENT)
    // This is how Braces will call plugins (on way comunications like events).
    // Clients (plugins) will be devided into groups of file types.
    [Route("api/[controller]")]
    [ApiController]
    public class MainWindowController : ControllerBase
    {
        private readonly IHubContext<MainWindowHub> _hubContext;

        public MainWindowController(IHubContext<MainWindowHub> hubContext)
        {
            this._hubContext = hubContext;
        }

        public async Task FireEvent( string eventName, string[] fileTypeName, object sender, RoutedEventArgs e, object args )
        {
            if (fileTypeName[0] == FileTypeName.ALL)
                await this._hubContext.Clients.Groups( FileTypeName.ALL ).SendAsync( eventName );
            else
            {
                for (int i = 0; i < fileTypeName.Length; ++i)
                {
                    await this._hubContext.Clients.Groups( fileTypeName[i] ).SendAsync( eventName );
                }
            }
        }
    }
}