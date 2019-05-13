# TODO

- Refactor the code base to only have the basic text editor as the Core. Everything else will be a plugin (file explorer; moving text);
  
  - Core Text Editor:
    - [Core] ExtensionSystem.
    - [Core] EventSystem (plugins can add their custom events that can be used by other plugins).
    - [Core] ConfigurationSystem (plugins can add their custom configurable configurations).
    - [Core] CommandSystem (also extensible; it will talk with the ExtensionSystem singleton).
    - [UI] Side Menu Slider Panels (Left and Bottom).
    - [UI] TabSystem for the text editors (UI).
    - [UI] Inline dropdown menu (for intelisence, snippets, etc.). Maybe it will also be a plugin.
    - [UI] Context Menu (on mouse hover/enter).
    - [UI] PopUps.

  - Built-in Extensions:
    - Commands (pop-up).
    - Terminal.
    - TaskList.
    - File Explorer.
    - Find and Replace.
    - IntelliSence.
    - Vim commands.
    - DarkTheme/LightTheme.

- Remove unnecessary references and target frameworks.

- Use interfaces as method parameters instead of object.

- Refactor all the XAML (Braces.UI.WPF).

- Refactor all the UI C# code (Braces.UI.WPF).

- Sandbox the plugins (Docker).

- Add a more strict UI API enforcement (and for multiple languages).

- Add try/catches and error handling (to the UI and ApiServer).

- Add support for having projects (workspaces): custom configuration.

- Add support for plugins written in multiple languages (e.g.: Node.js, Java).
