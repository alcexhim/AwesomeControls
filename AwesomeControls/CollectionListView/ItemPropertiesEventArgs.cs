using AwesomeControls.ListView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.CollectionListView
{
	public class ItemPropertiesEventArgs
	{
		private ListViewItem mvarItem = null;
		public ListViewItem Item { get { return mvarItem; } }
		
		public ItemPropertiesEventArgs(ListViewItem item)
		{
			mvarItem = item;
		}
	}
}
