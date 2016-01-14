using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Inventory
    {
        private List<Item> inventory;

        internal List<Item> getInventory
        {
            get { return inventory; }
        }

        public Inventory()
        {
            inventory = new List<Item>();
        }

        // methods
        // Add
        /*public void Add(Item newItem)
        {
            int position = inventory.Count() / 2;
            position = SortedAdd(position, newItem, inventory);

            inventory.Insert(position, newItem);

            // recursive sorted add method
            // half the list
            // is letter lower than the current index
            // Yes
            //      sorted add that half

        }*/

        /*private int SortedAdd(int position, Item newItem, List<Item> inventory)
        {
            string newItemName = newItem.ToString();
            string currentItemName = inventory[position].ToString();
            string nextItemName = inventory[position + 1].ToString();

            int compareCurrent = newItemName.CompareTo(currentItemName);
            int compareNext = newItemName.CompareTo(nextItemName);
            // return position if new item fits between the current items
            if (compareCurrent <= 0 && compareNext > 0)
            {
                return position;
            }
            // sort lower half if comparecurrent is negative
            else if (compareCurrent < 0)
            {

            }

        }*/

        // Count
        // 
    }
}
