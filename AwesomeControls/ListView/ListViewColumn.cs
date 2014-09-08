using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
	public class ListViewColumn
	{
		public class ListViewColumnCollection
			: System.Collections.ObjectModel.Collection<ListViewColumn>
		{
			public ListViewColumn Add(string Text)
			{
				return Add(Text, 60);
			}
			public ListViewColumn Add(string Text, int Width)
			{
				ListViewColumn item = new ListViewColumn();
				item.Text = Text;
				item.Width = Width;
				Add(item);
				return item;
			}

			public void AddRange(ListViewColumn[] listViewColumn)
			{
				foreach (ListViewColumn lvc in listViewColumn)
				{
					Add(lvc);
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private int mvarWidth = 60;
		public int Width { get { return mvarWidth; } set { mvarWidth = value; } }
	}
}
