using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    TODO:
    Sort the inventory
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
			Item rock = new Item("Rock", "It's a rock!");
            Item pen = new Item("Pen");
			l1.addItem(rock);
            l1.addItem(pen);

			Location l2 = new Location("End of hall", "You have reached the end of a long dark hallway. You can\nsee nowhere to go but back.");
			Item window = new Item("Window", "A small window, you might fit through if you can break the glass...");
            window.IsTakeable = false;
			l2.addItem(window);

			Location l3 = new Location("Small study", "This is a small and cluttered study, containing a desk covered with\npapers. Though they no doubt are of some importance,\nyou cannot read their writing");

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
                Look    x
                    showLocation
                Move    x
                Inventory   x
                Take    x
                    Location.takeItem
                Use
            */

            List<string> commandList = command.Split(' ').ToList();

            // split the string into a list of words

            if (command == "look")
            {
                showLocation();
            }
            else if (commandList[0].Equals("move") || commandList[0].Equals("walk"))
            {
                moveToLocation(commandList);
            }
            else if (command == "inventory")
            {
                showInventory();
            }
            else if (commandList[0].Equals("take"))
            {
                takeItem(commandList);
            }
            else if (commandList[0].Equals("use"))
            {
                Console.WriteLine("\nInvalid command, are you confused?\n");
            }
            else
            {
                Console.WriteLine("\nInvalid command, are you confused?\n");
            }
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
                            inventory.Add(currentLocation.takeItem(commandList[itemCount]));
                            isItem = true;
                            Console.WriteLine("You took the " + item.ToString() + ".");
                            break;
                        }
                    }
                    if (!isItem)
                    {
                        Console.WriteLine("\n\"" + commandList[itemCount] + "\" is not an item you can take.");
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
