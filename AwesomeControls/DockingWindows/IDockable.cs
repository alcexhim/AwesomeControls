using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.DockingWindows
{
	public interface IDockable
	{
		DockingContainerControl Parent { get; }
	}
	public class IDockableCollection
		: System.Collections.ObjectModel.Collection<IDockable>
	{
		internal DockingContainerControl mvarParent = null;
		private DockingWindowContainer mvarParentDock = null;
		internal IDockableCollection(DockingContainerControl parent, DockingWindowContainer parentDock)
		{
			mvarParent = parent;
			mvarParentDock = parentDock;
		}

		public DockingWindowContainer Add()
		{
			DockingWindowContainer dwc = new DockingWindowContainer();
			dwc.Parent = mvarParent;
			dwc.ParentDock = mvarParentDock;
			base.Add(dwc);
			return dwc;
		}

		public DockingWindow Add(string title, System.Windows.Forms.Control control)
		{
			return Add(title, title, control);
		}
		public DockingWindow Add(string name, string title, System.Windows.Forms.Control control)
		{
			DockingWindow window = new DockingWindow();
			window.Name = name;
			window.Title = title;
			window.Control = control;
			window.Parent = mvarParent;
			window.ParentDock = mvarParentDock;
			base.Add(window);
			return window;
		}

		protected override void InsertItem(int index, IDockable item)
		{
			base.InsertItem(index, item);

			if (mvarParent != null)
			{
                mvarParent.UpdateDockableItems();

                DockingWindow dw = (item as DockingWindow);
                if (dw != null)
                {
                    if (dw.Behavior == DockBehavior.DockAsTabbedDocument)
                    {
                        mvarParent.SelectedWindow = dw;
                    }
                }
			}
            if (item is DockingWindow)
            {
				(item as DockingWindow).Parent = mvarParent;
				(item as DockingWindow).ParentDock = mvarParentDock;
            }
            else if (item is DockingWindowContainer)
            {
				(item as DockingWindowContainer).Parent = mvarParent;
				(item as DockingWindowContainer).ParentDock = mvarParentDock;
            }
		}
		protected override void RemoveItem(int index)
		{
			DockingWindow dw = (this[index] as DockingWindow);
			if (dw != null)
			{
				dw.Parent.CloseWindow(dw);
			}
			base.RemoveItem(index);
		}

	}
}
