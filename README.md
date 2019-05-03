# \{ Braces \}

The code editor where everything is a plugin.


## Note

This is a work in progress. It is not even an alpha version.


## Codebase Architecture

- Braces.ApiServer: The bridge between the plugins and the UI. .NET Core 3. SignalR + WebAPI Controller.
- Braces.PluginHost: The domain that loads and manages the plugins. It has a SignalR Client that communicates with the ApiServer.
- Braces.Core: The class library. .NET Core 3 (E.g.: Models, ExtensionSystem, etc.).
- Braces.UI.WPF: The frontend. WPF in .NET Core 3. Platform dependent code.
