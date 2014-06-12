using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using AwesomeControls.CommandBars;

namespace AwesomeControls.DockingWindows
{
	public class PopoutPanel : Panel
	{
		private CBContextMenu mnuPanelTitlebarContextMenu = new CBContextMenu();

		private System.Windows.Forms.Panel popoutPanelTitlebar = null;
		public PopoutPanel()
		{
			mnuPanelTitlebarContextMenu.Items.Add("&Float", null, mnuPanelTitlebarContextMenuFloat_Click);
			mnuPanelTitlebarContextMenu.Items.Add("Doc&k", null, mnuPanelTitlebarContextMenuDock_Click);
			mnuPanelTitlebarContextMenu.Items.Add("Dock as &Tabbed Document", null, mnuPanelTitlebarContextMenuDockTabbed_Click);
			mnuPanelTitlebarContextMenu.Items.Add("&Auto Hide", null, mnuPanelTitlebarContextMenuAutoHide_Click);
			mnuPanelTitlebarContextMenu.Items.Add("&Hide", null, mnuPanelTitlebarContextMenuHide_Click);
			mnuPanelTitlebarContextMenu.Items[3].Enabled = false;

			popoutPanelTitlebar = new System.Windows.Forms.Panel();
			popoutPanelTitlebar.ContextMenuStrip = mnuPanelTitlebarContextMenu;
			popoutPanelTitlebar.MouseDown += new MouseEventHandler(popoutPanelTitlebar_MouseDown);
			popoutPanelTitlebar.Paint += new System.Windows.Forms.PaintEventHandler(popoutPanelTitlebar_Paint);
			popoutPanelTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
			popoutPanelTitlebar.Height = 18;
			Controls.Add(popoutPanelTitlebar);
		}

		#region Panel Titlebar Context Menu
		private void mnuPanelTitlebarContextMenuFloat_Click(object sender, EventArgs e)
		{
		}
		private void mnuPanelTitlebarContextMenuDock_Click(object sender, EventArgs e)
		{
		}
		private void mnuPanelTitlebarContextMenuDockTabbed_Click(object sender, EventArgs e)
		{
		}
		private void mnuPanelTitlebarContextMenuAutoHide_Click(object sender, EventArgs e)
		{
		}
		private void mnuPanelTitlebarContextMenuHide_Click(object sender, EventArgs e)
		{
		}
		#endregion

		void popoutPanelTitlebar_MouseDown(object sender, MouseEventArgs e)
		{
			Focus();
		}

		private bool mvarHasFocus = false;
		public bool HasFocus { get { return mvarHasFocus; } }

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			mvarHasFocus = true;
			popoutPanelTitlebar.Refresh();
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);

			mvarHasFocus = false;
			popoutPanelTitlebar.Refresh();
		}

		private Control mvarChildControl = null;
		public Control ChildControl
		{
			get { return mvarChildControl; }
			set
			{
				if (mvarChildControl != null)
				{
					if (Controls.Contains(mvarChildControl))
					{
						Controls.Remove(mvarChildControl);
					}
				}
				mvarChildControl = value;
				if (mvarChildControl != null)
				{
					if (!Controls.Contains(mvarChildControl))
					{
						Controls.Add(mvarChildControl);
					}
					mvarChildControl.Dock = DockStyle.Fill;
					mvarChildControl.BringToFront();
				}
			}
		}

		private System.Drawing.Image mvarIcon = null;
		public System.Drawing.Image Icon { get { return mvarIcon; } set { mvarIcon = value; } }

		private string mvarText = String.Empty;
		public override string Text
		{
			get { return mvarText; }
			set { mvarText = value; }
		}

		void popoutPanelTitlebar_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			System.Windows.Forms.Panel panel = (sender as System.Windows.Forms.Panel);
			if (panel == null) return;

			System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, panel.Width, panel.Height);
			Theming.Theme.CurrentTheme.DrawDockPanelTitleBarBackground(e.Graphics, rect, mvarHasFocus);

			System.Drawing.Rectangle rectText = new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
			System.Windows.Forms.TextRenderer.DrawText(e.Graphics, Text, Font, rectText, mvarHasFocus ? Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabTextNormal : Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabText, System.Windows.Forms.TextFormatFlags.Left | System.Windows.Forms.TextFormatFlags.VerticalCenter);

		}

	}
}
