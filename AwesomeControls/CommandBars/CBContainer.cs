// one line to give the program's name and an idea of what it does.
// Copyright (C) 2010  Mike Becker
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Collections.Generic;

namespace AwesomeControls.CommandBars
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class CBContainer : System.Windows.Forms.ToolStripContainer
	{
		public CBContainer()
		{
			base.TopToolStripPanel.Renderer = CBRenderer.Instance;
			base.BottomToolStripPanel.Renderer = CBRenderer.Instance;
			base.LeftToolStripPanel.Renderer = CBRenderer.Instance;
			base.RightToolStripPanel.Renderer = CBRenderer.Instance;
			base.ContentPanel.Renderer = CBRenderer.Instance;

			base.TopToolStripPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseDoubleClick);
			base.LeftToolStripPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseDoubleClick);
			base.BottomToolStripPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseDoubleClick);
			base.RightToolStripPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseDoubleClick);
			base.ContentPanel.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseDoubleClick);

			base.TopToolStripPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseUp);
			base.LeftToolStripPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseUp);
			base.BottomToolStripPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseUp);
			base.RightToolStripPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseUp);
			base.ContentPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(ToolStripPanel_MouseUp);
		}

		private void ToolStripPanel_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ShowCustomizeDialog();
			}
		}
		private void ToolStripPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				// TODO: Display the toolbar context menu

				CBContextMenu mnu = new CBContextMenu();
				System.Windows.Forms.ToolStripContainer panel = this;
				foreach (System.Windows.Forms.Control ctl in panel.TopToolStripPanel.Controls)
				{
					if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip))
					{
						System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
						mnuItem.CheckOnClick = true;
						mnuItem.Checked = ctl.Visible;
						mnuItem.Tag = ctl;
						mnu.Items.Add(mnuItem);
					}
				}
				foreach (System.Windows.Forms.Control ctl in panel.BottomToolStripPanel.Controls)
				{
					if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip))
					{
						System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
						mnuItem.CheckOnClick = true;
						mnuItem.Checked = ctl.Visible;
						mnuItem.Tag = ctl;
						mnu.Items.Add(mnuItem);
					}
				}
				foreach (System.Windows.Forms.Control ctl in panel.LeftToolStripPanel.Controls)
				{
					if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip))
					{
						System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
						mnuItem.CheckOnClick = true;
						mnuItem.Checked = ctl.Visible;
						mnuItem.Tag = ctl;
						mnu.Items.Add(mnuItem);
					}
				}
				foreach (System.Windows.Forms.Control ctl in panel.RightToolStripPanel.Controls)
				{
					if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip))
					{
						System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
						mnuItem.CheckOnClick = true;
						mnuItem.Checked = ctl.Visible;
						mnuItem.Tag = ctl;
						mnu.Items.Add(mnuItem);
					}
				}

				mnu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
				mnu.Items.Add("&Customize...", null, mnuViewToolbarsCustomize_Click);

				mnu.Show(PointToScreen(e.Location));
			}
		}

		private void mnuViewToolbarsToolbar_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.ToolStripMenuItem tsmi = (sender as System.Windows.Forms.ToolStripMenuItem);
			System.Windows.Forms.ToolStrip tb = (tsmi.Tag as System.Windows.Forms.ToolStrip);
			if (tb != null) tb.Visible = tsmi.Checked;
		}

		public System.Windows.Forms.DialogResult ShowCustomizeDialog()
		{
			CustomizeCommandBarDialog dlg = new CustomizeCommandBarDialog();
			if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				return System.Windows.Forms.DialogResult.OK;
			}
			return System.Windows.Forms.DialogResult.Cancel;
		}

		public CBContextMenu BuildToolbarContextMenu()
		{
			CBContextMenu mnu = new CBContextMenu();
			foreach (System.Windows.Forms.Control ctl in TopToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) /* && !(ctl is System.Windows.Forms.MenuStrip) */)
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					mnu.Items.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in BottomToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) /* && !(ctl is System.Windows.Forms.MenuStrip) */)
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					mnu.Items.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in LeftToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) /* && !(ctl is System.Windows.Forms.MenuStrip) */)
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					mnu.Items.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in RightToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) /* && !(ctl is System.Windows.Forms.MenuStrip) */)
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, mnuViewToolbarsToolbar_Click);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					mnu.Items.Add(mnuItem);
				}
			}

			mnu.Items.Add(new System.Windows.Forms.ToolStripSeparator());
			mnu.Items.Add("&Customize...", null, mnuViewToolbarsCustomize_Click);
			return mnu;
		}

		private void mnuViewToolbarsCustomize_Click(object sender, EventArgs e)
		{
			ShowCustomizeDialog();
		}

		public void BuildToolbarDropDownItems(EventHandler onClick, System.Windows.Forms.ToolStripItemCollection dropDownItems)
		{
			BuildToolbarDropDownItems(this, onClick, dropDownItems);
		}
		public static void BuildToolbarDropDownItems(System.Windows.Forms.ToolStripContainer panel, EventHandler onClick, System.Windows.Forms.ToolStripItemCollection dropDownItems)
		{
			foreach (System.Windows.Forms.Control ctl in panel.TopToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) && !(ctl is System.Windows.Forms.MenuStrip))
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, onClick);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					dropDownItems.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in panel.BottomToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) && !(ctl is System.Windows.Forms.MenuStrip))
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, onClick);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					dropDownItems.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in panel.LeftToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) && !(ctl is System.Windows.Forms.MenuStrip))
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, onClick);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					dropDownItems.Add(mnuItem);
				}
			}
			foreach (System.Windows.Forms.Control ctl in panel.RightToolStripPanel.Controls)
			{
				if ((ctl is System.Windows.Forms.ToolStrip) && !(ctl is System.Windows.Forms.StatusStrip) && !(ctl is System.Windows.Forms.MenuStrip))
				{
					System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(ctl.Text, null, onClick);
					mnuItem.CheckOnClick = true;
					mnuItem.Checked = ctl.Visible;
					mnuItem.Tag = ctl;
					dropDownItems.Add(mnuItem);
				}
			}
		}

		public void BuildThemesDropDownItems(System.Windows.Forms.ToolStripItemCollection dropDownItems)
		{
			foreach (Theming.Theme theme in Theming.Theme.Get())
			{
				System.Windows.Forms.ToolStripMenuItem mnuItem = new System.Windows.Forms.ToolStripMenuItem(theme.Title, null, mnuViewThemesTheme_Click);
				mnuItem.CheckOnClick = true;
				mnuItem.Checked = (Theming.Theme.CurrentTheme == theme);
				mnuItem.Tag = theme;
				dropDownItems.Add(mnuItem);
			}
		}
		private void mnuViewThemesTheme_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.ToolStripItem item = (sender as System.Windows.Forms.ToolStripItem);
			Theming.Theme theme = (item.Tag as Theming.Theme);
			if (theme != null)
			{
				foreach (System.Windows.Forms.ToolStripItem item1 in item.GetCurrentParent().Items)
				{
					if (item1 is System.Windows.Forms.ToolStripMenuItem) (item1 as System.Windows.Forms.ToolStripMenuItem).Checked = false;
				}

				if (item is System.Windows.Forms.ToolStripMenuItem) (item as System.Windows.Forms.ToolStripMenuItem).Checked = true;
				Theming.Theme.CurrentTheme = theme;

				Refresh();
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			if (mvarUseCommandManager)
			{
				RemoveToolStripsAndMenuStrips(this.TopToolStripPanel);
				RemoveToolStripsAndMenuStrips(this.BottomToolStripPanel);
				RemoveToolStripsAndMenuStrips(this.LeftToolStripPanel);
				RemoveToolStripsAndMenuStrips(this.RightToolStripPanel);
			}
		}

		private void RemoveToolStripsAndMenuStrips(System.Windows.Forms.ToolStripPanel toolStripPanel)
		{
			for (int i = 0; i < toolStripPanel.Controls.Count; i++)
			{
				if ((!(toolStripPanel.Controls[i] is System.Windows.Forms.StatusStrip))
					&& ((FindForm() != null) && FindForm().MainMenuStrip != toolStripPanel.Controls[i]))
				{
					toolStripPanel.Controls.RemoveAt(i);
					i--;
				}
			}
		}

		#region Command Manager
		private bool mvarUseCommandManager = false;
		/// <summary>
		/// Determines whether the <see cref="CBContainer" /> loads its initial state from the
		/// <see cref="CommandManager" />, ignoring the changes made by the user in the designer.
		/// </summary>
		public bool UseCommandManager
		{
			get { return mvarUseCommandManager; }
			set { mvarUseCommandManager = value; }
		}
		#endregion
	}
}