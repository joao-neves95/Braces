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

- Refactor all the UI's XAML.

- Refactor all the TextEditor control code (C#).

---

For possible future reference:

https://github.com/damienbod/SignalRMessagingErrorHandling
https://code.msdn.microsoft.com/windowsdesktop/Using-SignalR-in-WinForms-f1ec847b
