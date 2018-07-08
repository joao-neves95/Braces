using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Enums;
using System.Windows;

namespace Braces.Core.PluginSystem
{
    public interface IExtension
    {
        string Authors { get; }

        string Version { get;  }

        string Description { get; }

        ExtensionType ExtensionType { get;  }
    }
}
