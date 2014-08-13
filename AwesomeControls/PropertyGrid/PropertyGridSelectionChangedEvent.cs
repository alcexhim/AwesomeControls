using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyGridSelectionChangedEventHandler(object sender, PropertyGridSelectionChangedEventArgs e);
	public class PropertyGridSelectionChangedEventArgs : EventArgs
	{
		private Property mvarOldProperty = null;
		public Property OldProperty { get { return mvarOldProperty; } }

		private Property mvarNewProperty = null;
		public Property NewProperty { get { return mvarNewProperty; } }

		public PropertyGridSelectionChangedEventArgs(Property oldProperty, Property newProperty)
		{
			mvarOldProperty = oldProperty;
			mvarNewProperty = newProperty;
		}
	}
}
