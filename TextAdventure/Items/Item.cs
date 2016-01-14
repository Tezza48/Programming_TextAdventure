using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{    
	class Item
	{
        // base class for the Item type
        // can still be used for a basic item
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
}
