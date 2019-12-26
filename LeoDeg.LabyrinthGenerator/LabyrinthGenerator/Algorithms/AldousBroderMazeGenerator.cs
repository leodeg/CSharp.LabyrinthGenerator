using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class AldousBroderMazeGenerator
	{
		public Action OnStartCreateMaze;
		public Action OnIterationCreateMaze;

		public void CreateMaze (Maze grid)
		{
			OnStartCreateMaze?.Invoke ();

			Random rand = new Random ((int)DateTime.Now.Ticks);
			Cell currentCell = grid.GetRandomCell ();
			int unvisited = grid.Size - 1;

			while (unvisited > 0)
			{

				List<Cell> neighbours = currentCell.GetNeighbours ();
				Cell nextNeighbour = neighbours[rand.Next (neighbours.Count)];

				if (!nextNeighbour.Links.Any ())
				{
					OnIterationCreateMaze?.Invoke ();
					currentCell.AddLink (nextNeighbour, true);
					unvisited--;
				}

				currentCell = nextNeighbour;
			}
		}
	}
}
