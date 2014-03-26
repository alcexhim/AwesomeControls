using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.GanttChart
{
    public class GanttTask
    {
        public class GanttTaskCollection
            : System.Collections.ObjectModel.Collection<GanttTask>
        {
        }

        private int mvarHeight = 24;
        public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

        private GanttTaskCollection mvarTasks = new GanttTaskCollection();
        public GanttTaskCollection Tasks { get { return mvarTasks; } }

        private System.Drawing.Font mvarFont = null;
        public System.Drawing.Font Font { get { return mvarFont; } set { mvarFont = value; } }
    }
}
