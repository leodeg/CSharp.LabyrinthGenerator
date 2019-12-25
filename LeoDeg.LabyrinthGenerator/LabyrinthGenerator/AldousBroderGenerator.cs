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
		public Position currentPosition;
		public int OpenCells { get; protected set; }

		public AldousBroderGenerator (int size) : base (size)
		{

		}

		public override void Generate ()
		{
			OpenCells = 0;
			int exitAmount = 2;

			currentPosition = GetStartPosition ();
			Maze[currentPosition.x, currentPosition.y] = State.Open;
			++OpenCells;

			while (OpenCells < CellsNumber + exitAmount)
			{
				// Получить случайную позицию
				// Если локация не была посещена
				// Пометить посещенной
				// Разрушить стену между текущей и предыдущей локацией
				// Увеличить кол-во посещенных локаций
				// Если локация была посещена
				// Идти в следующую локацию

				Direction newDirection = GenerateNewDirection ();
				currentPosition += GetPosition (newDirection) * STEP;

				if (currentPosition.x > 0 && currentPosition.x < MazeSize &&
					currentPosition.y > 0 && currentPosition.y < MazeSize)
				{

				}

				if (currentPosition.x > 0 && currentPosition.x < MazeSize &&
					currentPosition.y > 0 && currentPosition.y < MazeSize)
				{
					if (Maze[currentPosition.x, currentPosition.y] == State.Close)
					{
						Maze[currentPosition.x, currentPosition.y] = State.Open;
						Position oppositePosition = GetOppositePosition (newDirection);
						Position previousPosition = currentPosition - oppositePosition;
						Maze[previousPosition.x, previousPosition.y] = State.Open;
						++OpenCells;
					}
				}
			}
		}
	}
}
