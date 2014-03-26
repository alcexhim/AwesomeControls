// one line to give the program's name and an idea of what it does.
// Copyright (C) 2010  Mike Becker
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;

namespace AwesomeControls.DockingWindows
{
	/// <summary>
	/// Description of DockingWindow.
	/// </summary>
	public class DockingWindow : IDockingContainerObject
	{
		public class DockingWindowCollection
			: System.Collections.ObjectModel.Collection<DockingWindow>
		{
			private DockingContainerControl mvarParentContainer = null;
			internal DockingWindowCollection(DockingContainerControl parent)
			{
				mvarParentContainer = parent;
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
				window.ParentContainer = mvarParentContainer;
				base.Add(window);
				return window;
			}

			public DockingWindow this[string name]
			{
				get
				{
					foreach (DockingWindow dw in this)
					{
						if (dw.Name == name) return dw;
					}
					return null;
				}
			}
			public DockingWindow this[System.Windows.Forms.Control childControl]
			{
				get
				{
					foreach (DockingWindow dw in this)
					{
						if (dw.Control == childControl) return dw;
					}
					return null;
				}
			}

			protected override void InsertItem(int index, DockingWindow item)
			{
				base.InsertItem(index, item);

				if (mvarParentContainer != null)
				{
					mvarParentContainer.UpdateDockableItems();

					DockingWindow dw = (item as DockingWindow);
					if (dw != null)
					{
						if (dw.Behavior == DockBehavior.Dock)
						{
							mvarParentContainer.SelectedWindow = dw;
						}
					}
				}

				item.ParentContainer = mvarParentContainer;
			}
			protected override void RemoveItem(int index)
			{
				DockingWindow dw = this[index];
				base.RemoveItem(index);
				if (dw != null)
				{
					dw.ParentContainer.CloseWindow(dw);
				}
			}

		}

		public class DockingAreaWindowCollection
			: System.Collections.ObjectModel.Collection<DockingWindow>
		{
			private DockingArea mvarParentArea = null;
			public DockingAreaWindowCollection(DockingArea parentArea)
			{
				mvarParentArea = parentArea;
			}

			protected override void InsertItem(int index, DockingWindow item)
			{
				base.InsertItem(index, item);
				item.ParentArea = mvarParentArea;
				item.ParentContainer.UpdateControlMetrics();

				if (item.Control != null)
				{
					item.ParentContainer.SwitchTab(item);
				}
			}
			protected override void RemoveItem(int index)
			{
				this[index].ParentArea = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, DockingWindow item)
			{
				this[index].ParentArea = null;
				base.SetItem(index, item);
				item.ParentArea = mvarParentArea;
				item.ParentContainer.UpdateControlMetrics();
			}
		}

		private DockingArea mvarParentArea = null;
		public DockingArea ParentArea { get { return mvarParentArea; } internal set { mvarParentArea = value; } }

		private DockingContainerControl mvarParentContainer = null;
		public DockingContainerControl ParentContainer
		{
			get { return mvarParentContainer; }
			internal set
			{
				DockingContainerControl dcc = mvarParentContainer;
				mvarParentContainer = value;
				if (dcc != null && dcc != mvarParentContainer) dcc.UpdateDockableItems();
				if (mvarParentContainer != null) mvarParentContainer.UpdateDockableItems();
			}
		}

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set { mvarName = value; }
		}

		private string mvarTabTitle = String.Empty;
		public string TabTitle
		{
			get { return mvarTabTitle; }
			set { mvarTabTitle = value; }
		}
		private string mvarPanelTitle = String.Empty;
		public string PanelTitle
		{
			get { return mvarPanelTitle; }
			set { mvarPanelTitle = value; }
		}

		public string Title
		{
			get { return mvarTabTitle; }
			set { mvarTabTitle = value; mvarPanelTitle = value; }
		}

		private System.Windows.Forms.Control mvarControl = null;
		public System.Windows.Forms.Control Control
		{
			get { return mvarControl; }
			set { mvarControl = value; }
		}

		private DockBehavior mvarBehavior = DockBehavior.Dock;
		public DockBehavior Behavior { get { return mvarBehavior; } set { mvarBehavior = value; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private ControlState mvarTabState = ControlState.Normal;
		public ControlState TabState { get { return mvarTabState; } set { mvarTabState = value; } }

		private bool mvarVisible = true;
		public bool Visible { get { return mvarVisible; } set { mvarVisible = value; } }

		private int mvarSize = 128;
		public int Size { get { return mvarSize; } set { mvarSize = value; } }

		private bool mvarSelected = false;
		public bool Selected { get { return mvarSelected; } set { mvarSelected = value; } }

		private bool mvarFocused = false;
		public bool Focused { get { return mvarFocused; } internal set { mvarFocused = value; } }

		private string mvarToolTipText = null;
		public string ToolTipText { get { return mvarToolTipText; } set { mvarToolTipText = value; } }

		private bool mvarEnableClose = true;
		public bool EnableClose { get { return mvarEnableClose; } set { mvarEnableClose = value; } }

		public override string ToString()
		{
			return mvarName + " [" + mvarPanelTitle + "]";
		}

		public void Close()
		{
			if (mvarParentContainer != null)
			{
				mvarParentContainer.CloseWindow(this);
			}
		}
	}
}
