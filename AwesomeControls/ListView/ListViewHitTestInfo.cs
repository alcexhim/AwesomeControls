using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
    public class ListViewHitTestInfo
    {
        private ListViewItem mvarItem = null;
        public ListViewItem Item { get { return mvarItem; } }

        public ListViewHitTestInfo(ListViewItem item)
        {
            mvarItem = item;
        }
    }
}
