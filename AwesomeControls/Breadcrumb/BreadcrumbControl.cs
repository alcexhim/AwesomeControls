using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AwesomeControls.Breadcrumb
{
	public class BreadcrumbControl : System.Windows.Forms.Control
	{
		private BreadcrumbItem.BreadcrumbItemCollection mvarItems = new BreadcrumbItem.BreadcrumbItemCollection();
		public BreadcrumbItem.BreadcrumbItemCollection Items { get { return mvarItems; } }

		private BreadcrumbItem mvarSelectedItem = null;
		public BreadcrumbItem SelectedItem { get { return mvarSelectedItem; } }

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			#region Back Button
			{
				
			}
			#endregion

			foreach (BreadcrumbItem bci in mvarItems)
			{
				if (bci == mvarSelectedItem)
				{
				}
				else
				{
				}
			}
		}
	}
}
