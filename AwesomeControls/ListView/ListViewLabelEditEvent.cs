using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
    public delegate void ListViewLabelEditEventHandler(object sender, ListViewLabelEditEventArgs e);
    public class ListViewLabelEditEventArgs : CancelEventArgs
    {
        private string mvarLabel = String.Empty;
        public string Label { get { return mvarLabel; } set { mvarLabel = value; } }
    }
}
