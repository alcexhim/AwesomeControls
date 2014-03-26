using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Ribbon
{
    public class RibbonPopupControl : Control
    {
        private Ribbon parent = null;
        public RibbonPopupControl(Ribbon parent)
        {
            this.parent = parent;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            parent.ClosePopupControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Theming.Theme.CurrentTheme.DrawRibbonTabPageBackground(e.Graphics, new Rectangle(0, 0, base.Width, base.Height));
        }
    }
}
