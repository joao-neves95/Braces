# TODO

- Refactor the code base to only have the basic text editor as the Core. Everything else will be a plugin (file explorer; moving text);
  
  - Core Text Editor:
    - [Core] ExtensionSystem.
    - [Core] EventSystem (plugins can add custom events that can be used by other plugins).
    - [Core] ConfigurationSystem (plugins can add their custom configurable configurations).
    - [UI] Side Menu Slider Panels (Left and Bottom).
    - [UI] TabSystem (UI).
    - [UI] Inline dropdown menu (for intelisence, snippets, etc.). Maybe it will also be a plugin.
    - [UI] Context Menu (on mouse hover/enter).
    - [UI] PopUps.

  - Built-in Extensions:
    - File Explorer.
    - Find and Replace.
    - Terminal.
    - IntelliSence.
    - Vim commands.
    - DarkTheme/LightTheme.
