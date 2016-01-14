using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Lock : Item
    {
        /*
            A losk must:
                have a key
                unlock a locked exit
                turn into another item when unlocked
                    (an unlocked version of it'self)
        */

        private Key key;
        private Item unlockedVersion;
        private Exit lockedExit; // exit that this lock blocks off

        public Lock()
        {
            itemName = "";
            itemDescription = "";
            key = null;
            unlockedVersion = null;
            lockedExit = null;
        }

        public Lock(string name, string description, Key _key, Item _unlockedVersion, LockedExit _lockedExit)
        {
            itemName = name;
            itemDescription = description;
            key = _key;
            unlockedVersion = _unlockedVersion;
            lockedExit = _lockedExit;
        }

        public Tuple<bool, Exit, Item> Unlock(Key _key)
        {
            // return whether the key is correct and the new room
            if (_key == key)
                return Tuple.Create(true, lockedExit, unlockedVersion);
            else
                return Tuple.Create(false, new Exit(), new Item());// create a blank room + item if incorrect
        }
    }
}
