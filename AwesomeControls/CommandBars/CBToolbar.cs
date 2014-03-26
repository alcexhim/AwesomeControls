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
using System.Windows.Forms;

namespace AwesomeControls.CommandBars
{
	/// <summary>
	/// Description of CommandBar.
	/// </summary>
	public class CBToolBar : System.Windows.Forms.ToolStrip, ICommandBar
    {
        private CBContainer GetContainer()
        {
            ToolStripPanel panel = (this.Parent as ToolStripPanel);
            if (panel == null) return null;

            ToolStripContainer container = (panel.Parent as ToolStripContainer);
            if (container == null) return null;

            return (container as CBContainer);
        }

		CBWindow wnd = null;

		private System.Windows.Forms.Timer tmrHover = null;

		public CBToolBar()
		{
			base.Renderer = CBRenderer.Instance;
			
			tmrHover = new Timer();
			tmrHover.Interval = 1;
			tmrHover.Tick += new EventHandler(tmrHover_Tick);
		}

		void tmrHover_Tick(object sender, EventArgs e)
		{
			if (wnd != null)
			{
				if (Control.MouseButtons == System.Windows.Forms.MouseButtons.None)
				{
					tmrHover.Enabled = false;
				}

				wnd.Location = Cursor.Position;

				if ((Cursor.Position.X > mvarLastKnownToolstripPanel.Left && Cursor.Position.X < mvarLastKnownToolstripPanel.Top)
					|| (Cursor.Position.Y > mvarLastKnownToolstripPanel.Top && Cursor.Position.Y < mvarLastKnownToolstripPanel.Bottom))
				{
					Attach();

					tmrHover.Enabled = false;
				}
			}
		}
		
		private bool m_Dragging = false;
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			if (mea.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (GripStyle == ToolStripGripStyle.Visible && mea.X < 8)
				{
					m_DraggingStarted = true;
					m_Dragging = false;
				}
			}
			else if (mea.Button == System.Windows.Forms.MouseButtons.Right)
			{
				// TODO: Display the toolbar context menu
                CBContainer container = GetContainer();
                if (container == null) return;

				CBContextMenu mnu = container.BuildToolbarContextMenu();
                mnu.Show(PointToScreen(mea.Location));
			}
		}

		private void mnuViewToolbarsCustomize_Click(object sender, EventArgs e)
		{

		}

		private void mnuViewToolbarsToolbar_Click(object sender, EventArgs e)
		{
			System.Windows.Forms.ToolStripMenuItem tsmi = (sender as System.Windows.Forms.ToolStripMenuItem);
			System.Windows.Forms.ToolStrip tb = (tsmi.Tag as System.Windows.Forms.ToolStrip);
			if (tb != null) tb.Visible = tsmi.Checked;
		}

		private bool m_DraggingStarted = false;
		private ToolStripPanel mvarLastKnownToolstripPanel = null;


		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			if (m_DraggingStarted)
			{
				ToolStripPanel panel = (base.Parent as ToolStripPanel);

				bool found = false;
				if (panel != null)
				{
					mvarLastKnownToolstripPanel = panel;
					if (Cursor.Position.X < panel.Left || Cursor.Position.Y < panel.Top || Cursor.Position.X > panel.Right || Cursor.Position.Y > panel.Bottom)
					{
						found = true;
					}
				}
				else
				{
					if (System.Windows.Forms.Cursor.Position.X < base.FindForm().Left || System.Windows.Forms.Cursor.Position.Y < base.FindForm().Top
						|| System.Windows.Forms.Cursor.Position.X > base.FindForm().Right || System.Windows.Forms.Cursor.Position.Y > base.FindForm().Bottom)
					{
						found = true;
					}
				}
				if (found)
				{
					m_DraggingStarted = false;
					Detach();

					tmrHover.Enabled = true;
				}
			}
			else if (m_Dragging && (wnd != null))
			{
				wnd.Location = System.Windows.Forms.Cursor.Position;
			}
		}
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			m_Dragging = false;

			tmrHover.Enabled = false;
		}
		
		public void Attach()
		{
			base.Visible = true;
			wnd.Visible = false;
			wnd.Dispose();
			wnd = null;

			Form form = FindForm();
			if (form != null) form.Focus();
		}
		public void Detach()
		{
			bool createdNew = false;
			if (wnd == null)
			{
				wnd = new CBWindow(this);
				createdNew = true;
			}
			if (wnd.IsDisposed) 
			{
				wnd = new CBWindow(this);
				createdNew = true;
			}
			if (createdNew)
			{
				/*
				wnd.Activated += delegate { wnd.Show(FindForm()); };
				wnd.Deactivate += delegate { wnd.Hide(); };
				FindForm().Deactivate += delegate { wnd.Hide(); };
				FindForm().Activated += delegate { wnd.Show(FindForm()); };
				*/
			}
			base.Visible = false;
			wnd.Text = this.Text;

			CBToolBar tb = new CBToolBar();
			tb.Text = base.Text;
			foreach (ToolStripItem tsi in base.Items)
			{
				ToolStripItem tsi1 = tsi.Clone();
				tb.Items.Add(tsi1);
			}
			tb.Dock = System.Windows.Forms.DockStyle.Fill;
			tb.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			tb.Visible = true;
			tb.Parent = null;
			wnd.Controls.Add(tb);

			wnd.Location = System.Windows.Forms.Cursor.Position;

			if (!wnd.Visible) wnd.Show(FindForm());
			wnd.BringToFront();
		}
	}
}
