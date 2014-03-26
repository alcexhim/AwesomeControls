using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.PropertyGrid
{
	public partial class PropertyGridErrorDialog : Form
	{
		public PropertyGridErrorDialog()
		{
			InitializeComponent();

			Font = SystemFonts.MenuFont;
			lblTitle.Font = new Font(Font, FontStyle.Bold);
		}

		public static DialogResult ShowDialog(string message, string details = "", string title = "Error")
		{
			PropertyGridErrorDialog dlg = new PropertyGridErrorDialog();
			dlg.Text = title;
			dlg.lblTitle.Text = message;
			dlg.txtDetails.Text = details;
			return dlg.ShowDialog();
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}
}
