# A toolkit comprising of many tools to help you build unity games faster

## BEWARE: Will not work without the Odin Inspector and Odin Validator
Third Party Dependencies:
- Odin Inspector and Validator (Not included, Needs to be purchased seperately)
- uLayout (Included as a submodule)

## Noteworthy Features

- Saving System
- Settings System
- Legacy UI Enhancements
- 3D Player based on the CC with a State Machine

### Others
- Debugging
  - Enhanced Debugging
  - Info Collector (Can be used to display real time debug information on the screen)
- Class Extension System
- Key System for things such as Localization Keys
- Base Class for Singleton Managers
- Pause System
- Achievement System
- Unlockables

- Helpers
  - State Machine
  - Utilities
  - Override Helpers
  - Input Helpers
  
- Game Mechanic Helpers:
  - Interaction System
    - Door System (Deprecated)
    - Pick up System (Deprecated)
  - Dialogue System

### Settings System

The Settings System is composed of 3 main components

- The settings manager
- A settings config
- and a setting instance

The config defines a key and what kind of range the value of the setting can have. 

When the game is started, the settings manager creates setting_instance objects based on the config objects. These instance objects can then be communicated to and be used to set or read the current setting

### UI Components

#### UI Menu

A class used as a base to implement menus. A menu referrs to an object that holds UI and can be enabled and disabled. This system implements various sub systems such as Transitions between Menus and a Navigation Stack that keeps track of visited menus.

#### UI Selectable

A replacement for unity's button component. 

The reason for its existance are some issues with the existing system.
One issue being that if you use the animation system for the buttons, the same animation is used when you stop clicking a button with your key as well as when you use your gamepad to select Buttons (Before pressing them). The selectable system also implements a way for animations thats more modular and allows you to add custom animations more easily.

In addition to this a custom navigation system has been implemented that adds functionality such as selectable groups which allow you to implement menus where you can click on selectables and they stay selected until you select another selectable in the group.

#### UI Modifier/Interactive

A set of scripts that modify the ui that they are attached to such as Localizing Text, fitting a rect to Text. Custom modifiers can also be added.

#### UI Configs

A system to define values that can be overwritten by underlying children. An object can request a value and will use the one that sits on the object of the nearest plane. This can be used to define values that are valid for an entire menu or even multiple menus but not all of them
