using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.CommandBars
{
	public class CBMenuItem : System.Windows.Forms.ToolStripMenuItem
	{
		private bool mvarVisible = true;

		private bool mvarHidden = false;
		/// <summary>
		/// Determines whether this <see cref="CBMenuItem" /> should default to hidden when SpaceSaver
		/// menus are enabled and the user has not yet triggered a change in SpaceSaver behavior.
		/// </summary>
		public bool Hidden
		{
			get { return mvarHidden; }
			set
			{
				mvarHidden = value;
				if (mvarHidden)
				{
					mvarVisible = Visible;
					Visible = false;
				}
				else
				{
					Visible = mvarVisible;
				}
			}
		}
	}
}
