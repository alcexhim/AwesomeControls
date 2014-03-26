using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AwesomeControls
{
	public delegate void MovingEventHandler(object sender, MovingEventArgs e);
	public class MovingEventArgs : EventArgs
	{
		private Rectangle mvarBounds = new Rectangle();
		public Rectangle Bounds { get { return mvarBounds; } }

		public MovingEventArgs(Rectangle rect)
		{
			mvarBounds = rect;
		}
	}
}
