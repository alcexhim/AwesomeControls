using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Label
{
    public class LabelControl : System.Windows.Forms.Label
    {
        private bool mvarHotTrack = false;
        public bool HotTrack { get { return mvarHotTrack; } set { mvarHotTrack = value; } }

        private System.Drawing.Color mvarForeColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.ControlText);
        public override System.Drawing.Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; base.ForeColor = value; } }

        private System.Drawing.Color mvarHoverColor = System.Drawing.Color.Red;
        public System.Drawing.Color HoverColor { get { return mvarHoverColor; } set { mvarHoverColor = value; } }

        private System.Windows.Forms.Cursor mvarCursor = System.Windows.Forms.Cursors.Default;
        public override System.Windows.Forms.Cursor Cursor { get { return mvarCursor; } set { mvarCursor = value; base.Cursor = value; } }

        private System.Drawing.Font mvarFont = System.Drawing.SystemFonts.DefaultFont;
        public override System.Drawing.Font Font { get { return mvarFont; } set { mvarFont = value; base.Font = value; } }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (mvarHotTrack)
            {
                Cursor = System.Windows.Forms.Cursors.Hand;
                base.ForeColor = mvarHoverColor;
                base.Font = new System.Drawing.Font(mvarFont, mvarFont.Style | System.Drawing.FontStyle.Underline);
            }
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (mvarHotTrack)
            {
                Cursor = mvarCursor;
                base.ForeColor = mvarForeColor;
                base.Font = new System.Drawing.Font(mvarFont, mvarFont.Style & ~System.Drawing.FontStyle.Underline);
                Refresh();
            }
        }
    }
}
