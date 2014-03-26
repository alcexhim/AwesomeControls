using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
    public delegate void ListViewItemDragEventHandler(object sender, ListViewItemDragEventArgs e);
    public class ListViewItemDragEventArgs : EventArgs
    {
        private System.Windows.Forms.IDataObject mvarDataObject = null;
        public System.Windows.Forms.IDataObject DataObject { get { return mvarDataObject; } set { mvarDataObject = value; } }

        private System.Windows.Forms.DragDropEffects mvarEffects = System.Windows.Forms.DragDropEffects.None;
        public System.Windows.Forms.DragDropEffects Effects { get { return mvarEffects; } set { mvarEffects = value; } }
    }
}
