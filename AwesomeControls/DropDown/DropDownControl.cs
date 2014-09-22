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
	[DefaultEvent("PaintContent"), Designer(typeof(DropDownControlDesigner))]
	public partial class DropDownControl : UserControl
	{
		public DropDownControl()
		{
			InitializeComponent();
			base.DoubleBuffered = true;
		}

		/// <summary>
		/// Paints the content of the dropdown.
		/// </summary>
		public event PaintEventHandler PaintContent;
		protected virtual void OnPaintContent(PaintEventArgs e)
		{
			if (PaintContent != null) PaintContent(this, e);
		}

		private Control mvarCustomDropDownControl = null;
		public Control CustomDropDownControl
		{
			get { return mvarCustomDropDownControl; } 
			set
			{
				if (value != null)
				{
					if (value == this)
					{
						throw new ArgumentException("Cannot use myself as my own custom dropdown control");
					}
					else if (value == ParentForm)
					{
						throw new ArgumentException("Cannot use my parent form as a custom dropdown control");
					}
				}
				mvarCustomDropDownControl = value; 
			}
		}

		private ListView.ListViewItem.ListViewItemCollection mvarDropDownItems = new ListView.ListViewItem.ListViewItemCollection();
		public ListView.ListViewItem.ListViewItemCollection DropDownItems { get { return mvarDropDownItems; } }

		private bool mvarInitialDropDownWindowSizeSet = false;

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
						if (!mvarInitialDropDownWindowSizeSet)
						{
							mvarDropDownWindow.Width = this.Width;
							mvarInitialDropDownWindowSizeSet = true;
						}
						if (mvarCustomDropDownControl != null)
						{
							// if (mvarCustomDropDownControl.IsDisposed) mvarCustomDropDownControl = (Control)(mvarCustomDropDownControl.GetType().Assembly.CreateInstance(mvarCustomDropDownControl.GetType().FullName));
							mvarDropDownWindow.Controls.Add(mvarCustomDropDownControl);
							mvarCustomDropDownControl.Visible = true;
							mvarCustomDropDownControl.Dock = DockStyle.Fill;
						}
						mvarDropDownWindow.Show(ParentForm);
						break;
					}
					case false:
					{
						mvarDropDownWindow.Hide();
						break;
					}
				}
				Invalidate();
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				mvarControlState = ControlState.Pressed;
				Invalidate();

				DropDownVisible = !DropDownVisible;
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				mvarControlState = ControlState.Normal;
				Invalidate();
			}
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
			Theming.Theme.CurrentTheme.DrawDropDownBackground(e.Graphics, new Rectangle(0, 0, Width - 1, Height - 1), mvarControlState);

			Rectangle rectDropDownButton = new Rectangle(Width - Theming.Theme.CurrentTheme.MetricTable.DropDownButtonWidth - Theming.Theme.CurrentTheme.MetricTable.DropDownButtonPadding.Right, Theming.Theme.CurrentTheme.MetricTable.DropDownButtonPadding.Top, Theming.Theme.CurrentTheme.MetricTable.DropDownButtonWidth, Height - Theming.Theme.CurrentTheme.MetricTable.DropDownButtonPadding.Top - Theming.Theme.CurrentTheme.MetricTable.DropDownButtonPadding.Bottom);
			Theming.Theme.CurrentTheme.DrawDropDownButton(e.Graphics, rectDropDownButton, mvarControlState);

			OnPaintContent(new PaintEventArgs(e.Graphics, new Rectangle(2, 2, Width - 4, Height - 4)));
		}
	}
}
