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
    - File Explorer.
    - Find and Replace.
    - IntelliSence.
    - Vim commands.
    - DarkTheme/LightTheme.

- Sandbox the plugins.

- Redo the XAML (UI).

- Add try/catches and error handling (to the UI).
