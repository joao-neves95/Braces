// The main guidelines for a plugin.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Braces.Core.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace Braces.Core.ExtensionSystem
{
    public interface IPlugin : IExtension
    {
        List<string> FileTypes { get; }

        HubConnection Connection { get; set; }

        void Execute();
    }
}
