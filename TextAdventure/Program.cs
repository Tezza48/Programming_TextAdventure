using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Program
    {
		

        static void Main(string[] args)
        {

			Game _Game = new Game();

			//start our game loop - we keep running this function until the player quits.
			while ( _Game.isRunning )
			{
				_Game.Update();
			}

        }
    }
}
