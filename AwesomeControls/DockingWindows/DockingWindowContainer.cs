using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.DockingWindows
{
	public class DockingWindowContainer : IDockable
	{
		public DockingWindowContainer()
		{
			mvarWindows = new IDockableCollection(mvarParent, this);
		}

		private DockingContainerControl mvarParent = null;
		public DockingContainerControl Parent { get { return mvarParent; } internal set { mvarParent = value; mvarWindows.mvarParent = mvarParent; } }

		private IDockableCollection mvarWindows = null;
		public IDockableCollection Windows
		{
			get { return mvarWindows; }
		}

		private DockPosition mvarDockPosition = DockPosition.Left;
		public DockPosition DockPosition { get { return mvarDockPosition; } set { mvarDockPosition = value; } }

		private DockingWindowContainer mvarParentDock = null;
		public DockingWindowContainer ParentDock { get { return mvarParentDock; } internal set { mvarParentDock = value; } }

        private int mvarSize = 128;
        public int Size { get { return mvarSize; } set { mvarSize = value; } }

		private bool mvarFocused = false;
		public bool Focused { get { return mvarFocused; } set { mvarFocused = value; } }

		public bool IsDocked
		{
			get
			{
				foreach (IDockable idck in mvarWindows)
				{
					if (idck is DockingWindow)
					{
						if ((idck as DockingWindow).Behavior == DockBehavior.Dock)
						{
							return true;
						}
					}
				}
				return false;
			}
		}
	}
}
