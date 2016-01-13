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
        private bool isTakeable = true;

        public bool IsTakeable
        {
            get
            {
                return isTakeable;
            }

            set
            {
                isTakeable = value;
            }
        }

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
