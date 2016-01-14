using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class LockedExit : Exit
    {
        // an exit that only becomes useble once an item is used with it
        // key used to unlock this exit
        private string key;
        // is this exit locked
        private bool isLocked;

        // constructors
        //  default
        public LockedExit()
        {
            direction = Directions.Undefined;
            leadsTo = null;
            key = null;
        }
        //  directions, location, key
        public LockedExit(Directions _direction, Location newLeadsTo, string _key)
        {
            direction = _direction;
            leadsTo = newLeadsTo;
            key = _key;
        }

        // methods
        //  bool unlock (key)
        public bool Unlock (string _key)
        {
        // if this key = key
            if (key == _key)
            {
                // unlocked = true
                isLocked = false;
                // return true
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
