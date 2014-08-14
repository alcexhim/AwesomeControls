using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public delegate void PropertyGridSelectionChangedEventHandler(object sender, PropertyGridSelectionChangedEventArgs e);
	public class PropertyGridSelectionChangedEventArgs : EventArgs
	{
		private IPropertyGridItem mvarOldProperty = null;
		public IPropertyGridItem OldProperty { get { return mvarOldProperty; } }

		private IPropertyGridItem mvarNewProperty = null;
		public IPropertyGridItem NewProperty { get { return mvarNewProperty; } }

		public PropertyGridSelectionChangedEventArgs(IPropertyGridItem oldProperty, IPropertyGridItem newProperty)
		{
			mvarOldProperty = oldProperty;
			mvarNewProperty = newProperty;
		}
	}
}
