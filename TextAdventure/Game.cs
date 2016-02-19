using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    TODO:
    use a more sophisticated sorting algorithm for searching the inventory and adding items
    Sort the items in the Take Command
*/

namespace TextAdventure
{
    enum CommandType
    {
        Use,
        Take,
        Look,
        Move,
        Inventory,
        Help
    }
	class Game
	{
		Location currentLocation;

		public bool isRunning = true;

		private List<Item> inventory; // the player's inventory

		public Game()
		{
			inventory = new List<Item>();

			Console.WriteLine("Welcome adventurer, prepare yourself for a fantastical journey into the unknown.");

            // build the "map"
            // add locations and items
            
			Location l1 = new Location("Entrance to hall", "You stand at the entrance of a long hallway. The hallways gets darker\nand darker, and you cannot see what lies beyond. To the east\nis an old oaken door, unlocked and beckoning.");
            Key rock = new Key("Rock", "It's a rock!", true);// smashes the window in l2
            Craftable mummy = new Craftable("Mummy", "");
            Item daddy = new Item("Daddy");
            Item baby = new Item("Baby");
            List<Item> daddyList = new List<Item>();
            daddyList.Add(daddy);
            Recipe business = new Recipe(daddyList, baby);
            mummy.Recipie = business;
            l1.addItem(rock);
            l1.addItem(mummy);
            l1.addItem(daddy);

			Location l2 = new Location("End of hall", "You have reached the end of a long dark hallway. You can\nsee a window above a bookcase to your left.");
            Item openWindow = new Item("Smashed_Window", "A small, now opened window");
			Lock window = new Lock("Window", "A small, fragile window", "you smashed the glass and opened the window", rock, Exit.Directions.Up, openWindow);

			Location l3 = new Location("Small study", "This is a small and cluttered study, containing a desk covered with\npapers. Though they no doubt are of some importance,\nyou cannot read their writing");
            Item letter = new Item("Closed_Letter", "An old letter, closed with a wax seal");

            Location l4 = new Location("Ledge", "A ledge high above the ground, leading round to the West wing.");
            window.LockedLocation = l4;
			l2.addItem(window);

            // add exits
			l1.addExit(new Exit(Exit.Directions.North, l2));
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
            Dictionary<string, CommandType> VerbCommands = new Dictionary<string, CommandType>
            {
                { "use", CommandType.Use },
                { "take", CommandType.Take },
                { "look", CommandType.Look },
                { "move", CommandType.Move },
                { "walk", CommandType.Move }
            };
            string[] wordList = command.Split(' ');
        }

        #region CommandMethods
        /* private void craftItem(List<string> commandList)
         {
             // search the player's inventory for the items and make a temporary list of these items
             List<Item> givenItems = new List<Item>();
             bool validItems = true;
             foreach (string givenItem in commandList)
             {
                 bool found = false;
                 foreach (Item currentItem in inventory)
                 {
                     if (currentItem.ToString() == givenItem)
                     {
                         found = true;
                         givenItems.Add(currentItem);
                         continue;
                     }
                 }
                 if (!found)
                 {
                     Console.WriteLine("\nYou don't have a \"" + givenItem + "\" in your bag");
                     validItems = false;
                     break;
                 }
             }
             if (validItems)
             {
                 // remove the craftable from the list and give it a separate variable
                 Craftable craftable = null;
                 foreach (Item item in givenItems)
                 {
                     craftable = item as Craftable;
                     if (craftable != null)
                     {
                         givenItems.Remove(craftable);
                         break;
                     }
                 }
                 // if none of the items is a craftable display an error message and stop
                 if (craftable != null)
                 {
                     // use the craft method of the craftable and pass it the other items left in the list
                     Item product = craftable.Craft(givenItems);
                     // if it returns null the give an error message and stop the operation
                     if (product != null)
                     {
                         // remove the items in the temporary list and the craftable from the inventory
                         foreach (Item item in givenItems)
                             inventory.Remove(item);
                         inventory.Remove(craftable);
                         // add the returned item to the player's inventory
                         inventory.Add(product);
                         Console.WriteLine("\nYou created: " + product.ToString() + "!");
                     }
                     else
                     {
                         Console.WriteLine("\nThese Items don't work together.");
                     }
                 }
                 else
                 {
                     Console.WriteLine("\nThese Items don't work together.");
                 }
             }
        }*/

