using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public delegate void DesignerObjectLocationChangingEventHandler(object sender, DesignerObjectLocationChangingEventArgs e);
    public class DesignerObjectLocationChangingEventArgs : System.ComponentModel.CancelEventArgs
    {
        private DesignerObject mvarItem = null;
        public DesignerObject Item { get { return mvarItem; } }

        private System.Drawing.Point mvarOldLocation = System.Drawing.Point.Empty;
        public System.Drawing.Point OldLocation { get { return mvarOldLocation; } }

        private System.Drawing.Point mvarNewLocation = System.Drawing.Point.Empty;
        public System.Drawing.Point NewLocation { get { return mvarNewLocation; } set { mvarNewLocation = value; } }

        public DesignerObjectLocationChangingEventArgs(DesignerObject item, System.Drawing.Point oldLocation, System.Drawing.Point newLocation)
        {
            mvarItem = item;
            mvarOldLocation = oldLocation;
            mvarNewLocation = newLocation;
        }
    }
    public delegate void DesignerObjectLocationChangedEventHandler(object sender, DesignerObjectLocationChangedEventArgs e);
    public class DesignerObjectLocationChangedEventArgs
    {
        private DesignerObject mvarItem = null;
        public DesignerObject Item { get { return mvarItem; } }

        private System.Drawing.Point mvarOldLocation = System.Drawing.Point.Empty;
        public System.Drawing.Point OldLocation { get { return mvarOldLocation; } }

        private System.Drawing.Point mvarNewLocation = System.Drawing.Point.Empty;
        public System.Drawing.Point NewLocation { get { return mvarNewLocation; } }

        public DesignerObjectLocationChangedEventArgs(DesignerObject item, System.Drawing.Point oldLocation, System.Drawing.Point newLocation)
        {
            mvarItem = item;
            mvarOldLocation = oldLocation;
            mvarNewLocation = newLocation;
        }
    }
}
