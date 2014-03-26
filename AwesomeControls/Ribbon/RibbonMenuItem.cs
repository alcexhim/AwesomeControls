using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AwesomeControls.Ribbon
{
    public class RibbonMenuItem
    {
        public class RibbonMenuItemCollection
            : System.Collections.ObjectModel.Collection<RibbonMenuItem>
        {
        }

        private string mvarName = String.Empty;
        public string Name
        {
            get { return mvarName; }
            set { mvarName = value; }
        }

        private string mvarText = String.Empty;
        public string Text
        {
            get { return mvarText; }
            set { mvarText = value; }
        }

        private RibbonControlDisplayStyle mvarDisplayStyle = RibbonControlDisplayStyle.ImageBesideText;
        public RibbonControlDisplayStyle DisplayStyle
        {
            get { return mvarDisplayStyle; }
            set { mvarDisplayStyle = value; }
        }

        private Image mvarImage = null;
        public Image Image
        {
            get { return mvarImage; }
            set { mvarImage = value; }
        }
    }
}
