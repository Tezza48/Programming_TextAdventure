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
        private Exit.Directions exitDirection;// desired exit direction
        private Item unlockedVersion;
        private Location lockedLocation; // location that this lock blocks off
        private string unlockMessage;

        internal Location LockedLocation { set { lockedLocation = value; } }

        public Lock()
        {
            itemName = "";
            itemDescription = "";
            key = null;
            exitDirection = Exit.Directions.Undefined;
            unlockedVersion = null;
            lockedLocation = null;
            unlockMessage = "";
        }

        public Lock(string name, string description, string message)
        {
            itemName = name;
            itemDescription = description;
            unlockMessage = message;
            key = null;
            exitDirection = Exit.Directions.Undefined;
            unlockedVersion = null;
            lockedLocation = null;
        }

        public Lock(string name, string description, string message, Key _key, Exit.Directions _exitDirection, Item _unlockedVersion)
        {
            itemName = name;
            itemDescription = description;
            unlockMessage = message;
            key = _key;
            exitDirection = _exitDirection;
            unlockedVersion = _unlockedVersion;
            lockedLocation = null;
        }

        public Lock(string name, string description, string message, Key _key, Exit.Directions _exitDirection, Item _unlockedVersion, Location _lockedLocation)
        {
            itemName = name;
            itemDescription = description;
            unlockMessage = message;
            key = _key;
            exitDirection = _exitDirection;
            unlockedVersion = _unlockedVersion;
            lockedLocation = _lockedLocation;
        }

        public Tuple<bool, Exit.Directions, Location, string, Item> Unlock(Key _key)
        {
            // return whether the key is correct and the new room
            if (_key == key)
                return Tuple.Create(true, exitDirection, lockedLocation, unlockMessage, unlockedVersion);
            else
                return Tuple.Create(false, Exit.Directions.Undefined, new Location(), "", new Item());// create a blank room + item if incorrect
        }
    }
}
