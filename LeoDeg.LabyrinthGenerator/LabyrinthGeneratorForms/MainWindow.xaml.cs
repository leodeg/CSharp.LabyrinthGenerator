﻿using LabyrinthGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		MazeGenerator mazeGenerator;

		public MainWindow ()
		{
			InitializeComponent ();

			mazeGenerator = new MazeGenerator (10);
			MessageBox.Show (mazeGenerator.ToString ());
		}
	}
}
