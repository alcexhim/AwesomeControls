using AwesomeControls.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AwesomeControls.CollectionListView
{
	public delegate void ItemPropertiesEventHandler(object sender, ItemPropertiesEventArgs e);
	public class ItemPropertiesEventArgs : CancelEventArgs
	{
		private ListViewItem mvarItem = null;
		public ListViewItem Item { get { return mvarItem; } }
		
		public ItemPropertiesEventArgs(ListViewItem item)
		{
			mvarItem = item;
		}
	}
}
