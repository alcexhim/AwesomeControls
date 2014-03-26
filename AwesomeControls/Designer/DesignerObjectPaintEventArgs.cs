using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public class DesignerObjectPaintEventArgs : System.Windows.Forms.PaintEventArgs
    {
        private DesignerObject mvarItem = null;
        public DesignerObject Item { get { return mvarItem; } }

        public DesignerObjectPaintEventArgs(DesignerObject item, System.Drawing.Graphics graphics, System.Drawing.Rectangle clipRect) : base(graphics, clipRect)
        {
            mvarItem = item;
        }
    }
}
