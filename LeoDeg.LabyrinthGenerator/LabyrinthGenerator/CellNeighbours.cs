using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LabyrinthGenerator
{
	public class CellNeighbours
	{
		public Cell North { get; set; }
		public Cell South { get; set; }
		public Cell East { get; set; }
		public Cell West { get; set; }
	}

}
