using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
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
}
