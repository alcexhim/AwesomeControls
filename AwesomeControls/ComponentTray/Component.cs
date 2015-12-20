using System;
using System.Drawing;

namespace AwesomeControls.ComponentTray
{
	public class Component
	{
		public class ComponentCollection
			: System.Collections.ObjectModel.Collection<Component>
		{
			private ComponentTrayControl mvarOwner = null;
			public ComponentCollection(ComponentTrayControl owner = null)
			{
				mvarOwner = owner;
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				if (mvarOwner != null) mvarOwner.Refresh();
			}
			protected override void InsertItem(int index, Component item)
			{
				base.InsertItem(index, item);
				if (mvarOwner != null) mvarOwner.Refresh();
			}
			protected override void RemoveItem(int index)
			{
				base.RemoveItem(index);
				if (mvarOwner != null) mvarOwner.Refresh();
			}
			protected override void SetItem(int index, Component item)
			{
				base.SetItem(index, item);
				if (mvarOwner != null) mvarOwner.Refresh();
			}
		}

		public Component(string title = "", Image image = null)
		{
			mvarTitle = title;
			mvarImage = image;
		}

		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
