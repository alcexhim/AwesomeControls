using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.CommandBars
{
	public class Command
	{
		public class CommandCollection
			: System.Collections.ObjectModel.Collection<Command>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private System.Drawing.Image mvarImage = null;
		public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }
	}

	public class CommandBarItem
	{
		public class CommandBarItemCollection
			: System.Collections.ObjectModel.Collection<CommandBarItem>
		{

		}

	}
	public class CommandBarItemSeparator : CommandBarItem
	{

	}
	public class CommandBarItemCommand : CommandBarItem
	{

		private System.Windows.Forms.ToolStripItemDisplayStyle mvarDisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		public System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { get { return mvarDisplayStyle; } set { mvarDisplayStyle = value; } }
	}
	public class CommandBarItemMenu : CommandBarItem
	{

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private CommandBarItem.CommandBarItemCollection mvarItems = new CommandBarItem.CommandBarItemCollection();
		public CommandBarItem.CommandBarItemCollection Items { get { return mvarItems; } }
	}
}
