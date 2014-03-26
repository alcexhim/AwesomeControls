using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.SystemControls
{
    public delegate void ListViewItemDoubleClickEventHandler(object sender, ListViewItemDoubleClickEventArgs e);
    public class ListViewItemDoubleClickEventArgs : EventArgs
    {
        private ListViewItem mvarItem = null;
        public ListViewItem Item { get { return mvarItem; } }

        private ListViewItem.ListViewSubItem mvarSubItem = null;
        public ListViewItem.ListViewSubItem SubItem { get { return mvarSubItem; } }

        public ListViewItemDoubleClickEventArgs(ListViewItem lvi, ListViewItem.ListViewSubItem subitem)
        {
            mvarItem = lvi;
            mvarSubItem = subitem;
        }
    }
}
