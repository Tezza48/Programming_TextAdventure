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
        // is this exit locked
        private Exit unlockedExit;

        // constructors
        //  default
        public LockedExit()
        {
            direction = Directions.Undefined;
            leadsTo = null;
        }
        //  directions, location, key
        public LockedExit(Directions _direction, Location newLeadsTo)
        {
            direction = _direction;
            leadsTo = newLeadsTo;
        }
    }
}
