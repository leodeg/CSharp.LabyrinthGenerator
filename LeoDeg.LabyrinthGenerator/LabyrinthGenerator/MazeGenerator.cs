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
		public Direction[] Cardinal { get; } = { Direction.North, Direction.East, Direction.West, Direction.South };
		public Direction[] Opposite { get; } = { Direction.South, Direction.West, Direction.East, Direction.North };

		protected Position CurrentPosition { get; set; }
		protected const int STEP = 2;

		private Random random;

		private readonly Dictionary<Direction, Position> Locations = new Dictionary<Direction, Position>
		{
			{ Direction.North, new Position (-1, 0) },
			{ Direction.East, new Position (0, 1) },
			{ Direction.West, new Position (0, -1) },
			{ Direction.South, new Position (1, 0) }
		};


		public MazeGenerator (int size)
		{
			CellsNumber = size;
			WallsNumber = size + 1;
			MazeSize = CellsNumber + WallsNumber;
			Maze = new State[MazeSize, MazeSize];
			AssignWalls ();

			random = new Random ();
		}

		public virtual void Generate ()
		{
			throw new NotImplementedException ();
		}

		#region Get Position And Direction

		protected Position GetStartPosition ()
		{
			bool found = false;
			Position position = new Position (-1, -1);

			while (!found)
			{
				position.x = random.Next (0, MazeSize - 1);
				position.y = random.Next (0, MazeSize - 1);
				if (!(Maze[position.x, position.y] == State.Wall))
					found = true;
			}

			return position;
		}

		protected int GetNewPoint ()
		{
			return random.Next (0, MazeSize - 1);
		}

		protected Position GetPosition (Direction direction)
		{
			Position position;
			Locations.TryGetValue (direction, out position);
			return position;
		}

		protected Position GetNewPosition ()
		{
			Position position;
			Locations.TryGetValue (GenerateNewDirection (), out position);
			return position;
		}

		protected Direction GenerateNewDirection ()
		{
			return (Direction)random.Next (0, 3);
		}

		protected Position GetOppositePosition (Direction direction)
		{
			Position position;
			Locations.TryGetValue (GetOppositeDirection (direction), out position);
			return position;
		}

		protected Direction GetOppositeDirection (Direction direction)
		{
			Direction cardinalDirection = Cardinal.First (x => x == direction);
			return Opposite[(int)cardinalDirection];
		}

		#endregion

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
