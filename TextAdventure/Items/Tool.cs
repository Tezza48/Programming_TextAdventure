using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
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
}
