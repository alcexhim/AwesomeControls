using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.Ribbon
{
    public class RibbonQuickAccessToolbar
    {
        private bool mvarShowInTitlebar = true;
        public bool ShowInTitlebar
        {
            get { return mvarShowInTitlebar; }
            set { mvarShowInTitlebar = value; }
        }

        private RibbonControl.RibbonControlCollection mvarControls = new RibbonControl.RibbonControlCollection();
        public RibbonControl.RibbonControlCollection Controls
        {
            get { return mvarControls; }
        }
    }
}
