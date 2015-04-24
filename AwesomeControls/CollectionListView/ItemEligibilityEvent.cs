using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AwesomeControls.ListView;

namespace AwesomeControls.CollectionListView
{
	public delegate void ItemEligibilityEventHandler(object sender, ItemEligibilityEventArgs e);

	/// <summary>
	/// Event arguments for determining the eligiblity of a <see cref="ListViewItem" /> for a particular operation.
	/// </summary>
	public class ItemEligibilityEventArgs : EventArgs
	{
		private ListViewItem mvarItem = null;
		/// <summary>
		/// The <see cref="ListViewItem" /> whose eligibility is to be determined.
		/// </summary>
		public ListViewItem Item { get { return mvarItem; } }

		private bool mvarEligible = true;
		/// <summary>
		/// Determines whether the specified <see cref="ListViewItem" /> is eligible for the current operation.
		/// </summary>
		public bool Eligible { get { return mvarEligible; } set { mvarEligible = value; } }

		public ItemEligibilityEventArgs(ListViewItem item)
		{
			mvarItem = item;
		}
	}
}
