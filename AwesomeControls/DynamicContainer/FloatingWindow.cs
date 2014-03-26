using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DynamicContainer
{
	public partial class FloatingWindow : Form
	{
		public FloatingWindow()
		{
			InitializeComponent();
		}

		private DynamicContainerControl _parent = null;
		public FloatingWindow(DynamicContainerControl parent) : this()
		{
			_parent = parent;

			if (parent.CurrentWindow != null)
			{
				Text = parent.CurrentWindow.Title;

				parent.pnlContainer.Controls.Remove(parent.CurrentWindow.Control);
				this.Controls.Add(parent.CurrentWindow.Control);
			}
		}

		private struct RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}
		private struct POINTS
		{
			public short x;
			public short y;
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 0x0232)
			{
				// WM_EXITSIZEMOVE
				OnMoved(EventArgs.Empty);
			}
		}

		protected virtual void OnMoved(EventArgs e)
		{
			Point pt = Location;
			Rectangle rect = _parent.RectangleToScreen(_parent.ClientRectangle);
			if (pt.X < rect.X || pt.Y < rect.Y || pt.X > rect.Right || pt.Y > rect.Bottom)
			{
				_parent.Visible = false;

				// pt.Offset(_prevPt);
				_parent.ShowFloatingWindow(pt);
			}
			else
			{
				_parent.HideFloatingWindow();
				_parent.Visible = true;
			}
		}

		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			Point pt = this.Location;
			Rectangle rect = _parent.RectangleToScreen(_parent.ClientRectangle);
			if (pt.X < rect.X || pt.Y < rect.Y || pt.X > rect.Right || pt.Y > rect.Bottom)
			{
				_parent.Visible = false;

				// pt.Offset(_prevPt);
				_parent.ShowFloatingWindow(pt);
			}
			else if (!_parent.Visible)
			{
				// _parent.HideFloatingWindow();
				_parent.Visible = true;
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_parent.CurrentWindow != null)
			{
				this.Controls.Remove(_parent.CurrentWindow.Control);
				_parent.pnlContainer.Controls.Add(_parent.CurrentWindow.Control);

				_parent.Visible = true;
			}
			base.OnClosing(e);
		}
	}
}
