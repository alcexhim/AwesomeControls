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
	public partial class ToggleSwitchTest : Form
	{
		public ToggleSwitchTest()
		{
			InitializeComponent();

			toggleSwitchControl1.Text = "Align Assignments";
			toggleSwitchControl2.Text = "Automatic Brace Completion";
			toggleSwitchControl3.Text = "Colorized Parameter Help";
			toggleSwitchControl4.Text = "Column Guides";
		}
	}
}
