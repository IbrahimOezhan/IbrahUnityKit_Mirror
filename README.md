# IbrahUnityKit

Source Code: [Gitlab](https://gitlab.com/ibrahim_oezhan/IbrahUnityKit)

### Dependencies

- <span style="color:red">Odin Inspector/Odin Validator: Not included! </span>

Unfortunately the Toolkit relies on Odin for specific Inspector related Attributes. I personally refuse to "vibe code" replacements for them as I don't want to have native replacements unless I myself understand them and preferably written them. This is why the code in the toolkit will not compile unless you have Odin Inspector as well as Odin Validator in your project. If you are capable and manage to code your own replacements or you know of existing, free, human-written libraries/plugins that could be used feel free to create a merge-request with the changes

- uLayout

The toolkit uses uLayout to compliment the UI System. As its open source and the license allows it, it is included as a git submodule in the project.

### Structure/ Sub Systems

Its composed of many different components and subsystems for doing different things. The components are structured in layers where each component can only use components from the layer below it.

The systems it contains are:

- Settings
- Saving
- User Interface
- Interaction
- Localization
- Dialogue
- Utilities
- Debugging
- Unlockables (with Achievements as Specialization)
- Player Controller (Based on the Character Controller)

### Installation



##### OpenUPM

##### Second Option: UPM Git URL



##### Third Option: Git Clone

Simply clone the repository and put it into your project or add it as a submodule

### History

IbrahUnityKit is  a toolkit for Unity that I have been working on for a long time now as I have been building my games.
The first version where similar already long refactored code system was implemented was actually [My Uncles Story](https://ibrahim-oezhan.itch.io/my-uncles-story). Back then I made a terrible Interaction System and some other code that I then thought I could reuse for my next projects. And so I did and with each project I improved it until its now in this state.

### AI Usage Disclosure

AI is used in an Assisting way, instead of what many call "vibe coding". I write the entire code and I never copy paste code from an output to the codebase. I have set the instructions so that if I ask about something programming related I let AI only answer in conceptual terms by hand but AI is used for the following things:

- When I forget the name of a method that does X,Y,Z I ask AI to remind me
- When I can't find the issue in my code I use AI as a help for debugging although the instructions are set in a way where it only gradually gives more revealing hints and starts with very simple ones first so it doesn't spoil the answer and I can find it out myself
- AI is also used to discuss design decisions and the pros and cons if I don't feel I know if option A or B is the right one
