using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Braces.Core;

namespace Braces.ApiServer.Hubs
{
    public class ConfigurationHub : Hub
    {
        public string GetConfiguration()
        {
            return "config";
        }
    }
}
