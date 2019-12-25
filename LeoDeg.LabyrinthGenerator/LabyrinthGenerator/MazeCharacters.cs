using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LabyrinthGenerator.MazeGenerator;

namespace LabyrinthGenerator
{
	public static class MazeCharacters
	{
		public const char Wall = '*';
		public const char Close = 'X';
		public const char Open = 'O';

		public static char GetCharacter (State state)
		{
			switch (state)
			{
				case State.Close:
					return Close;
				case State.Open:
					return Open;
				case State.Wall:
					return Wall;
				default:
					return Close;
			}
		}
	}
}
