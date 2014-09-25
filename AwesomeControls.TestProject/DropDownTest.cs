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
	public partial class DropDownTest : Form
	{
		public DropDownTest()
		{
			InitializeComponent();
		}

		private void dropDownControl1_PaintContent(object sender, PaintEventArgs e)
		{
			TextRenderer.DrawText(e.Graphics, "Select from list OBJECTS", dropDownControl1.Font, new Rectangle(2, 4, dropDownControl1.Width - 4, dropDownControl1.Height - 4), Color.Red, TextFormatFlags.Left);
		}
	}
}
