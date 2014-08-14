using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public class PropertyCategory : IPropertyGridItem
	{
		public class PropertyCategoryCollection
			: System.Collections.ObjectModel.Collection<PropertyCategory>
		{

		}

		public PropertyCategory(string title)
			: this(title, title)
		{

		}
		public PropertyCategory(string name, string title)
		{
			mvarName = name;
			mvarTitle = title;
		}
		public PropertyCategory(string name, string title, string description)
		{
			mvarName = name;
			mvarTitle = title;
			mvarDescription = description;
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTitle = String.Empty;
		public string Title
		{
			get { return mvarTitle; }
			set
			{
				if (value == null) value = String.Empty;
				mvarTitle = value;
			}
		}

		private string mvarDescription = String.Empty;
		public string Description 
		{
			get { return mvarDescription; } 
			set 
			{
				if (value == null) value = String.Empty;
				mvarDescription = value; 
			}
		}

		private bool mvarExpanded = true;
		public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; } }
	}
}
