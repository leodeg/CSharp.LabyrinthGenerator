using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class Maze
	{
		/// <summary>
		/// Height without walls.
		/// </summary>
		public int Height { get; protected set; }

		/// <summary>
		/// Width without walls.
		/// </summary>
		public int Width { get; protected set; }
		public int Size { get { return Height * Width;  } }

		public int HeightWithWalls { get { return Height * 2 + 1; } }
		public int WidthWithWalls { get { return Width * 2 + 1; } }

		public Cell[,] Cells { get; protected set; }


		private Random randomGenerator;

		public Maze (int height, int width)
		{
			randomGenerator = new Random ((int)DateTime.Now.Ticks);

			Height = height;
			Width = width;

			CreateGrid ();
			LinkNeighbours ();
		}

		public List<Cell> GetCells ()
		{
			List<Cell> cells = new List<Cell> ();

			for (int i = 0; i < Height; i++)
				for (int j = 0; j < Width; j++)
					cells.Add (Cells[i, j]);

			return cells;
		}

		private void CreateGrid ()
		{
			Cells = new Cell[Height, Width];
			for (int row = 0; row < Height; row++)
				for (int column = 0; column < Width; column++)
					Cells[row, column] = new Cell (row, column);
		}

		private void LinkNeighbours ()
		{
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					if (y > 0)
						Cells[y, x].Neighbours.North = Cells[y - 1, x];
					if (y < (Height - 1))
						Cells[y, x].Neighbours.South = Cells[y + 1, x];
					if (x < (Width - 1))
						Cells[y, x].Neighbours.East = Cells[y, x + 1];
					if (x > 0)
						Cells[y, x].Neighbours.West = Cells[y, x - 1];
				}
			}
		}

		public Cell GetRandomCell ()
		{
			int row = randomGenerator.Next (Height - 1);
			int column = randomGenerator.Next (Width - 1);
			return Cells[row, column];
		}

		public Cell GetCell (int x, int y)
		{
			if (y >= 0 && y <= Height && x >= 0 && x <= Width)
				return Cells[y, x];

			return null;
		}

		public State[,] GetMaze ()
		{
			State[,] maze = new State[HeightWithWalls, WidthWithWalls];
			string mazeString = this.ToString ();

			int stringIndex = 0;
			for (int y = 0; y < HeightWithWalls; y++)
			{
				for (int x = 0; x < WidthWithWalls; x++)
				{
					if (mazeString[stringIndex] == MazeChar.Open)
						maze[y, x] = State.Open;
					else maze[y, x] = State.Wall;

					++stringIndex;
				}
				stringIndex += 2; // \n -> symbol of a new line have a 2 characters
			}

			return maze;
		}

		public string ToString (State[,] maze)
		{
			StringBuilder stringBuilder = new StringBuilder ();
			for (int row = 0; row < HeightWithWalls; row++)
			{
				for (int column = 0; column < WidthWithWalls; column++)
					stringBuilder.Append (MazeChar.GetCharacter (maze[row, column]));
				stringBuilder.AppendLine ();
			}
			return stringBuilder.ToString ();
		}

		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();

			builder.Append (MazeChar.Intersection)
				.AppendLine (new string (MazeChar.Wall, Width)
				.Replace (MazeChar.Wall.ToString (), MazeChar.HorizontalIntersection));

			for (int row = 0; row < Height; row++)
			{
				string currentLinkedLine = MazeChar.VerticalLine.ToString ();
				string bottomLinkedLine = MazeChar.Intersection.ToString ();

				for (int column = 0; column < Width; column++)
				{
					var currentCell = GetCell (column, row);

					char rigthSymbol = currentCell.IsLinked (currentCell.Neighbours.East) ? MazeChar.Open : MazeChar.VerticalLine;
					currentLinkedLine = currentLinkedLine + MazeChar.Open + rigthSymbol;

					char bottomSymbol = currentCell.IsLinked (currentCell.Neighbours.South) ? MazeChar.Open : MazeChar.HorizontalLine;
					bottomLinkedLine = bottomLinkedLine + bottomSymbol + MazeChar.Intersection;

				}

				builder.AppendLine (currentLinkedLine);
				builder.AppendLine (bottomLinkedLine);
			}

			return builder.ToString ();
		}
	}
}
