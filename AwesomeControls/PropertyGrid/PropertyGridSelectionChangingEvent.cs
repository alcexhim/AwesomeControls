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
		private IPropertyGridItem mvarOldProperty = null;
		public IPropertyGridItem OldProperty { get { return mvarOldProperty; } }

		private IPropertyGridItem mvarNewProperty = null;
		public IPropertyGridItem NewProperty { get { return mvarNewProperty; } set { mvarNewProperty = value; } }

		public PropertyGridSelectionChangingEventArgs(IPropertyGridItem oldProperty, IPropertyGridItem newProperty)
		{
			mvarOldProperty = oldProperty;
			mvarNewProperty = newProperty;
		}
	}
}
