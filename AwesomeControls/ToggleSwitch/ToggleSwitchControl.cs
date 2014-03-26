using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.ToggleSwitch
{
	public partial class ToggleSwitchControl : UserControl
	{
		public ToggleSwitchControl()
		{
			InitializeComponent();
		}

		private bool mvarToggled = false;
		public bool Toggled { get { return mvarToggled; } set { mvarToggled = value; } }

		private string mvarText = String.Empty;
		[Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text { get { return mvarText; } set { mvarText = value; Refresh(); } }


		private ControlState state = ControlState.Normal;
		private bool _pressed = false;

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Rectangle rect = base.ClientRectangle;
			Rectangle buttonContainerRect = new Rectangle(rect.X, rect.Y, 78, rect.Height);
			Rectangle buttonRect = new Rectangle();
			Rectangle textRect = new Rectangle();

			if (mvarToggled)
			{
				buttonRect = new Rectangle(rect.X, rect.Y, 39, rect.Height);
				textRect = new Rectangle(rect.X + 39, rect.Y, 39, rect.Height);

				TextRenderer.DrawText(e.Graphics, "OFF", Font, textRect, Color.FromKnownColor(KnownColor.ControlText));
			}
			else
			{
				buttonRect = new Rectangle(rect.X + 39, rect.Y, 39, rect.Height);
				textRect = new Rectangle(rect.X, rect.Y, 39, rect.Height);

				e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedBackground), rect.X, rect.Y, 39, rect.Height);
				TextRenderer.DrawText(e.Graphics, "ON", Font, textRect, Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedForeground);
			}
			Theming.Theme.CurrentTheme.DrawButtonBackground(e.Graphics, buttonRect, state);

			e.Graphics.DrawRoundedRectangle(DrawingTools.Pens.ShadowPen, buttonContainerRect);

			textRect = new Rectangle(rect.X + 91, rect.Y, rect.Width - 91, rect.Height);
			TextRenderer.DrawText(e.Graphics, Text, Font, textRect, ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			state = ControlState.Pressed;
			_pressed = true;
			Refresh();
		}
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			if (_pressed)
			{
				state = ControlState.Pressed;
			}
			else
			{
				state = ControlState.Hover;
			}
			Refresh();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			state = ControlState.Normal;
			Refresh();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			mvarToggled = !mvarToggled;
			_pressed = false;
			state = ControlState.Hover;
			Refresh();
		}
	}
}
