using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
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

        public Multitool(string name, string description, List<Craftable> _itemsUsedOn)
        {
            itemName = name;
            itemDescription = description;
            itemsUsedOn = _itemsUsedOn;
        }
    }
}
