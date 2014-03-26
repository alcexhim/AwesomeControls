using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.GanttChart
{
    public class GanttColumn
    {
        public class GanttColumnCollection
            : System.Collections.ObjectModel.Collection<GanttColumn>
        {
        }

        private int mvarWidth = 0;
        public int Width { get { return mvarWidth; } set { mvarWidth = value; } }


    }
}
