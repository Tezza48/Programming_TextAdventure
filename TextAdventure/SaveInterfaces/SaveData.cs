using Newtonsoft.Json;
using System.Collections.Generic;

namespace TextAdventure.LevelEditor
{
    //struct GameData
    //{
    //    public string startText;
    //    public List<Location> locations;
    //    public List<Item> playerInventory;
    //}

    struct SaveData
    {
        public string startText;
        public List<Location> locations;
        public List<Item> playerInventory;
    }

    class Location
    {
        [JsonRequired]
        private string title;
        [JsonRequired]
        private string description;
        [JsonRequired]
        private Exit[] exits;
        [JsonRequired]
        private List<Item> inventory;

        [JsonIgnore]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        [JsonIgnore]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }
        [JsonIgnore]
        public List<Item> Inventory
        {
            get
            {
                return inventory;
            }

            set
            {
                inventory = value;
            }
        }
        [JsonIgnore]
        public Exit[] Exits
        {
            get
            {
                return exits;
            }
        }

        public Location(string _title = "New Location", string _description = "")
        {
            title = _title;
            description = _description;
            exits = new Exit[(int)Exit.Directions.Out + 1];
            inventory = new List<Item>();
        }
    }
    
    class Key : Item
    {
        [JsonRequired]
        private string keyName;
        [JsonRequired]
        private string keyDescription;
        [JsonRequired]
        private bool destroyedOnUse;

        [JsonIgnore]
        public string KeyName { get { return keyName; } set { keyName = value; } }

        [JsonIgnore]
        public string KeyDescription { get { return keyDescription; } set { keyDescription = value; } }

        [JsonIgnore]
        public bool DestroyedOnUse { get { return destroyedOnUse; } }

        public Key(string _name = "New Key", string _description = "", bool _destroyedOnUse = true)
        {
            keyName = _name;
            keyDescription = _description;
            destroyedOnUse = _destroyedOnUse;
        }
    }

    class Item
    {
        [JsonRequired]
        private string itemName;
        [JsonRequired]
        private string itemDescription;

        [JsonIgnore]
        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }
        [JsonIgnore]
        public string ItemDescription
        {
            get
            {
                return itemDescription;
            }

            set
            {
                itemDescription = value;
            }
        }

        public Item(string _name = "New Item", string _description = "")
        {
            itemName = _name;
            itemDescription = _description;
        }
    }

    class Exit
    {
        public enum Directions
        {
            Undefined, North, South, East, West, Up, Down, NorthEast, NorthWest, SouthEast, SouthWest, In, Out
        };

        [JsonRequired]
        private int leadsTo;
        [JsonRequired]
        private Directions direction;
        private Key key;

        [JsonProperty]
        public Key Key
        {
            get
            {
                if (key == null)
                {
                    return null;
                }
                else
                {
                    return key;
                }
            }

            set
            {
                key = value;
            }
        }
        public int LeadsTo { get { return leadsTo; } }
        public Directions Direction { get { return direction; } }

        public Exit(Directions _direction, int _leadsTo, Key _key = null)
        {
            direction = _direction;
            leadsTo = _leadsTo;
            key = _key;
        }
    }

}
