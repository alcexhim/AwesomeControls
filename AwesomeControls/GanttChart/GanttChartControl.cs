using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.GanttChart
{
    public partial class GanttChartControl : UserControl
    {
        public GanttChartControl()
        {
            InitializeComponent();
            base.DoubleBuffered = true;
        }

        private GanttTask.GanttTaskCollection mvarTasks = new GanttTask.GanttTaskCollection();
        public GanttTask.GanttTaskCollection Tasks { get { return mvarTasks; } }

        private GanttColumn.GanttColumnCollection mvarColumns = new GanttColumn.GanttColumnCollection();
        public GanttColumn.GanttColumnCollection Columns { get { return mvarColumns; } }

        private int GetStartOfGanttChart()
        {
            int i = 0;
            foreach (GanttColumn col in mvarColumns)
            {
                i += col.Width;
            }
            return i;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw the column headers
            foreach (GanttColumn col in mvarColumns)
            {
            }

            int y = 0;
            foreach (GanttTask task in mvarTasks)
            {
                DrawTask(e.Graphics, task, new int[] { mvarTasks.IndexOf(task) }, ref y);
            }
        }

        private void DrawTask(Graphics g, GanttTask task, int[] indices, ref int y)
        {
            Font font = task.Font;
            if (font == null) font = Font;

            Rectangle rect = new Rectangle(0, 0, GetStartOfGanttChart(), task.Height);
            for (int i = 0; i < indices.Length; i++)
            {
                TextRenderer.DrawText(g, indices[i].ToString(), font, rect, ForeColor);
                if (i < indices.Length - 1)
                {
                    TextRenderer.DrawText(g, ".", font, rect, ForeColor);
                }
            }


            
            y += task.Height;
            foreach (GanttTask task1 in task.Tasks)
            {
                int[] indices2 = new int[indices.Length + 1];
                Array.Copy(indices, 0, indices2, 0, indices.Length);
                indices2[indices2.Length - 1] = task.Tasks.IndexOf(task1);

                DrawTask(g, task1, indices2, ref y);
            }
        }
    }
}
