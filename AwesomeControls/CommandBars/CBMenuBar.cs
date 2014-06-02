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
	/// Description of CBMenuBar.
	/// </summary>
	public class CBMenuBar : System.Windows.Forms.MenuStrip, ICommandBar
	{
		public CBMenuBar()
		{
			base.Renderer = CBRenderer.Instance;
		}
		CBWindow wnd = null;

		protected override System.Drawing.Size DefaultSize
		{
			get
			{
				return new System.Drawing.Size(base.DefaultSize.Width, 25);
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			// update menu open handlers
			UpdateOpenedHandlersIfNecessary();
		}

		private ToolStripDropDown mvarCurrentDropDown = null;
		/// <summary>
		/// Gets the currently open <see cref="ToolStripDropDown" /> on this menu.
		/// </summary>
		public ToolStripDropDown CurrentDropDown { get { return mvarCurrentDropDown; } }

		private System.Collections.Generic.Dictionary<ToolStripItem, bool> OpenedHandlerSetup = new System.Collections.Generic.Dictionary<ToolStripItem, bool>();
		private void UpdateOpenedHandlersIfNecessary()
		{
			foreach (ToolStripItem tsi in Items)
			{
				if (tsi is ToolStripDropDownItem)
				{
					ToolStripDropDownItem tsddi = (tsi as ToolStripDropDownItem);
					if (!OpenedHandlerSetup.ContainsKey(tsi))
					{
                        tsddi.DropDownOpening += tsddi_DropDownOpening;
						tsddi.DropDownOpened += tsddi_DropDownOpened;
						tsddi.DropDownClosed += tsddi_DropDownClosed;
						if (tsddi.DropDown.Visible) mvarCurrentDropDown = tsddi.DropDown;
						OpenedHandlerSetup[tsi] = true;
					}
				}
			}
		}

		void tsddi_DropDownClosed(object sender, EventArgs e)
		{
			mvarSpaceSaverMenusExpanded = false;
		}

		private bool mvarSpaceSaverMenusExpanded = false;

        private void tsddi_DropDownOpening(object sender, EventArgs e)
        {
            mvarCurrentDropDown = (sender as ToolStripDropDownItem).DropDown;
            if (CurrentDropDown == null) return;

            #region SpaceSaver Menus
            if (Theming.Theme.CurrentTheme.EnableSpaceSaverMenus)
            {
                foreach (ToolStripItem tsi in CurrentDropDown.Items)
                {
                    if (tsi is CBMenuItem)
                    {
                        CBMenuItem cbmi = (tsi as CBMenuItem);
                        if (cbmi.Hidden) cbmi.Visible = DesignMode;
                    }
                }
            }
            #endregion
        }
		private void tsddi_DropDownOpened(object sender, EventArgs e)
		{
			mvarCurrentDropDown = (sender as ToolStripDropDownItem).DropDown;
			if (CurrentDropDown == null) return;

			#region Menu Animations
            if (Theming.Theme.CurrentTheme.CommandBarMenuAnimationType == Theming.CommandBarMenuAnimationType.Fade && !DesignMode)
			{
				mvarCurrentDropDown.AllowTransparency = true;
				mvarCurrentDropDown.Opacity = 0.0;

				while (mvarCurrentDropDown.Opacity < 1.0)
				{
					mvarCurrentDropDown.Opacity += 0.1;
					Application.DoEvents();
					System.Threading.Thread.Sleep(30);
				}
			}
			#endregion
			#region SpaceSaver Menus
            if (Theming.Theme.CurrentTheme.EnableSpaceSaverMenus && !DesignMode)
			{
				if (mvarSpaceSaverMenusExpanded)
				{
					NewMenuThread();
				}
				else
				{
					if (tNewMenuThread != null)
					{
						tNewMenuThread.Abort();
					}
					tNewMenuThread = new System.Threading.Thread(tNewMenuThread_ThreadStart);
					tNewMenuThread.Start();
				}
			}
			#endregion
		}

		private System.Threading.Thread tNewMenuThread = null;

		private void tNewMenuThread_ThreadStart()
		{
			System.Threading.Thread.Sleep(2000);
            try
            {
                Invoke(new Action(NewMenuThread));
            }
            catch (InvalidOperationException ex)
            {
            }
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);

			if (tNewMenuThread != null)
			{
				tNewMenuThread.Abort();
			}
		}

		private void NewMenuThread()
		{
			foreach (ToolStripItem tsi in CurrentDropDown.Items)
			{
				if (tsi is CBMenuItem)
				{
					CBMenuItem cbmi = (tsi as CBMenuItem);
					cbmi.Visible = true;
				}
			}
			mvarSpaceSaverMenusExpanded = true;
		}

		private bool m_Dragging = false;
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			if (mea.Button == System.Windows.Forms.MouseButtons.Left)
			{
				m_Dragging = true;
			}
			else if (mea.Button == System.Windows.Forms.MouseButtons.Right)
			{
				// TODO: Display the toolbar context menu
				CBContainer container = GetContainer();
				if (container == null) return;

				CBContextMenu mnu = container.BuildToolbarContextMenu();
				if (mnu != null) mnu.Show(PointToScreen(mea.Location));
			}
		}

		private CBContainer GetContainer()
		{
			ToolStripPanel panel = (this.Parent as ToolStripPanel);
			if (panel == null) return null;

			ToolStripContainer container = (panel.Parent as ToolStripContainer);
			if (container == null) return null;

			return (container as CBContainer);
		}

		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			if (m_Dragging)
			{
				if (System.Windows.Forms.Cursor.Position.X < base.FindForm().Left || System.Windows.Forms.Cursor.Position.Y < base.FindForm().Top
					|| System.Windows.Forms.Cursor.Position.X > base.FindForm().Right || System.Windows.Forms.Cursor.Position.Y > base.FindForm().Bottom)
				{
					m_Dragging = false;
					Detach();
					base.OnMouseUp(mea);
				}
			}
		}

		public void Attach()
		{
			base.Visible = true;
			wnd.Visible = false;
			wnd.Dispose();
			wnd = null;

			FindForm().Focus();
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

			CBMenuBar tb = new CBMenuBar();
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
