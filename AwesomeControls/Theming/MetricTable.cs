﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Theming
{
	public class MetricTable
	{
		private int mvarBreadcrumbItemSpacing = 3;
		public int BreadcrumbItemSpacing { get { return mvarBreadcrumbItemSpacing; } set { mvarBreadcrumbItemSpacing = value; } }

		private int mvarDockingWindowTitlebarSize = 21;
		public int DockingWindowTitlebarSize { get { return mvarDockingWindowTitlebarSize; } set { mvarDockingWindowTitlebarSize = value; } }

		private int mvarDockingWindowSplitterSize = 6;
		public int DockingWindowSplitterSize { get { return mvarDockingWindowSplitterSize; } set { mvarDockingWindowSplitterSize = value; } }
		
		private int mvarDockingWindowTabSize = 20;
		public int DockingWindowTabSize { get { return mvarDockingWindowTabSize; } set { mvarDockingWindowTabSize = value; } }

		private int mvarDockingWindowTabDockedUnderlineSize = 6;
		public int DockingWindowTabDockedUnderlineSize { get { return mvarDockingWindowTabDockedUnderlineSize; } set { mvarDockingWindowTabDockedUnderlineSize = value; } }

		private int mvarDockingWindowTabSpacing = 1;
		public int DockingWindowTabSpacing { get { return mvarDockingWindowTabSpacing; } set { mvarDockingWindowTabSpacing = value; } }

		private int mvarDockingWindowTabScrollArrowSize = 16;
		public int DockingWindowTabScrollArrowSize { get { return mvarDockingWindowTabScrollArrowSize; } set { mvarDockingWindowTabScrollArrowSize = value; } }
	}
}
