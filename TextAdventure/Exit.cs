using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Exit
    {
		public enum Directions
		{
			Undefined, North, South, East, West, Up, Down, NorthEast, NorthWest, SouthEast, SouthWest, In, Out
		};

		public static string[] shortDirections = {"Null", "N", "S", "E", "W", "U", "D", "NE", "NW", "SE", "SW", "I", "O"};

		private Location leadsTo;
		private Directions direction;

		public Exit()
		{
			direction = Directions.Undefined;
			leadsTo = null;
		}

		public Exit(Directions _direction, Location newLeadsTo)
		{
			direction = _direction;
			leadsTo = newLeadsTo;
		}

		public override string ToString()
		{
			return direction.ToString();
		}

		public void setDirection(Directions _direction)
		{
			direction = _direction;
		}

		public Directions getDirection()
		{
			return direction;
		}

		public string getShortDirection()
		{
			return shortDirections[(int)direction].ToLower();
		}

		public void setLeadsTo(Location _leadsTo)
		{
			leadsTo = _leadsTo;
		}

		public Location getLeadsTo()
		{
			return leadsTo;
		}
    }
}
