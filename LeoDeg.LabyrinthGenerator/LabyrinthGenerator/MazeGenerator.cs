using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{

	public partial class MazeGenerator
	{

		public State[,] Maze { get; protected set; }
		public int MazeSize { get; protected set; }
		public int WallsNumber { get; protected set; }
		public int CellsNumber { get; protected set; }

		Random random;

		public MazeGenerator (int size)
		{
			CellsNumber = size;
			WallsNumber = size + 1;
			MazeSize = CellsNumber + WallsNumber;
			Maze = new State[MazeSize, MazeSize];
			AssignWalls ();

			random = new Random ((int)DateTime.Now.Ticks);
		}

		public virtual void Generate ()
		{
			throw new NotImplementedException ();
		}

		#region Assign Walls

		protected void AssignWalls ()
		{
			AssignTopWalls ();
			AssignMiddleWalls ();
			AssignBottomWalls ();
		}

		private void AssignTopWalls ()
		{
			for (int column = 0; column < MazeSize; column++)
				Maze[0, column] = State.Wall;
		}

		private void AssignMiddleWalls ()
		{
			for (int row = 0; row < MazeSize - 2; row += 2)
				for (int column = 0; column < MazeSize; column ++)
					Maze[row, column] = State.Wall;

			for (int row = 1; row < MazeSize - 1; row += 2)
				for (int column = 0; column < MazeSize; column += 2)
					Maze[row, column] = State.Wall;
		}

		private void AssignBottomWalls ()
		{
			for (int column = 0; column < MazeSize; column++)
				Maze[MazeSize - 1, column] = State.Wall;
		}

		#endregion

		public override string ToString ()
		{
			StringBuilder stringBuilder = new StringBuilder ();
			for (int row = 0; row < MazeSize; row++)
			{
				for (int column = 0; column < MazeSize; column++)
					stringBuilder.Append (MazeCharacters.GetCharacter (Maze[row, column]));
				stringBuilder.AppendLine ();
			}
			return stringBuilder.ToString ();
		}
	}
}
