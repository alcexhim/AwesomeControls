using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.CommandBars
{
	public class CBContextMenu : ContextMenuStrip
	{
		public CBContextMenu()
		{
			base.Renderer = CBRenderer.Instance;
		}
		public CBContextMenu(System.ComponentModel.IContainer container) : base(container)
		{
			base.Renderer = CBRenderer.Instance;
		}
	}
}
