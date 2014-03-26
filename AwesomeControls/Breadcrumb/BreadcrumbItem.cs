using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Breadcrumb
{
	public class BreadcrumbItem
	{
		public class BreadcrumbItemCollection
			: System.Collections.ObjectModel.Collection<BreadcrumbItem>
		{
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

		private BreadcrumbItemCollection mvarItems = new BreadcrumbItemCollection();
		public BreadcrumbItemCollection Items { get { return mvarItems; } }
	}
}
