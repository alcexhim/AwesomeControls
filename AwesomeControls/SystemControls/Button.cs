using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace AwesomeControls.SystemControls
{
	public partial class Button : System.Windows.Forms.Button
	{
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Theming.Theme.CurrentTheme.DrawButtonBackground(e.Graphics, ClientRectangle, mvarState);
		}

		private ControlState mvarState = ControlState.Normal;
		public ControlState State { get { return mvarState; } }

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);

			if (base.Enabled)
			{
				mvarState &= ~ControlState.Disabled;
			}
			else
			{
				mvarState |= ControlState.Disabled;
			}
		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);

			mvarState |= ControlState.Hover;
			Refresh();
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);

			mvarState &= ~ControlState.Hover;
			Refresh();
		}
	}
}
