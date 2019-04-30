using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Braces.Core.Enums;
using Microsoft.AspNetCore.SignalR.Client;

namespace Braces.Core.ExtensionSystem
{
    public abstract class ComponentPlugin : IPlugin
    {
        #region INTERFACE CONTENTS

        public abstract List<string> FileTypes { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string[] Authors { get; }
        public abstract string Version { get; }
        public abstract ExtensionType ExtensionType { get; }

        public string Path => throw new NotImplementedException();

        public abstract HubConnection Connection { get; set; }

        public abstract void Execute();

        #endregion

        public virtual async Task AddComponentToWindow() { }
    }
}
