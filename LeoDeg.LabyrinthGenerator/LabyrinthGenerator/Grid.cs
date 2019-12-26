using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class Grid
	{
		public int Height { get; protected set; }
		public int Width { get; protected set; }
		public int Size { get; protected set; }

		private Cell[,] cells;

		private Random random;

		public Grid (int height, int width)
		{
			random = new Random ((int)DateTime.Now.Ticks);

			Height = height;
			Width = width;
			Size = Width * Height;

			CreateGrid ();
			LinkNeighbours ();
		}

		private void CreateGrid ()
		{
			cells = new Cell[Height, Width];
			for (int row = 0; row < Height; row++)
				for (int column = 0; column < Width; column++)
					cells[row, column] = new Cell (row, column);
		}

		private void LinkNeighbours ()
		{
			for (int y = 0; y < Height; y++)
			{
				for (int x = 0; x < Width; x++)
				{
					if (y > 0)
						cells[y, x].North = cells[y - 1, x];
					if (y < (Height - 1))
						cells[y, x].South = cells[y + 1, x];
					if (x < (Width - 1))
						cells[y, x].East = cells[y, x + 1];
					if (x > 0)
						cells[y, x].West = cells[y, x - 1];
				}
			}
		}

		public Cell GetRandomCell ()
		{
			int row = random.Next (Height - 1);
			int column = random.Next (Width - 1);
			return cells[row, column];
		}

		public Cell GetCell (int x, int y)
		{
			if (y >= 0 && y <= Height &&
				x >= 0 && x <= Width)
				return cells[y, x];

			return null;
		}

		public State[,] GetMaze ()
		{
			int totalHeight = Height * 2 + 1;
			int totalWidth = Width * 2 + 1;

			State[,] maze = new State[totalHeight, totalWidth];
			string mazeString = this.ToString ();

			int index = 0;
			for (int y = 0; y < totalHeight; y++)
			{
				for (int x = 0; x < totalWidth; x++)
				{
					if (mazeString[index] == ' ')
						maze[y, x] = State.Open;
					else if (mazeString[index] == '+' || mazeString[index] == '-' || mazeString[index] == '|')
						maze[y, x] = State.Wall;
					else maze[y, x] = State.Wall;

					++index;
				}
				index += 2; // \n -> symbol of a new line have a 2 characters
			}

			return maze;
		}

		public string ToString (State[,] maze)
		{
			int height = Height * 2 + 1;
			int width = Width * 2 + 1;
			StringBuilder stringBuilder = new StringBuilder ();
			for (int row = 0; row < height; row++)
			{
				for (int column = 0; column < width; column++)
					stringBuilder.Append (MazeCharacters.GetCharacter (maze[row, column]));
				stringBuilder.AppendLine ();
			}
			return stringBuilder.ToString ();
		}

		public override string ToString ()
		{
			StringBuilder builder = new StringBuilder ();
			builder.AppendLine ("+" + new string ('£', Width).Replace ("£", "-+"));

			for (int row = 0; row < Height; row++)
			{
				string top = "|";
				string bottom = "+";

				for (int col = 0; col < Width; col++)
				{
					var currentCell = GetCell (col, row);
					string body = " ";

					var east_boundary = currentCell.IsLinked (currentCell.East) ? " " : "|";
					top = top + body + east_boundary;

					var south_boundary = currentCell.IsLinked (currentCell.South) ? " " : "-";
					bottom = bottom + south_boundary + "+";

				}

				builder.AppendLine (top);
				builder.AppendLine (bottom);
			}

			return builder.ToString ();
		}
	}
}
