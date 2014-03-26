using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
	public class PropertyChangedEventArgs : EventArgs
	{
		private Property mvarProperty = null;
		public Property Property { get { return mvarProperty; } }

		public PropertyChangedEventArgs(Property property)
		{
			mvarProperty = property;
		}
	}
}
