using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.CommandBars
{
	public static class ToolStripItemExtensions
	{
		public static void ShowTooltipMenu(this ToolStripItem item)
		{
		}
		public static ToolStripItem Clone(this ToolStripItem item)
		{
			ToolStripItem tsi = null;
			if (item is ToolStripButton)
			{
				tsi = new ToolStripButton();
			}
			else if (item is ToolStripComboBox)
			{
				tsi = new ToolStripComboBox();
			}
			else if (item is ToolStripSplitButton)
			{
				tsi = new ToolStripSplitButton();
				foreach (ToolStripItem tsmi in (item as ToolStripSplitButton).DropDownItems)
				{
					(tsi as ToolStripSplitButton).DropDownItems.Add(tsmi.Clone());
				}
			}
			else if (item is ToolStripSeparator)
			{
				tsi = new ToolStripSeparator();
			}
			else if (item is ToolStripMenuItem)
			{
                tsi = new ToolStripMenuItem();
                foreach (ToolStripItem tsmi in (item as ToolStripMenuItem).DropDownItems)
                {
                    (tsi as ToolStripMenuItem).DropDownItems.Add(tsmi.Clone());
                }
			}
			else if (item is ToolStripLabel)
			{
				tsi = new ToolStripLabel();
			}
			tsi.Name = item.Name;
			tsi.Text = item.Text;
			tsi.Image = item.Image;
			tsi.DisplayStyle = item.DisplayStyle;

			tsi.Tag = item;

			tsi.Click += delegate(object sender, EventArgs e)
			{
				((ToolStripItem)((ToolStripItem)sender).Tag).PerformClick();
			};
			return tsi;
		}
	}
}
