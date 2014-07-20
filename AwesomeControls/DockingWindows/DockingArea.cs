using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.DockingWindows
{
	public class DockingArea : IDockingContainerObject
	{
		public class DockingAreaCollection
		{
			private Dictionary<DockPosition, DockingArea> areasByPosition = new Dictionary<DockPosition, DockingArea>();

			private DockingArea mvarParentArea = null;
			public DockingAreaCollection(DockingArea parentArea = null)
			{
				mvarParentArea = parentArea;
			}

			public DockingArea this[DockPosition position]
			{
				get
				{
					if (!areasByPosition.ContainsKey(position))
					{
						DockingArea area = new DockingArea();
						area.Position = position;
						area.ParentArea = mvarParentArea;
						areasByPosition.Add(position, area);
					}
					return areasByPosition[position];
				}
			}

			public int Count { get { return areasByPosition.Count; } }

			public bool Contains(DockPosition position)
			{
				return areasByPosition.ContainsKey(position);
			}
		}

		public DockingArea()
		{
			mvarWindows = new DockingWindow.DockingAreaWindowCollection(this);
			mvarAreas = new DockingAreaCollection(this);
		}

		public override string ToString()
		{
			return mvarPosition.ToString();
		}

		private DockPosition mvarPosition = DockPosition.Center;
		public DockPosition Position { get { return mvarPosition; } set { mvarPosition = value; } }

		private DockingArea.DockingAreaCollection mvarAreas = new DockingArea.DockingAreaCollection();
		public DockingArea.DockingAreaCollection Areas { get { return mvarAreas; } }

		private DockingWindow.DockingAreaWindowCollection mvarWindows = null;
		public DockingWindow.DockingAreaWindowCollection Windows { get { return mvarWindows; } }

		private int mvarSize = 200;
		/// <summary>
		/// If <see cref="DockPosition"/> is <see cref="DockPosition.Left"/> or <see cref="DockPosition.Right"/>,
		/// determines the width of the docking panel. If <see cref="DockPosition"/> is <see cref="DockPosition.Top"/>
		/// or <see cref="DockPosition.Bottom"/>, determines the height of the docking panel. If <see cref="DockPosition"/>
		/// is <see cref="DockPosition.Center"/>, this property has no effect.
		/// </summary>
		public int Size { get { return mvarSize; } set { mvarSize = value; } }

		private DockingArea mvarParentArea = null;
		public DockingArea ParentArea { get { return mvarParentArea; } internal set { mvarParentArea = value; } }

		public bool IsDocked
		{
			get
			{
				foreach (DockingWindow dw in mvarWindows)
				{
					if (dw.Behavior == DockBehavior.Dock) return true;
				}

				if (mvarAreas.Count != 0)
				{
					if (mvarAreas[DockPosition.Bottom].IsDocked) return true;
					if (mvarAreas[DockPosition.Center].IsDocked) return true;
					if (mvarAreas[DockPosition.Left].IsDocked) return true;
					if (mvarAreas[DockPosition.Right].IsDocked) return true;
					if (mvarAreas[DockPosition.Top].IsDocked) return true;
				}

				// if (mvarParentArea != null) return mvarParentArea.IsDocked;
				return false;
			}
		}

		public bool Focused
		{
			get
			{
				foreach (DockingWindow window in mvarWindows)
				{
					if (window.Focused) return true;
				}
				return false;
			}
		}
	}
}