        private bool useKey(List<string> commandList)
        {
            // find the lock in the currentLocation corresponding with the third command
            foreach (Item currentLockItem in currentLocation.getInventory())
            {
                Lock currentLock = currentLockItem as Lock;
                if (currentLock != null)
                {
                    if (currentLock.ToString() == commandList[2])// this lock is the one specified
                    {
                        // if it's key is in the player's inventory
                        foreach (Item currentKeyItem in inventory)
                        {
                            Key currentKey = currentKeyItem as Key;
                            if (currentKey != null)
                            {
                                if (currentKey.ToString() == commandList[1])
                                {
                                    Tuple<bool, Exit.Directions, Location, string, Item> unlock = currentLock.Unlock(currentKey);
                                    if (unlock.Item1)
                                    {
                                        // remove the lock from the currentLocation's inventory
                                        currentLocation.removeItem(currentLock);
                                        // add the lock's alternative item to the currentLocation's inventory
                                        currentLocation.addItem(unlock.Item5);
                                        // add an exit to the current location leading to the Lock's location
                                        currentLocation.addExit(new Exit(unlock.Item2, unlock.Item3));
                                        // display the lock's unlocking message
                                        Console.WriteLine(unlock.Item5);
                                        Console.WriteLine();
                                        // return once unlocked
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        
        private bool inspectInventory(string itemName, List<Item> searchInventory)
        {
            // inspect an inventory and write the description of the item if found, else return false
            // check if that item is in your inventory.
            foreach (Item currentItem in searchInventory)
            {
                if (currentItem.ToString().Equals(itemName))
                {
                    // write the item's description to the output stream
                    Console.WriteLine(currentItem.ItemDescription + "\n");
                    return true;
                }
            }
            return false;
        }

        private void moveToLocation(List<string> commandList)
        {
            // check if it contains one more word
            if (commandList.Count == 2)
            {
                // check the second word aginst all other exits
                foreach (Exit exit in currentLocation.getExits())
                {
                    bool longCommand = commandList[1].Equals(exit.ToString().ToLower());
                    bool shortCommand = commandList[1].Equals(exit.getShortDirection());
                    if (longCommand || shortCommand)
                    {
                        // go to that location
                        currentLocation = exit.getLeadsTo();
                        showLocation();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\n\"" + commandList[1] + "\" Is not a valid direction. \nare you confused?\n");
                    }
                }
            }
        }
        
        private void takeItem(List<string> commandList)
        {
            // try to take all commands after take
            for (int itemCount = 1; itemCount < commandList.Count; itemCount++)
            {
                if (currentLocation.getInventory().Count() > 0)
                {
                    // is this command an item in the location
                    bool isItem = false;
                    // for each item specified after the command
                    foreach (Item item in currentLocation.getInventory())
                    {
                        // look through the inventory for that item
                        if (item.ToString().Equals(commandList[itemCount]))
                        {
                            if (item.GetType() != typeof(Lock))
                            {
                                // TODO add to a sorted position
                                inventory.Add(currentLocation.takeItem(commandList[itemCount]));
                                Console.WriteLine("You took the " + item.ToString() + ".");
                                isItem = true;
                                break;
                            }
                            else
                            {
                                isItem = true;
                                Console.WriteLine("You cant take the " + item.ToString() + ".");
                            }
                        }
                    }
                    if (!isItem)
                    {
                        Console.WriteLine("\"" + commandList[itemCount] + "\" is not an item you can take.");
                    }
                }
                else
                {
                    Console.WriteLine("\nThere are no items here.");
                }
                Console.WriteLine();
            }
        }

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

		public void showLocation()
		{
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
