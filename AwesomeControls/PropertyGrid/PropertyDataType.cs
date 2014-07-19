using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
    public class PropertyDataType
    {
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private PropertyEditor mvarEditor = null;
		public PropertyEditor Editor { get { return mvarEditor; } set { mvarEditor = value; } }

		private List<object> mvarChoices = new List<object>();
		public List<object> Choices { get { return mvarChoices; } }

		private bool mvarRequireSelectionFromChoices = false;
		public bool RequireSelectionFromChoices { get { return mvarRequireSelectionFromChoices; } set { mvarRequireSelectionFromChoices = value; } }

		public PropertyDataType(string title, object[] choices = null, bool requireSelectionFromChoices = false)
		{
			mvarTitle = title;
			if (choices != null)
			{
				foreach (object choice in choices)
				{
					mvarChoices.Add(choice);
				}
			}
			mvarRequireSelectionFromChoices = requireSelectionFromChoices;
		}
    }
	public static class PropertyDataTypes
	{
		private static PropertyDataType mvarString = new PropertyDataType("String");
		public static PropertyDataType String { get { return mvarString; } }

		private static PropertyDataType mvarBoolean = new PropertyDataType("Boolean", new object[] { true, false }, true);
		public static PropertyDataType Boolean { get { return mvarBoolean; } }
	}
}
