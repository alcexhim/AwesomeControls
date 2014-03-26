using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DynamicContainer
{
	public partial class DynamicContainerControl : UserControl
	{
		public DynamicContainerControl()
		{
			InitializeComponent();
			mvarWindows = new DynamicContainerWindow.DynamicContainerWindowCollection(this);
		}
		#region Collection Processing
		internal void ClearItems()
		{
			pnlContainer.Controls.Clear();
			tsbWindowList.DropDownItems.Clear();
		}

		internal void InsertItem(DynamicContainerWindow item)
		{
			if (!pnlContainer.Controls.Contains(item.Control)) pnlContainer.Controls.Add(item.Control);
			item.Control.Dock = DockStyle.Fill;

			ToolStripMenuItem tsmi = new ToolStripMenuItem();
			tsmi.Text = item.Title;
			if (item.Image != null)
			{
				tsmi.Image = item.Image;
			}
			tsmi.Tag = item;
			tsmi.Click += tsmi_Click;
			tsbWindowList.DropDownItems.Add(tsmi);

			if (tsbWindowList.DropDownItems.Count == 1) SwitchTo(item);
		}

		private void tsmi_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
			if (tsmi == null) return;

			DynamicContainerWindow wnd = (tsmi.Tag as DynamicContainerWindow);
			if (wnd == null) return;

			SwitchTo(wnd);
		}

		internal void RemoveItem(DynamicContainerWindow item)
		{
			if (pnlContainer.Controls.Contains(item.Control)) pnlContainer.Controls.Remove(item.Control);
			ToolStripMenuItem tsmiToRemove = null;
			foreach (ToolStripItem tsi in tsbWindowList.DropDownItems)
			{
				ToolStripMenuItem tsmi = (tsi as ToolStripMenuItem);
				if (tsmi == null) continue;

				if (tsmi.Tag == item)
				{
					tsmiToRemove = tsmi;
					break;
				}
			}
			tsbWindowList.DropDownItems.Remove(tsmiToRemove);
		}
		#endregion

		private DynamicContainerWindow.DynamicContainerWindowCollection mvarWindows = null;
		public DynamicContainerWindow.DynamicContainerWindowCollection Windows { get { return mvarWindows; } }

		private DynamicContainerWindow mvarCurrentWindow = null;
		public DynamicContainerWindow CurrentWindow
		{
			get { return mvarCurrentWindow; }
			set
			{
				if (mvarWindows.Contains(value)) SwitchTo(value);
			}
		}

		public void SwitchTo(DynamicContainerWindow item)
		{
			foreach (Control control in pnlContainer.Controls)
			{
				if (control == item.Control)
				{
					control.Enabled = true;
					control.Visible = true;
				}
				else
				{
					control.Visible = false;
					control.Enabled = false;
				}
			}

			foreach (ToolStripItem tsi in tsbWindowList.DropDownItems)
			{
				ToolStripMenuItem tsmi = (tsi as ToolStripMenuItem);
				if (tsmi == null) continue;

				tsmi.Checked = (tsmi.Tag == item);
			}
			mvarCurrentWindow = item;

			if (mvarCurrentWindow != null)
			{
				lblWindowTitle.Text = mvarCurrentWindow.Title;
			}
		}

		private void tb_MouseEnter(object sender, EventArgs e)
		{
			Cursor = Cursors.SizeAll;
		}

		private void tb_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private Point _prevPt = Point.Empty;
		private FloatingWindow wndFloater = null;
		public void ShowFloatingWindow(Point pt)
		{
			if (wndFloater == null) wndFloater = new FloatingWindow(this);
			if (wndFloater.IsDisposed) wndFloater = new FloatingWindow(this);

			wndFloater.Location = pt;
			wndFloater.ClientSize = ClientSize;

			if (!wndFloater.Visible)
			{
				wndFloater.Show(this.ParentForm);
			}
		}
		public void HideFloatingWindow()
		{
			if (wndFloater != null) wndFloater.Close();
		}

		private void tb_MouseMove(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.SizeAll;
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				Point pt = tb.PointToScreen(e.Location);
				Rectangle rect = RectangleToScreen(ClientRectangle);
				if (pt.X < rect.X || pt.Y < rect.Y || pt.X > rect.Right || pt.Y > rect.Bottom)
				{
					Visible = false;

					pt.Offset(_prevPt);
					ShowFloatingWindow(pt);
				}
				else if (!Visible)
				{
					HideFloatingWindow();
					Visible = true;
				}
			}
		}

		private void tsbWindowList_MouseEnter(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void tb_MouseDown(object sender, MouseEventArgs e)
		{
			_prevPt = new Point(-e.Location.X, -e.Location.Y);
		}

	}
}
