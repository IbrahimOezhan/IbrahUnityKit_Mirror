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

