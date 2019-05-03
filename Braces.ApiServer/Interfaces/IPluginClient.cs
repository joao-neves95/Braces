using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braces.Core.ExtensionSystem;

namespace Braces.ApiServer.Interfaces
{
    public interface IPluginClient : IPlugin
    {
        Task OnFileOpen( object args );

        Task OnFileSave( object args );
    }
}
