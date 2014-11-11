using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.FileTextBox
{
	[DefaultEvent("SelectionChanged")]
	public partial class FileTextBoxControl : UserControl
	{
		public FileTextBoxControl()
		{
			InitializeComponent();
		}

		public event CancelEventHandler SelectionChanging;
		protected virtual void OnSelectionChanging(CancelEventArgs e)
		{
			if (SelectionChanging != null)
			{
				SelectionChanging(this, e);
			}
		}

		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (SelectionChanged != null)
			{
				SelectionChanged(this, e);
			}
		}

		private FileTextBoxMode mvarMode = FileTextBoxMode.Open;
		public FileTextBoxMode Mode { get { return mvarMode; } set { mvarMode = value; } }

		public string SelectedFileName { get { return txt.Text; } set { txt.Text = value; } }

		private void txt_Click(object sender, EventArgs e)
		{
			string fileName = null;
			switch (mvarMode)
			{
				case FileTextBoxMode.Open:
				{
					OpenFileDialog ofd = new OpenFileDialog();
					ofd.FileName = SelectedFileName;
					if (ofd.ShowDialog() == DialogResult.OK)
					{
						fileName = ofd.FileName;
					}
					break;
				}
				case FileTextBoxMode.Save:
				{
					SaveFileDialog sfd = new SaveFileDialog();
					sfd.FileName = SelectedFileName;
					if (sfd.ShowDialog() == DialogResult.OK)
					{
						fileName = sfd.FileName;
					}
					break;
				}
			}
			if (fileName == null) return;

			FileTextBoxSelectionChangingEventArgs ee = new FileTextBoxSelectionChangingEventArgs(fileName);
			OnSelectionChanging(ee);
			if (ee.Cancel) return;

			fileName = ee.FileName;
			SelectedFileName = fileName;

			OnSelectionChanged(e);
		}
	}
}
