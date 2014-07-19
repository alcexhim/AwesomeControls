using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
	public class PropertyChangingEventArgs : System.ComponentModel.CancelEventArgs
	{
		private Property mvarProperty = null;
		public Property Property { get { return mvarProperty; } }

		private object mvarNewValue = String.Empty;
		public object NewValue { get { return mvarNewValue; } set { mvarNewValue = value; } }

		public PropertyChangingEventArgs(Property property, object newValue)
		{
			mvarProperty = property;
			mvarNewValue = newValue;
		}
	}
}
