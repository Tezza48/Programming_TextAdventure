using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
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
}
