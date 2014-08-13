using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyGridSelectionChangingEventHandler(object sender, PropertyGridSelectionChangingEventArgs e);
	public class PropertyGridSelectionChangingEventArgs : CancelEventArgs
	{
		private Property mvarOldProperty = null;
		public Property OldProperty { get { return mvarOldProperty; } }

		private Property mvarNewProperty = null;
		public Property NewProperty { get { return mvarNewProperty; } set { mvarNewProperty = value; } }

		public PropertyGridSelectionChangingEventArgs(Property oldProperty, Property newProperty)
		{
			mvarOldProperty = oldProperty;
			mvarNewProperty = newProperty;
		}
	}
}
