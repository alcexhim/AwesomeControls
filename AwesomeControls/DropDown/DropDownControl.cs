using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.DropDown
{
	public partial class DropDownControl : UserControl
	{
		public DropDownControl()
		{
			InitializeComponent();
			base.DoubleBuffered = true;
		}

		private DropDownWindow mvarDropDownWindow = null;
		public bool DropDownVisible
		{
			get
			{
				if (mvarDropDownWindow != null && !mvarDropDownWindow.IsDisposed) return mvarDropDownWindow.Visible;
				return false;
			}
			set
			{
				if (mvarDropDownWindow == null || mvarDropDownWindow.IsDisposed) mvarDropDownWindow = new DropDownWindow();
				switch (value)
				{
					case true:
					{
						if (mvarDropDownWindow.Visible) mvarDropDownWindow.Hide();
						mvarDropDownWindow.Location = PointToScreen(new Point(0, Height));
						mvarDropDownWindow.Width = this.Width;
						mvarDropDownWindow.Show(ParentForm);
						break;
					}
					case false:
					{
						mvarDropDownWindow.Close();
						break;
					}
				}
				Invalidate();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			mvarControlState = ControlState.Pressed;
			Invalidate();

			DropDownVisible = true;
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			mvarControlState = ControlState.Normal;
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);

			mvarControlState = ControlState.Hover;
			Invalidate();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			if (!DropDownVisible)
			{
				mvarControlState = ControlState.Normal;
				Invalidate();
			}
		}

		private ControlState mvarControlState = ControlState.Normal;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			switch (mvarControlState)
			{
				case ControlState.Normal: e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.DropDownBackgroundColorNormal); break;
				case ControlState.Hover: e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.DropDownBackgroundColorHover); break;
				case ControlState.Pressed: e.Graphics.Clear(Theming.Theme.CurrentTheme.ColorTable.DropDownBackgroundColorPressed); break;
			}
			Theming.Theme.CurrentTheme.DrawDropDownBackground(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1), mvarControlState);

			Rectangle rectDropDownButton = new Rectangle(Width - Theming.Theme.CurrentTheme.MetricTable.DropDownButtonWidth - 2, 2, Theming.Theme.CurrentTheme.MetricTable.DropDownButtonWidth, Height - 4);
			Theming.Theme.CurrentTheme.DrawDropDownButton(e.Graphics, rectDropDownButton, mvarControlState);
		}
	}
}
