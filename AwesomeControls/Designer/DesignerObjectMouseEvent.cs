using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public delegate void DesignerObjectMouseEventHandler(object sender, DesignerObjectMouseEventArgs e);
    public class DesignerObjectMouseEventArgs : System.Windows.Forms.MouseEventArgs
    {
        private DesignerObject mvarItem = null;
        public DesignerObject Item { get { return mvarItem; } }

        public DesignerObjectMouseEventArgs(DesignerObject item, System.Windows.Forms.MouseButtons button, int clicks, int x, int y, int delta)
            : base(button, clicks, x, y, delta)
        {
            mvarItem = item;
        }
    }
}
