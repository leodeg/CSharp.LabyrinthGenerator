using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class AldousBroderGenerator : MazeGenerator
	{
		public AldousBroderGenerator (int size) : base (size)
		{

		}


		public static void CreateMaze (Grid grid)
		{
			Random rand = new Random ((int)DateTime.Now.Ticks);
			Cell currentCell = grid.GetRandomCell ();
			int unvisited = grid.Size - 1;

			while (unvisited > 0)
			{
				List<Cell> neighbours = currentCell.Neighbours ();
				Cell nextNeighbour = neighbours[rand.Next (neighbours.Count)];

				if (!nextNeighbour.Links.Any ())
				{
					currentCell.LinkCells (nextNeighbour, true);
					unvisited--;
				}

				currentCell = nextNeighbour;
			}
		}
	}
}
