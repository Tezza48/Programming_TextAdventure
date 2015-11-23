using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{    
	class Item
	{
        private string name;
        private string description;

        public string Name { get { return name; } }
        public string Description { get { return description; } }

        public Item()
		{
            name = "A strange item";
            description = "I do not know what this item is";
		}
        public Item ( string name )
        {
            this.name = name;
            description = "There is nothing interesting about this item.";
        }
        public Item ( string name, string description )
        {
            this.name = name;
            this.description = description;
        }

        public override string ToString()
        {
            return name;
        }

	}
}
