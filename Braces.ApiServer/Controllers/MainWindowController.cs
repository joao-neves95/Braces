using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Windows;
using Braces.ApiServer.Hubs;
using Braces.Core.ExtensionSystem;
using Braces.Core.Enums;

namespace Braces.ApiServer.Controllers
{
    // (EXPERIMENT)
    // This is how Braces will call plugins (on way comunications like events).
    // Clients (plugins) will be devided into groups of file types.
    [Route("api/main-window")]
    [ApiController]
    public class MainWindowController : ControllerBase
    {
        private readonly IHubContext<MainWindowHub, Plugin> _hubContext;

        public MainWindowController(IHubContext<MainWindowHub, Plugin> hubContext)
        {
            this._hubContext = hubContext;
        }
    }
}