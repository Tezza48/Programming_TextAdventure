using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.LevelEditor;

namespace TextAdventure
{
    class Key : Item
    {
        /*
            A key must:
                know if it's destroyed on use
        */
        private bool isDestroyedOnUse;
        public bool IsDestroyedOnUse { get { return isDestroyedOnUse; } set { isDestroyedOnUse = value; } }

        public Key()
        {
            itemName = "";
            itemDescription = "";
            isDestroyedOnUse = true;
        }

        public Key(string name, bool _isDestroyedOnUse)
        {
            itemName = name;
            itemDescription = "";
            isDestroyedOnUse = _isDestroyedOnUse;
        }

        public Key(string name, string description, bool _isDestroyedOnUse)
        {
            itemName = name;
            itemDescription = description;
            isDestroyedOnUse = _isDestroyedOnUse;
        }

        public static explicit operator Key(LevelEditor.Key v)
        {
            return new Key(v.KeyName, v.KeyDescription, v.DestroyedOnUse);
        }
    }
}
