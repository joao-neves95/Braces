using System;
using System.Collections.Generic;
using System.Text;
using Braces.Core.Enums;
using System.Windows;

namespace Braces.Core.ExtensionSystem
{
    public interface IExtension
    {
        string Name { get; }

        string Description { get; }

        string[] Authors { get; }

        string Version { get; }

        // TODO: Do this with another way to simplify.
        ExtensionType ExtensionType { get;  }
    }
}
