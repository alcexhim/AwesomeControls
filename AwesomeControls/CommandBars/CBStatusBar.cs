using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.CommandBars
{
	public class CBStatusBar : System.Windows.Forms.StatusStrip
	{
		public CBStatusBar()
		{
			base.Renderer = CBRenderer.Instance;
		}
	}
}
