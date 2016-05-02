using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    class Program
    {
        public static string StoryPath = "data.json";

        static void Main(string[] args)
        {
            List<string> lArgs = args.ToList();
            if (args.Contains("-E"))
            {
                try
                {
                    System.Diagnostics.Process.Start("LevelEditor.exe");
                }
                catch
                {
                    Console.Write("You tried to launch the Level Editor, it didn't work, sorry.\nMake sure it's correctly Named \"LevelEditor.exe\".");
                    Console.ReadKey();
                }
                return;
            }
			Game _Game = new Game();

			//start our game loop - we keep running this function until the player quits.
			while ( _Game.isRunning )
			{
				_Game.Update();
			}

        }
    }
}
