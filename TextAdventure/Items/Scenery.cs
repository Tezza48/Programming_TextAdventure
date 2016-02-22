using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Scenery : Item
    {
        // an item that cannot be taken

        private string longDescription;

        public Scenery()
        {
            itemName = "";
            itemDescription = "";
            longDescription = "";
        }

        public Scenery (string name, string description, string lDescription)
        {
            itemName = name;
            itemDescription = description;
            longDescription = lDescription;
        }

    }
}
