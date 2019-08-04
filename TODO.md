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
    - Braces Extensions to VSCode API (use Braces extensions on VSCode)
    - VSCode Extensions to Braces API (use VSCode extensions on any Braces UI)
    - Vim plugins to Braces API (use Vim plugins on any Braces UI) - (???)

- Move all hardcoded path strings to a resources file.

- Remove unnecessary references and target frameworks.

- Use interfaces as method parameters instead of object.

- Automate docker install (if it is not already installed) and the braces.plugin-host docker container build.

- Refactor all the XAML (Braces.UI.WPF).

- Refactor all the UI C# code (Braces.UI.WPF).

- Add a more strict UI API enforcement (and for multiple languages).

- Add try/catches and error handling (to the UI and ApiServer).

- Add support for having projects (workspaces): custom configuration.

- Add support for plugins written in multiple languages (e.g.: Node.js, Java).

- Add support (API) for using plugins from other editors (Eg: VSCode, Atom, etc.).

- Add support (API) for using Braces plugins in other editors (Eg: VSCode, Atom, etc.).
