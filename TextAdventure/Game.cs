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
			Location l1 = new Location("Entrance to hall", "You stand at the entrance of a long hallway. The hallways gets darker\nand darker, and you cannot see what lies beyond. To the east\nis an old oaken door, unlocked and beckoning.");
            Multitool pen = new Multitool("Pen","A pen with an intricate gold inlay", null);
			Key rock = new Key("Rock", "It's a rock!", true);// smashes the window in l2
            l1.addItem(pen);
			l1.addItem(rock);

			Location l2 = new Location("End of hall", "You have reached the end of a long dark hallway. You can\nsee a window above a bookcase to your left.");
            Item openWindow = new Item("Smashed Window", "A small, now opened window");
			Lock window = new Lock("Window", "A small, fragile window", "you smashed the glass and opened the window", rock, Exit.Directions.Up, openWindow);

			Location l3 = new Location("Small study", "This is a small and cluttered study, containing a desk covered with\npapers. Though they no doubt are of some importance,\nyou cannot read their writing");
            Item letter = new Item("Closed_Letter", "An old letter, closed with a wax seal");

            Location l4 = new Location("Ledge", "A ledge high above the ground, leading round to the West wing.");
            window.LockedLocation = l4;
			l2.addItem(window);

			l1.addExit(new Exit(Exit.Directions.North, l2));
			l1.addExit(new Exit(Exit.Directions.East, l3));

			l2.addExit(new Exit(Exit.Directions.South, l1));

			l3.addExit(new Exit(Exit.Directions.West, l1));

			currentLocation = l1;
			showLocation();
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
					Console.WriteLine(currentLocation.getInventory()[i].ToString());
				}
			}
	
			Console.WriteLine("\nAvailable Exits: \n");

			foreach (Exit exit in currentLocation.getExits() )
			{
				Console.WriteLine(exit.getDirection());
			}

			Console.WriteLine();
		}

        // TODO: Implement the input handling algorithm.
		public void doAction(string command)
		{
            /*
                Look        x
                Move        x
                Inventory   x
                Take        x
                Use         x

                
            */

            // split the string into a list of words
            List<string> commandList = command.Split(' ').ToList();

            if (commandList[0].Equals("look"))
            {
                if (commandList.Count() == 1)
                    showLocation();
                else if (commandList.Count() == 2)
                {
                    bool itemExists = inspectInventory(commandList[1], currentLocation.getInventory());
                    if (!itemExists)
                        Console.WriteLine("\nThere is no " + commandList[1] + "in this location.\n");
                }
            }
            else if (commandList[0].Equals("move") || commandList[0].Equals("walk"))
            {
                moveToLocation(commandList);
            }
            else if (commandList[0].Equals("inventory"))
            {
                if (commandList.Count() == 1)
                    showInventory();
                else if (commandList.Count() == 2)
                {
                    bool itemExists = inspectInventory(commandList[1], inventory);
                    if (!itemExists)
                        Console.WriteLine("\nThere is no " + commandList[1] + "in your inventory.\n");
                }
            }
            else if (commandList[0].Equals("take"))
            {
                takeItem(commandList);
            }
            else if (commandList[0].Equals("use"))
            {
                bool canUse;
                if (commandList.Count() == 3)
                {
                    canUse = useKey(commandList);
                    if(!canUse) Console.WriteLine("\nYou cant use \"" + commandList[1] + "\" on \"" + commandList[2] + "\"");
                }
                else
                {
                    Console.WriteLine("\nInvalid \"take\"command, are you confused?\n");
                }

            }
            else
            {
                Console.WriteLine("\nInvalid command, are you confused?\n");
            }
		}

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
                                        currentLocation.getInventory().Remove(currentLock);
                                        // add the lock's alternative item to the currentLocation's inventory
                                        currentLocation.addItem(unlock.Item5);
                                        // add an exit to the current location leading to the Lock's location
                                        currentLocation.addExit(new Exit(unlock.Item2, unlock.Item3));
                                        // display the lock's unlocking message
                                        Console.WriteLine(unlock.Item5);
                                        // return once unlocked
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
					Console.WriteLine(item.ToString());
				}
			}
			else
			{
				Console.WriteLine("Your bag is empty.");
			}

			Console.WriteLine("");
		}

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
