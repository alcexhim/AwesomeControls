using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public delegate void DesignerObjectSelectedEventHandler(object sender, DesignerObjectSelectedEventArgs e);
    public class DesignerObjectSelectedEventArgs : EventArgs
    {
        private DesignerObject mvarItem = null;
        public DesignerObject Item { get { return mvarItem; } }

        public DesignerObjectSelectedEventArgs(DesignerObject item)
        {
            mvarItem = item;
        }
    }
}
