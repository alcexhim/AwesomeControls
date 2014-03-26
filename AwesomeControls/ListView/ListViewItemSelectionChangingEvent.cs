using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
    public delegate void ListViewItemSelectionChangingEventHandler(object sender, ListViewItemSelectionChangingEventArgs e);
    public class ListViewItemSelectionChangingEventArgs : CancelEventArgs
    {
        private ListViewItem mvarItem = null;
        public ListViewItem Item { get { return mvarItem; } }

        public ListViewItemSelectionChangingEventArgs(ListViewItem item)
        {
            mvarItem = item;
        }
    }
}
