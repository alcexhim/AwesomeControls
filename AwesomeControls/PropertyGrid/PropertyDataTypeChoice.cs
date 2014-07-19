using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public class PropertyDataTypeChoice
	{
		public class PropertyDataTypeChoiceCollection
			: System.Collections.ObjectModel.Collection<PropertyDataTypeChoice>
		{
			public int IndexOf(object value)
			{
				for (int i = 0; i < Count; i++)
				{
					if (this[i].Value == value) return i;
				}
				return -1;
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		public PropertyDataTypeChoice(object value)
		{
			if (value != null) mvarTitle = value.ToString();
			mvarValue = value;
		}
		public PropertyDataTypeChoice(string title, object value)
		{
			mvarTitle = title;
			mvarValue = value;
		}
	}
}
