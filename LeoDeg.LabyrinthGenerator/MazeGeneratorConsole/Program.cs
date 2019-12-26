using LabyrinthGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorConsole
{
	class Program
	{
		static void Main (string[] args)
		{
			//AldousBroderGenerator mazeGenerator = new AldousBroderGenerator (4);
			//mazeGenerator.OnWriteLine += Console.WriteLine;
			//mazeGenerator.Generate ();
			//Console.ReadLine ();

			Maze grid = new Maze (6, 10);
			Console.WriteLine (grid.ToString ());

			AldousBroderMazeGenerator generator =  new	AldousBroderMazeGenerator ();
			generator.CreateMaze (grid);

			Console.WriteLine (grid.ToString ());

			State[,] maze = grid.GetMaze ();

			string mazeString = grid.ToString (maze);

			Console.WriteLine (mazeString.Length);

			Console.WriteLine (grid.ToString (grid.GetMaze ()));

		}
	}
}
