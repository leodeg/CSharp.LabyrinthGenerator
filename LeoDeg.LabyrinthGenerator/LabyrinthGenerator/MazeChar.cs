using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public static class MazeChar
	{
		public const char Wall = '*';
		public const char Close = 'X';
		public const char Open = ' ';

		public const char Intersection = '+';
		public const char VerticalLine = '|';
		public const char HorizontalLine = '-';

		public const string HorizontalIntersection = "-+";

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
