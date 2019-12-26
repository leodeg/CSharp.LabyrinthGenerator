using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class Cell
	{
		public Cell North { get; set; }
		public Cell South { get; set; }
		public Cell East { get; set; }
		public Cell West { get; set; }

		public int Row { get; set; }
		public int Column { get; set; }

		public List<Cell> Links = new List<Cell> ();

		public Cell (int row, int column)
		{
			Row = row;
			Column = column;
		}

		public Cell LinkCells (Cell cell, bool biDirectional)
		{
			Links.Add (cell);
			if (biDirectional)
				cell.LinkCells (this, false);
			return this;
		}

		public Cell UnlinkCell (Cell cell, bool biDirectional)
		{
			if (Links.Contains (cell))
			{
				Links.Remove (cell);
				if (biDirectional)
					cell.UnlinkCell (this, false);

			}

			return this;
		}

		public bool IsLinked (Cell cell)
		{
			if (Links.Contains (cell))
				return true;
			return false;
		}

		public List<Cell> Neighbours ()
		{
			List<Cell> neighbours = new List<Cell> ();

			if (North != null) neighbours.Add (North);
			if (South != null) neighbours.Add (South);
			if (East != null) neighbours.Add (East);
			if (West != null) neighbours.Add (West);

			return neighbours;

		}
	}

}
