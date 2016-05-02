using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.LevelEditor;

namespace TextAdventure
{
    class Location
    {
        private string roomTitle;
        private string roomDescription;
        private List<Exit> exits;
		private List<Item> inventory; // the room's inventory

        public Location()
        {
            // Blank out the title and description at start
			roomTitle = "";
			roomDescription = "";
			exits = new List<Exit>();
			inventory = new List<Item>();
        }

		public Location(string title)
		{
			roomTitle = title;
			roomDescription = "";
			exits = new List<Exit>();
			inventory = new List<Item>();
		}

		public Location(string title, string description)
		{
			roomTitle = title;
			roomDescription = description;
			exits = new List<Exit>();
			inventory = new List<Item>();
		}

		public override string ToString()
		{
			return roomTitle;
		}

		public void addExit(Exit exit)
		{
			exits.Add(exit);
		}

		public void removeExit(Exit exit)
		{
			if (exits.Contains(exit))
			{
				exits.Remove(exit);
			}
		}

		public List<Exit> getExits()
		{
			return new List<Exit>(exits);
		}

        public List<Item> getInventory()
		{
			return new List<Item>(inventory);
		}

		public void addItem(Item itemToAdd)
		{
            // TODO add to a sorted position
			inventory.Add(itemToAdd);
		}

		public void removeItem(Item itemToRemove)
		{
			if ( inventory.Contains(itemToRemove) )
			{
				inventory.Remove(itemToRemove);
			}
		}

		public string getTitle()
		{
			return roomTitle;
		}

		public void setTitle(string title)
		{
			roomTitle = title;
		}

		public string getDescription()
		{
			return roomDescription;
		}

		public void setDescription(string description)
		{
			roomDescription = description;
		}

        public static explicit operator Location(LevelEditor.Location v)
        {
            Location newLoc = new Location(v.Title, v.Description);
            List<Item> items = new List<Item>();
            List<Key> keys = new List<Key>();
            foreach (LevelEditor.Item item in v.Inventory)
            {
                LevelEditor.Key iKey = item as LevelEditor.Key;
                if (iKey == null)
                    items.Add((Item)item);
                else
                    keys.Add((Key)iKey);
            }
            newLoc.inventory.AddRange(items);
            newLoc.inventory.AddRange(keys);
            for (int i = 0; i < v.Exits.Length; i++)
            {
                if (v.Exits[i] != null)
                {
                    newLoc.addExit((Exit)v.Exits[i]);
                }
            }
            return newLoc;
        }
    }
}
