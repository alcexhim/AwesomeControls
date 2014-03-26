using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.Ribbon
{
	public class RibbonTab
	{
		public class RibbonTabCollection
			: System.Collections.ObjectModel.Collection<RibbonTab>
		{
			private Ribbon mvarParent = null;

			public RibbonTabCollection()
			{
				mvarParent = null;
			}
			public RibbonTabCollection(Ribbon parent)
			{
				mvarParent = parent;
			}

			public RibbonTab Add(string NameAndText)
			{
				return Add(NameAndText, NameAndText);
			}
			public RibbonTab Add(string Name, string Text)
			{
				RibbonTab tab = new RibbonTab();
				tab.Name = Name;
				tab.Text = Text;
				Add(tab);
				return tab;
			}

			public new void Add(RibbonTab item)
			{
				item.Parent = mvarParent;
				base.Add(item);
			}
		}

    	private Ribbon mvarParent = null;
		public Ribbon Parent
		{
			get { return mvarParent; }
			private set { mvarParent = value; }
		}

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set { mvarName = value; }
		}
		private string mvarText = String.Empty;
		public string Text
		{
			get { return mvarText; }
			set { mvarText = value; }
		}
		private bool mvarEnabled = true;
		public bool Enabled
		{
			get { return mvarEnabled; }
			set { mvarEnabled = value; }
		}

        public override string ToString()
        {
            return String.Format("{0}: \"{1}\"", mvarName, mvarText);
        }

		private RibbonControlGroup.RibbonControlGroupCollection mvarGroups = new RibbonControlGroup.RibbonControlGroupCollection();
		public RibbonControlGroup.RibbonControlGroupCollection Groups
		{
			get { return mvarGroups; }
		}
	}
}
