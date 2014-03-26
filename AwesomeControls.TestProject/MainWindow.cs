using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.TestProject
{
	public partial class MainWindow : Form
	{
		public MainWindow()
		{
			InitializeComponent();

			tc.Groups.Add("center");
			tc.Groups.Add("leg IK right");
			tc.Groups.Add("leg IK left");

			tc.Groups[1].Entries.Add("keyframe 1", 0, 96);
			tc.Groups[1].Entries.Add("kf2", 100, 32, Color.Red);

			tc.Groups[2].Entries.Add("a--eh", 64, 100, Color.CadetBlue);
		}
	}
}
