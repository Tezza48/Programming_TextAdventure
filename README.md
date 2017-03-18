# Programming_TextAdventure
Text Adventure Game/Engine. Zork style input

## The Input Algorithm

This Engine is Created from a bare minimum framework provided for the Programming module I have tanken at University.
The focus of this is the Input handelling which implements Natural Language Parsing.
I designed the algorithm to Feel as intuitive as the input of Zork.

A full breakdown of the input algorithm is written in the __Assignment 1 Report__ file however the short explanation is as follows:

### Single Words

Single word commandsare found by the splitting process which is explaned below, which will assign a command enumeration. This enum is run through a Switch block that finds the correct function to run.

### Multiple Words

Scentences go through a more complex system however. The scentence is split into simgle words by removing spaces and punctuation, then all words that immediately removable are removed such as _on, at and my_, followed by removing splitter words like _and and the_ to find what words could be connected such as two part item names like _glass bottle_ and finding exit names and commands from set listsfor the next stage. We are left with only Commands, exits and items at this point.

Commands currently in the game with matching functions are `Undefined Help Inventory Look Move Take Use ERROR`.

The words left over are the checked against all exit names like _North, East, In, Up_, all item names either in the current room or in the player's inventory and then checking for strings in a commands dictionary which let us assign an enumeration for a command. The dictionary matching strings and command enums looks like this:

```
Dictionary<string, CommandType> VerbCommands = new Dictionary<string, CommandType>
{
    { "use", CommandType.Use }, { "take", CommandType.Take }, { "look", CommandType.Look },
    { "move", CommandType.Move }, { "walk", CommandType.Move }, { "go", CommandType.Move }, { "help", CommandType.Help }
};
```

As you can see some commands are specified multiple times like _Move_ for which _move, walk and go_ all mean the same thing. Finally, once we have these formatted commands, they run through a switch block which will attempt to run the functions for the specified command with the item names and exits found, failing this,  a message is written to the console saying that the command was no understood followed by a sarcastic comment.

## Level Editing

The project on branch [JSON-Story](https://github.com/Tezza48/Programming_TextAdventure/tree/JSON-Story) will load a story from a json file that can be created in my [Level Editor](https://github.com/Tezza48/Programming_LevelEditor).

## Additional

I made sure to keep the code well commented and presentable, You will see that algorithms and methods are also explaned in comments allongside the code.

