using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{    
	class Item
	{

        private string itemName;
        private string itemDescription;
        private bool isTakeable = false; // can this item be taken
        private Item itemUsedWith; 
        private Item alternateItem;
        private LockedExit pairedLockedExit = null; // locked exit this is paired to if this item can become an exit 

        public string ItemDescription { get { return itemDescription; } }

        public bool IsTakeable { get { return isTakeable; } set { isTakeable = value; } }

        internal Item ItemUsedWithName { get { return itemUsedWith; } set { itemUsedWith = value; } }

        internal Item AlternateItem { get { return alternateItem; } set { alternateItem = value; } }

        internal LockedExit PairedLockedExit { get { return pairedLockedExit; } set { pairedLockedExit = value; } }

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

        public Item(string name, bool takeable)
        {
            itemName = name;
            itemDescription = "";
            isTakeable = takeable;
        }

        public Item(string name, string description, bool takeable)
        {
            itemName = name;
            itemDescription = description;
            isTakeable = takeable;
        }

        public override string ToString()
        {
            return itemName.ToLower();
        }

        public void addUse()
        {

        }
    }
}
