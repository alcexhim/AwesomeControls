using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls
{
	public static class ToolStripExtensions
	{
		public static void LoadThemeIcons(this ToolStrip parent, string path)
		{
			RecursiveLoadToolbarItemImages(parent.Items, path);
		}

		private static void RecursiveLoadToolbarItemImages(ToolStripItemCollection coll, string path)
		{
			foreach (ToolStripItem tsi in coll)
			{
				string name = tsi.Name;
				if (name.StartsWith("tsb")) name = name.Substring(3);
				tsi.Image = AwesomeControls.Theming.Theme.CurrentTheme.GetImage(path + System.IO.Path.DirectorySeparatorChar.ToString() + name + ".png");
				if (tsi is ToolStripDropDownItem)
				{
					ToolStripDropDownItem tsddi = (tsi as ToolStripDropDownItem);
					RecursiveLoadToolbarItemImages(tsddi.DropDownItems, path);
					if (tsi.Image == null && tsddi.DropDownItems.Count > 0) tsi.Image = tsddi.DropDownItems[0].Image;
				}
			}
		}
	}
}
