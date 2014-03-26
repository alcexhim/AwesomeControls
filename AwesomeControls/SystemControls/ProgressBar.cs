using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AwesomeControls.SystemControls
{
    public partial class ProgressBar : System.Windows.Forms.Control
    {
        public ProgressBar()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        public ProgressBar(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }

        private System.Windows.Forms.Orientation mvarOrientation = System.Windows.Forms.Orientation.Horizontal;
        public System.Windows.Forms.Orientation Orientation { get { return mvarOrientation; } set { mvarOrientation = value; } }

        private int mvarMinimum = 0;
        public int Minimum { get { return mvarMinimum; } set { mvarMinimum = value; } }

        private int mvarMaximum = 100;
        public int Maximum { get { return mvarMaximum; } set { if (value < 1) value = 1; mvarMaximum = value; } }

        private int mvarValue = 0;
        public int Value { get { return mvarValue; } set { mvarValue = value; } }

        private System.Windows.Forms.ProgressBarStyle mvarStyle = System.Windows.Forms.ProgressBarStyle.Blocks;
        public System.Windows.Forms.ProgressBarStyle Style { get { return mvarStyle; } set { mvarStyle = value; } }

        int xpos;
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle bounds = new Rectangle(64, 64, base.Width - 128, 20);

            // draw the progress bar outline
            Theming.Theme.CurrentTheme.DrawProgressBarBackground(e.Graphics, bounds, mvarOrientation);

            double c = (double)(mvarMaximum - mvarMinimum);
            if (c < 1) c = 1;

            Rectangle chunkRect = new Rectangle(bounds.X + 1, bounds.Y + 1, (int)(bounds.Width * (double)((double)mvarValue / (double)c)), bounds.Height - 2);
            Theming.Theme.CurrentTheme.DrawProgressBarChunk(e.Graphics, chunkRect, mvarOrientation);
            
            xpos += 3;
            Rectangle pulseRect = new Rectangle(bounds.Left + xpos, bounds.Top, 50, 20);
            if (xpos + pulseRect.Width >= chunkRect.Width) xpos = -pulseRect.Width;  // Note: intentionally too far left

            Theming.Theme.CurrentTheme.DrawProgressBarPulse(e.Graphics, pulseRect, mvarOrientation);
        }

    }
}
