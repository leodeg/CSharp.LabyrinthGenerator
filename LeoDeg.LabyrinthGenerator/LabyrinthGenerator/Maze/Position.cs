using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public struct Position
	{
		public Position (int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int x;
		public int y;

		public static Position operator + (Position a, Position b)
		{
			return new Position (a.x + b.x, a.y + b.y);
		}

		public static Position operator - (Position a, Position b)
		{
			return new Position (a.x - b.x, a.y - b.y);
		}

		public static Position operator * (Position a, int b)
		{
			return new Position (a.x * b, a.y * b);
		}

		public static bool operator == (Position a, Position b)
		{
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator != (Position a, Position b)
		{
			return !(a.x == b.x && a.y == b.y);
		}

		public override bool Equals (object obj)
		{
			return obj is Position position &&
				   x == position.x &&
				   y == position.y;
		}

		public override int GetHashCode ()
		{
			return x.GetHashCode () + y.GetHashCode ();
		}
	}
}
