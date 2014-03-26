using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.DockingWindows
{
	public delegate void WindowClosedEventHandler(object sender, WindowClosedEventArgs e);
	public class WindowClosedEventArgs : EventArgs
	{
		private DockingWindow mvarWindow = null;
		public DockingWindow Window { get { return mvarWindow; } }

		public WindowClosedEventArgs(DockingWindow window)
		{
			mvarWindow = window;
		}
	}
}
