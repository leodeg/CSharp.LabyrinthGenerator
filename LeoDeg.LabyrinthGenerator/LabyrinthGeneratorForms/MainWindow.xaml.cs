using LabyrinthGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabyrinthGeneratorForms
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Maze mazeGrid;
		AldousBroderMazeGenerator mazeGenerator;

		private const double CELL_SIZE = 0.5;
		private const double SPACE = 0.000001;

		private int size;
		private double cellWidth;
		private double cellHeight;
		private bool mazeCreated = false;

		public MainWindow ()
		{
			InitializeComponent ();
			mazeGenerator = new AldousBroderMazeGenerator ();
		}

		private void canvasMazeGrid_SizeChanged (object sender, SizeChangedEventArgs e)
		{
			// TODO: fix index was going out of cells' range error

			//if (mazeCreated)
			//{
			//	DrawMazeCells ();
			//	DrawMazeWalls ();
			//}
		}

		private void buttonCreateMaze_Click (object sender, RoutedEventArgs e)
		{
			if (canvasMazeGrid.Children.Count > 0)
				canvasMazeGrid.Children.Clear ();
			mazeCreated = false;

			GetMazeSize ();
			CreateMaze (size);
			DrawMazeCells ();
			DrawMazeWalls ();

			MessageBox.Show (string.Format ("Maze was draw." + canvasMazeGrid.Children.Count.ToString ()));
		}

		private void GetMazeSize ()
		{
			try
			{
				size = int.Parse (textBoxMazeSize.Text);
			}
			catch (FormatException ex)
			{
				MessageBox.Show (string.Format ("Wrong size of a new maze. \nFormat exception: ", ex.Message));
				throw;
			}
		}

		private void CreateMaze (int size)
		{
			mazeGrid = new Maze (size, size);
			mazeGenerator.CreateMaze (mazeGrid);
			mazeCreated = true;
		}

		private void DrawMazeCells ()
		{
			cellHeight = canvasMazeGrid.ActualHeight / mazeGrid.Height;
			cellWidth = canvasMazeGrid.ActualWidth / mazeGrid.Width;
			double nextX = 0;
			double nextY = 0;

			for (int i = 0; i < mazeGrid.Size; i++)
			{
				AssignNewBorderToCanvas (nextX, nextY);

				nextX += cellWidth + SPACE;
				if (nextX > canvasMazeGrid.ActualWidth)
				{
					nextX = 0;
					nextY += cellHeight;
				}
			}

		}

		private void AssignNewBorderToCanvas (double xPos, double yPos)
		{
			Border border = GetNewBorder ();
			canvasMazeGrid.Children.Add (border);
			Canvas.SetTop (border, yPos);
			Canvas.SetLeft (border, xPos);
		}

		private Border GetNewBorder ()
		{
			return new Border
			{
				Height = cellHeight,
				Width = cellWidth,
				BorderThickness = new Thickness (CELL_SIZE),
				Background = Brushes.White,
				BorderBrush = Brushes.Black,
				Visibility = Visibility.Visible
			};
		}

		public void DrawMazeWalls ()
		{
			List<Cell> cells = mazeGrid.GetCells ();

			int index = 0;
			foreach (var cell in canvasMazeGrid.Children)
			{
				if (index < cells.Count)
				{
					Cell currentCell = cells[index];
					Border border = cell as Border;
					border.BorderThickness = DeleteWallsFor (currentCell);

					++index;
				}
			}
		}

		private Thickness DeleteWallsFor (Cell cell)
		{
			Thickness thickness = new Thickness (1);

			if (cell.IsLinked (cell.Neighbours.North))
				thickness.Top = 0;

			if (cell.IsLinked (cell.Neighbours.South))
				thickness.Bottom = 0;

			if (cell.IsLinked (cell.Neighbours.West))
				thickness.Left = 0;

			if (cell.IsLinked (cell.Neighbours.East))
				thickness.Right = 0;

			return thickness;
		}
	}
}
