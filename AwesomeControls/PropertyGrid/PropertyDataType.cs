using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public class PropertyDataType
	{
		public event PropertyValueRenderingEventHandler PropertyValueRendering;
		public event PropertyValueParsingEventHandler PropertyValueParsing;

		internal bool TriggerPropertyValueRendering(PropertyValueRenderingEventArgs e)
		{
			if (PropertyValueRendering != null)
			{
				PropertyValueRendering(this, e);
				return true;
			}
			return false;
		}
		internal bool TriggerPropertyValueParsing(PropertyValueParsingEventArgs e)
		{
			if (PropertyValueParsing != null)
			{
				PropertyValueParsing(this, e);
				return true;
			}
			return false;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private PropertyEditor mvarEditor = null;
		public PropertyEditor Editor { get { return mvarEditor; } set { mvarEditor = value; } }

		private PropertyDataTypeChoice.PropertyDataTypeChoiceCollection mvarChoices = new PropertyDataTypeChoice.PropertyDataTypeChoiceCollection();
		public PropertyDataTypeChoice.PropertyDataTypeChoiceCollection Choices { get { return mvarChoices; } }

		private bool mvarRequireSelectionFromChoices = false;
		public bool RequireSelectionFromChoices { get { return mvarRequireSelectionFromChoices; } set { mvarRequireSelectionFromChoices = value; } }

		private Property.PropertyCollection mvarProperties = new Property.PropertyCollection();
		public Property.PropertyCollection Properties { get { return mvarProperties; } }

		public PropertyDataType(string title)
		{
			mvarTitle = title;
		}
		public PropertyDataType(string title, object[] choices, bool requireSelectionFromChoices = false)
		{
			mvarTitle = title;
			foreach (object choice in choices)
			{
				mvarChoices.Add(new PropertyDataTypeChoice(choice));
			}
			mvarRequireSelectionFromChoices = requireSelectionFromChoices;
		}
		public PropertyDataType(string title, PropertyDataTypeChoice[] choices, bool requireSelectionFromChoices = false)
		{
			mvarTitle = title;
			if (choices != null)
			{
				foreach (PropertyDataTypeChoice choice in choices)
				{
					mvarChoices.Add(choice);
				}
			}
			mvarRequireSelectionFromChoices = requireSelectionFromChoices;
		}

		private static PropertyDataType mvarEmpty = new PropertyDataType(String.Empty);
		public static PropertyDataType Empty { get { return mvarEmpty; } }
	}
	public static class PropertyDataTypes
	{
		private static PropertyDataType mvarString = new PropertyDataType("String");
		public static PropertyDataType String { get { return mvarString; } }

		private static PropertyDataType mvarBoolean = new PropertyDataType("Boolean", new object[] { true, false }, true);
		public static PropertyDataType Boolean { get { return mvarBoolean; } }
	}
}
