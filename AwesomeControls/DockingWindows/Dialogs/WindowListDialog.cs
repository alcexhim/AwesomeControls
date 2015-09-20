using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DockingWindows.Dialogs
{
	public partial class WindowListDialog : Form
	{
		public WindowListDialog(DockingContainerControl parentControl)
		{
			InitializeComponent();
			mvarParentContainer = parentControl;
			Font = SystemFonts.MenuFont;
		}

		private DockingContainerControl mvarParentContainer = null;
		public DockingContainerControl ParentContainer { get { return mvarParentContainer; } }

		private void cmdDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private DockingWindow mvarSelectedWindow = null;
		public DockingWindow SelectedWindow { get { return mvarSelectedWindow; } set { mvarSelectedWindow = value; } }

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			lv.Items.Clear();
			foreach (DockingWindow dw in mvarParentContainer.Areas[DockPosition.Center].Areas[DockPosition.Center].Windows)
			{
				ListViewItem lvi = new ListViewItem();
				lvi.Text = dw.Title;
				lv.Items.Add(lvi);
			}
		}
	}
}
