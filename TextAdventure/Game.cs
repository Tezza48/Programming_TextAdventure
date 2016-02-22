#define DEBUG_INPUT


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    enum CommandType
    {
        Undefined,
        Help,
        Inventory,
        Look,
        Move,
        Take,
        Use,
        ERROR
    }
	class Game
	{
        Location currentLocation;

		public bool isRunning = true;

		private List<Item> inventory; // the player's inventory

        //
        private List<string> splitterWords = new List<string> { "the", "with", "on", "and", "at", "room", "my", "is"};
        Dictionary<string, CommandType> VerbCommands = new Dictionary<string, CommandType>
            {
                { "use", CommandType.Use }, { "take", CommandType.Take }, { "look", CommandType.Look },
                { "move", CommandType.Move }, { "walk", CommandType.Move }, { "help", CommandType.Help }
            };

        public Game()
		{
            inventory = new List<Item>();

			Console.WriteLine("Welcome adventurer, prepare yourself for a fantastical journey into the unknown.");

            // build the "map"
            // add locations and items
            
			Location l1 = new Location("Entrance to hall", "You stand at the entrance of a long hallway. The hallways gets darker\nand darker, and you cannot see what lies beyond. To the east\nis an old oaken door, unlocked and beckoning.");
            Key rock = new Key("Rock", "It's a rock!", true);// smashes the window in l2y);
            l1.addItem(rock);

			Location l2 = new Location("End of hall", "You have reached the end of a long dark hallway. You can\nsee a window above a bookcase to your left.");

			Location l3 = new Location("Small study", "This is a small and cluttered study, containing a desk covered with\npapers. Though they no doubt are of some importance,\nyou cannot read their writing");

            Location l4 = new Location("Ledge", "A ledge high above the ground, leading round to the West wing.");

            // add exits
			l1.addExit(new Exit(Exit.Directions.North, l2, rock));
			l1.addExit(new Exit(Exit.Directions.East, l3));

			l2.addExit(new Exit(Exit.Directions.South, l1));

			l3.addExit(new Exit(Exit.Directions.West, l1));
            

             currentLocation = l1;

            // Location C1L1 = new Location("");

			showLocation();
		}


        // TODO: Implement the input handling algorithm.
		public void doAction(string command)
		{
            #region Plan
            // what do i want the user to input
            /*
                single commands
                    look at the current area
                    A single direction
                    looking in inventory
                complex commands
                    above commands including verbs and filler words
                    sentences for take commands
                    sentences for using items together
            */
            // What words will i need to look for?
            /*
                verbs: use, take, look, move, walk, help, inspect
                single words: any exit name, inventory, look
                "connector words" for splitting item names when crafting/using items together: with, and
                removable words: at, my, in, the, from
            */
            // what do i want the algorithm to "output"
            /*
                Enum for a command type
                    command type will dictate which method to use
                Array of strings
                    the various items/exits that may need to be used as paramiters for methods
                Look at the room
                Look at an item (in room or inventory)
                Change Location
                Look at inventory
                Take an item
                use an item on another item (key on lock)
            */
            // actual plan
            /*
                First:
                    Count the number of words
                        if 1,
                            check if it's
                                an exit
                                inventory
                                help
                                look
                        if more,
                            Look for a verb

            */
            #endregion

            CommandType currentCommand = CommandType.Undefined;

            List<string> commandList = splitCommands(command);

            #if DEBUG
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            for (int i = 0; i < commandList.Count(); i++)
            {
                Console.Write(commandList[i]);
                if (i + 1 != commandList.Count())
                {
                    Console.Write("\t|");
                }
            }
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
            #endif

            #region SingleCommands
            // parse single commands
            if (commandList.Count == 1)
            {
                // check for a direction
                foreach (Exit currentExit in currentLocation.getExits())
                {
                    if (currentExit.ToString().ToLower().Equals(command) || currentExit.getShortDirection().Equals(command))
                    {
                        moveToLocation(currentExit);
                        return;
                    }
                }

                // check for a single command verb
                switch (command)
                {
                    case "look":
                        showLocation();
                        return;
                    case "inventory":
                        showInventory();
                        return;
                    case "help":
                        displayHelp();
                        return;
                    default:
                        Console.WriteLine("I don't understand {0}, are you confused?\n", command);
                        return;
                }
            }
            #endregion

            #region ComplexCommands
            // parse complex commands
            else
            {
                // search through the verb dictionary to find a verb
                foreach (string currentVerb in VerbCommands.Keys)
                {
                    if (commandList.Contains(currentVerb))
                    {
                        // set the current verp type and remove it from the list.
                        currentCommand = VerbCommands[currentVerb];
                        commandList.Remove(currentVerb);
                        break;
                    }
                    else
                    {
                        currentCommand = CommandType.ERROR;
                    }
                }

                switch (currentCommand)
                {
                    case CommandType.Undefined:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Something went wrong in the code. Sorry.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case CommandType.Help:
                        displayHelp();
                        break;
                    case CommandType.Inventory:
                        inspectInventory(commandList, inventory);
                        break;
                    case CommandType.Look:
                        inspectInventory(commandList, currentLocation.getInventory());
                        inspectInventory(commandList, inventory);
                        break;
                    case CommandType.Move:
                        moveToLocation(commandList);
                        break;
                    case CommandType.Take:
                        takeItem(commandList);
                        break;
                    case CommandType.Use:
                        switch (commandList.Count)
                        {
                            case 2:
                                useKey(commandList);
                                break;
                            default:
                                break;
                        }
                        break;
                    case CommandType.ERROR:
                        Console.WriteLine("I don't understand that, maybe you could re-phrase it.");
                        break;
                }

            }
            #endregion
        }

        private List<string> splitCommands(string _command)
        {
            List<string> commandList = _command.Split(' ').ToList();
            foreach (string removable in splitterWords)
            {
                if (commandList.Contains(removable))
                {
                    commandList.RemoveAll(item => item == removable);
                }
            }
            return commandList;
        }

        #region CommandMethods
        // display the help message to the console
        private void displayHelp()
        {
            Console.WriteLine("You think you need help? I had to make this!\nThe cheek of some people!");
        }

        // write the discriptions of the specified items in the respective inventory in the console
        private void inspectInventory(List<string> items, List<Item> searchInventory)
        {
            // inspect an inventory and write the description of the item if found, else return false
            // check if that item is in your inventory.
            foreach (Item currentItem in searchInventory)
            {
                if (items.Contains(currentItem.ToString()))
                {
                    Console.WriteLine(currentItem.ItemDescription + "\n");
                    items.Remove(currentItem.ToString());
                }
            }
            if (items.Count() < 0)
            {
                foreach (string badItem in items)
                {
                    Console.Write("{0}, ", badItem);
                }
                Console.WriteLine("could not be taken.\n");
            }
        }

        // display the player's inventory in the console
        private void showInventory()
		{
			if ( inventory.Count > 0 )
			{
				Console.WriteLine("\nA quick look in your bag reveals the following:\n");

				foreach ( Item item in inventory )
				{
					Console.WriteLine(item.ItemName);
				}
			}
			else
			{
				Console.WriteLine("Your bag is empty.");
			}

			Console.WriteLine("");
		}

        // write the current location's title, description items and exits to the console
		public void showLocation()
		{
            #if DEBUG
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("This is a debug build.\nAnything in dark cyan is a debug message.");
            Console.ForegroundColor = ConsoleColor.White;
            #endif
            Console.WriteLine("\n" + currentLocation.getTitle() + "\n");
			Console.WriteLine(currentLocation.getDescription());

			if (currentLocation.getInventory().Count > 0)
			{
				Console.WriteLine("\nThe room contains the following:\n");

				for ( int i = 0; i < currentLocation.getInventory().Count; i++ )
				{
					Console.WriteLine(currentLocation.getInventory()[i].ItemName);
				}
			}
	
			Console.WriteLine("\nAvailable Exits: \n");

			foreach (Exit exit in currentLocation.getExits() )
			{
				Console.WriteLine(exit.getDirection());
			}

			Console.WriteLine();
		}

        // change to current location to another one
        private void moveToLocation(Exit targetExit)
        {
            // if that exit isn't locked
            if (targetExit.Key == null)
            {
                currentLocation = targetExit.getLeadsTo();
                showLocation();
            }
            else
            {
                Console.WriteLine("That exit is Locked");
            }
        }
        // find a location from the exit matching the given string and use the above method to move to that location
        private void moveToLocation(List<string> newLocation)
        {
            // make sure there was only one command left after formatting
            if (newLocation.Count == 1)
            {
                // check the location for the exit specified
                foreach (Exit currentExit in currentLocation.getExits())
                {
                    if (currentExit.ToString().ToLower().Equals(newLocation[0]) || currentExit.getShortDirection().Equals(newLocation[0]))
                    {
                        // change the loaction to the exit's location
                        moveToLocation(currentExit);
                        // return out of the method
                        return;
                    }
                }
                // This error is written only if the above cannot find an exit matching the command
                Console.WriteLine("I cant go {0} from here.", newLocation[0]);
            }
            else
            {
                Console.WriteLine("I cant be in two places at once!");
            }
        }

        // tries to take all items asked for and add them to the player's inventory
        private void takeItem(List<string> items)
        {
            bool isItem;
            foreach (string item in items)
            {
                isItem = false;
                foreach (Item inventoryItem in currentLocation.getInventory())
                {
                    // make sure it's not a scenery item
                    if (inventoryItem.GetType() != typeof(Scenery))
                    {
                        string itemString = inventoryItem.ToString();
                        if (item == itemString)
                        {
                            isItem = true;
                            inventory.Add(inventoryItem);
                            currentLocation.removeItem(inventoryItem);
                            Console.WriteLine("You took the {0}", inventoryItem.ItemName);
                        }
                    }
                }
                if (!isItem)
                {
                    Console.WriteLine("{0} isn't an item you can take.", item);
                }
            }
        }

        // unlock a preveously locked exit
        private void useKey(List<string> commandList)
        {
            // Input a list of strings
            // list should contain a item's name and an exit's name

            // find the item in the player's inventory
            // the lambda expression checks the first string in commandList against each item's ToString() value
            Key keyItem = inventory.Find(strName => strName.ToString().Equals(commandList[0])) as Key;
            // find the exit in the currentLocation
            Exit targetExit = findExit(commandList[1]);
            // ceck that both were found
            if (keyItem != null && targetExit != null)
            {
                // if the currentLocation's key item == the item found, unlock the exit.
                if (targetExit.Key.Equals(keyItem))
                {
                    targetExit.Key = null;
                    Console.WriteLine("You unlocked the {0} exit with the {1}!", targetExit.getDirection().ToString(), keyItem.ItemName);
                    if (keyItem.IsDestroyedOnUse)
                    {
                        inventory.Remove(keyItem);
                        Console.WriteLine("{0} was destroyed.", keyItem.ItemName);
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("You cant use that item like that!");
                }

            }
            // Unlocking
            // remove the key from the exit
            // if the key is destroyed on use, destroy it
        }

        // returns an exit matching the given string
        private Exit findExit(string exitName)
        {
            // the predicate alows me to find the exit with a string
            return currentLocation.getExits().Find(strExit => strExit.ToString().Equals(exitName) || strExit.getShortDirection().ToLower().Equals(exitName));
        }

        #endregion

        public void Update()
		{

			string currentCommand = Console.ReadLine().ToLower();

			// instantly check for a quit
			if (currentCommand == "quit" || currentCommand == "q")
			{
				isRunning = false;
				return;
			}
				
			// otherwise, process commands.
			doAction(currentCommand);
		}
	}
}
