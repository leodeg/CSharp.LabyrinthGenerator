using LabyrinthGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGeneratorConsole
{
	class Program
	{
		static void Main (string[] args)
		{
			AldousBroderGenerator mazeGenerator = new AldousBroderGenerator (7);
			//mazeGenerator.Generate ();
			Console.WriteLine (mazeGenerator.ToString ());
			Console.ReadLine ();
		}
	}
}
