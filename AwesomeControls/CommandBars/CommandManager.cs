using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.CommandBars
{
	public static class CommandManager
	{
		private static Command.CommandCollection mvarCommands = new Command.CommandCollection();
		public static Command.CommandCollection Commands { get { return mvarCommands; } }

		private static CommandManagerMenuBar mvarMenuBar = new CommandManagerMenuBar();
		public static CommandManagerMenuBar MenuBar { get { return mvarMenuBar; } }

		private static CommandManagerToolbar.CommandManagerToolbarCollection mvarToolbars = new CommandManagerToolbar.CommandManagerToolbarCollection();
		public static CommandManagerToolbar.CommandManagerToolbarCollection Toolbars { get { return mvarToolbars; } }
	}
	public class CommandManagerMenuBar
	{
		private CommandBarItem.CommandBarItemCollection mvarItems = new CommandBarItem.CommandBarItemCollection();
		public CommandBarItem.CommandBarItemCollection Items { get { return mvarItems; } }
	}
	public class CommandManagerToolbar
	{
		public class CommandManagerToolbarCollection
			: System.Collections.ObjectModel.Collection<CommandManagerToolbar>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private CommandBarItem.CommandBarItemCollection mvarItems = new CommandBarItem.CommandBarItemCollection();
		public CommandBarItem.CommandBarItemCollection Items { get { return mvarItems; } }
	}
}
