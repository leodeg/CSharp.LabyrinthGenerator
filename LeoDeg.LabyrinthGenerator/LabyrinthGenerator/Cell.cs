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
		public CellNeighbours Neighbours { get; set; }

		public int RowIndex { get; set; }
		public int ColumnIndex { get; set; }

		public List<Cell> Links = new List<Cell> ();

		public Cell (int row, int column)
		{
			RowIndex = row;
			ColumnIndex = column;
			Neighbours = new CellNeighbours ();
		}

		public Cell AddLink (Cell cell, bool bidirectional)
		{
			Links.Add (cell);
			if (bidirectional)
				cell.AddLink (this, false);
			return this;
		}

		public Cell RemoveLink (Cell cell, bool bidirectional)
		{
			if (Links.Contains (cell))
			{
				Links.Remove (cell);
				if (bidirectional)
					cell.RemoveLink (this, false);
			}

			return this;
		}

		public bool IsLinked (Cell cell)
		{
			return Links.Contains (cell);
		}

		public List<Cell> GetNeighbours ()
		{
			List<Cell> neighbours = new List<Cell> ();

			if (Neighbours.North != null) neighbours.Add (Neighbours.North);
			if (Neighbours.South != null) neighbours.Add (Neighbours.South);
			if (Neighbours.East != null) neighbours.Add (Neighbours.East);
			if (Neighbours.West != null) neighbours.Add (Neighbours.West);

			return neighbours;
		}
	}
}
