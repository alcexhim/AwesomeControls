using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyValueRenderingEventHandler(object sender, PropertyValueRenderingEventArgs e);
	public class PropertyValueRenderingEventArgs
	{
		private Property mvarProperty = null;
		public Property Property { get { return mvarProperty; } set { mvarProperty = value; } }

		private string mvarDisplayString = String.Empty;
		public string DisplayString { get { return mvarDisplayString; } set { mvarDisplayString = value; } }
	}
}
