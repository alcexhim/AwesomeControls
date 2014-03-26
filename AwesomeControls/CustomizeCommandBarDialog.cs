using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls
{
	public partial class CustomizeCommandBarDialog : Dialog
	{
		public CustomizeCommandBarDialog()
		{
			InitializeComponent();
			Font = SystemFonts.MenuFont;
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
	}
}
