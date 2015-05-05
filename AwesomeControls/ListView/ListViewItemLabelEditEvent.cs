using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
	public delegate void ListViewItemLabelEditingEventHandler(object sender, ListViewItemLabelEditingEventArgs e);
	public class ListViewItemLabelEditingEventArgs : CancelEventArgs
	{
		private ListViewItem mvarItem = null;
		public ListViewItem Item { get { return mvarItem; } }

		private string mvarLabel = String.Empty;
		public string Label { get { return mvarLabel; } set { mvarLabel = value; } }

		public ListViewItemLabelEditingEventArgs(ListViewItem item, string label)
		{
			mvarItem = item;
			mvarLabel = label;
		}
	}

	public delegate void ListViewItemLabelEditedEventHandler(object sender, ListViewItemLabelEditedEventArgs e);
	public class ListViewItemLabelEditedEventArgs : EventArgs
	{
		private ListViewItem mvarItem = null;
		public ListViewItem Item { get { return mvarItem; } }

		public ListViewItemLabelEditedEventArgs(ListViewItem item)
		{
			mvarItem = item;
		}
	}
}
