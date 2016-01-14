using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{    

	class Item
	{
        protected string itemName;
        protected string itemDescription;

        public string ItemDescription { get { return itemDescription; } }

        public Item()
		{
            itemName = "";
            itemDescription = "";
		}

        public Item(string name)
        {
            itemName = name;
            itemDescription = "";
        }

        public Item(string name, string description)
        {
            itemName = name;
            itemDescription = description;
        }

        public override string ToString()
        {
            return itemName.ToLower();
        }
    }

    class Tool : Item
    {
        /*
            A tool must:
                have an item it's used on
        */

        private Craftable itemUsedOn;

        public Tool()
        {
            itemName = "";
            itemDescription = "";
            itemUsedOn = null;
        }

        public Tool(string name, string description, Craftable _itemUsedOn)
        {
            itemName = name;
            itemDescription = description;
            itemUsedOn = _itemUsedOn;
        }
    }

    class Multitool : Tool
    {
        // can be used on multiple craftables
        private List<Craftable> itemsUsedOn;

        public Multitool()
        {
            itemName = "";
            itemDescription = "";
            itemsUsedOn = new List<Craftable>();
        }

        public Multitool(string name, string description)
        {
            itemName = name;
            itemDescription = description;
            itemsUsedOn = new List<Craftable>();
        }

        public Multitool (string name, string description, List<Craftable> _itemsUsedOn)
        {
            itemName = name;
            itemDescription = description;
            itemsUsedOn = _itemsUsedOn;
        }
    }

    class Craftable : Item
    {
        /*
            A craftable must:
                turn into another item when it's tool is used on it
        */
        private Item craftedItem;

        public Craftable()
        {
            itemName = "";
            itemDescription = "";
        }

        public Craftable(string name, string description, Item _craftedItem)
        {
            itemName = name;
            itemDescription = description;
            craftedItem = _craftedItem;
        }

        public Item Craft()
        {
            return craftedItem;
        }
    }

    class Key : Item
    {
        /*
            A key must:
                know if it's destroyed on use
        */

        private bool isDestroyedOnUse;

        public Key()
        {
            itemName = "";
            itemDescription = "";
            isDestroyedOnUse = true;
        }

        public Key(string name, string description, bool _isDestroyedOnUse)
        {
            itemName = name;
            itemDescription = description;
            isDestroyedOnUse = _isDestroyedOnUse;
        }
    }

    class Lock : Item
    {
        /*
            A losk must:
                have a key
                unlock a locked exit
                turn into another item when unlocked
                    (an unlocked version of it'self)
        */

        private Key key;
        private Item unlockedVersion;
        private LockedExit lockedExit;

        public Lock()
        {
            itemName = "";
            itemDescription = "";
            key = null;
            unlockedVersion = null;
            lockedExit = null;
        }

        public Lock(string name, string description, Key _key, Item _unlockedVersion, LockedExit _lockedExit)
        {
            itemName = name;
            itemDescription = description;
            key = _key;
            unlockedVersion = _unlockedVersion;
            lockedExit = _lockedExit;
        }
    }
}
