using System.Collections.Generic;

namespace TextAdventure
{
    struct GameData
    {
        public string startText;
        public List<Location> locations;
        public List<Item> playerInventory;
    }
    struct SaveData
    {
        public int currentLocation;
        public string startText;
        public List<Location> locations;
        public List<Item> playerInventory;
    }
}
