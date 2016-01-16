using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Craftable : Item
    {
        // a craftable is an item that when used with the other items in it's recipie, creates another item
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
