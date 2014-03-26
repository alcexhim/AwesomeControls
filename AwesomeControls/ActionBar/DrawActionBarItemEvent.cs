using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.ActionBar
{
    public delegate void DrawActionBarItemEventHandler(object sender, DrawActionBarItemEventArgs e);
    public class DrawActionBarItemEventArgs : System.Windows.Forms.PaintEventArgs
    {
        private ActionBarItem mvarItem = null;
        public ActionBarItem Item { get { return mvarItem; } }

        public DrawActionBarItemEventArgs(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipRect, ActionBarItem item) : base(graphics, clipRect)
        {
            mvarItem = item;
        }
    }
}
