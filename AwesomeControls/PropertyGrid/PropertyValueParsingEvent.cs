using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyValueParsingEventHandler(object sender, PropertyValueParsingEventArgs e);
	public class PropertyValueParsingEventArgs : CancelEventArgs
	{
		private Property mvarProperty = null;
		public Property Property { get { return mvarProperty; } }

		private string mvarDisplayString = String.Empty;
		public string DisplayString { get { return mvarDisplayString; } set { mvarDisplayString = value; } }

		private string mvarMessage = String.Empty;
		public string Message { get { return mvarMessage; } set { mvarMessage = value; } }
	}
}
