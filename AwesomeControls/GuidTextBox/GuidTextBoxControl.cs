using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.GuidTextBox
{
	public partial class GuidTextBoxControl : UserControl
	{
		public GuidTextBoxControl()
		{
			InitializeComponent();
		}

		private Guid mvarValue = Guid.Empty;
		public Guid Value { get { return mvarValue; } set { mvarValue = value; txtGuid.Text = mvarValue.ToString("B"); } }

		private void cmdGuidNew_Click(object sender, EventArgs e)
		{
			NewGuid();
		}

		private void cmdGuidEmpty_Click(object sender, EventArgs e)
		{
			Clear();
		}

		public void NewGuid()
		{
			Value = Guid.NewGuid();
		}
		public void Clear()
		{
			Value = Guid.Empty;
		}

		private void txtGuid_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				Guid guid = new Guid(txtGuid.Text);
				Value = guid;
			}
			catch
			{
				if (MessageBox.Show("Please enter a valid GUID.", "Invalid GUID", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
				{
					e.Cancel = true;
				}
			}
		}
	}
}
