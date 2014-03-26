using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public class PropertyGroup
	{
		public PropertyGroup()
		{
		}
		public PropertyGroup(string Name)
		{
			mvarName = Name;
		}
		public PropertyGroup(string Name, string TypeName)
		{
			mvarName = Name;
			mvarTypeName = TypeName;
		}

		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarTypeName = "";
		public string TypeName { get { return mvarTypeName; } set { mvarTypeName = value; } }

		private Property.PropertyCollection mvarProperties = new Property.PropertyCollection();
		public Property.PropertyCollection Properties { get { return mvarProperties; } }

		public class PropertyGroupCollection
			: System.Collections.ObjectModel.Collection<PropertyGroup>
		{
			private PropertyGridControl _parent = null;
			public PropertyGroupCollection(PropertyGridControl parent)
			{
				_parent = parent;
			}

			protected override void InsertItem(int index, PropertyGroup item)
			{
				base.InsertItem(index, item);
				if (_parent != null) _parent.cboObject.Items.Add(item);
			}
			protected override void ClearItems()
			{
				base.ClearItems();
				if (_parent != null) _parent.cboObject.Items.Clear();
			}
			protected override void RemoveItem(int index)
			{
				if (_parent != null) _parent.cboObject.Items.Remove(this[index]);
				base.RemoveItem(index);
			}

			public PropertyGroup Add(string Name)
			{
				return Add(Name, "");
			}
			public PropertyGroup Add(string Name, string TypeName)
			{
				PropertyGroup pg = new PropertyGroup();
				pg.Name = Name;
				pg.TypeName = TypeName;
				Add(pg);
				return pg;
			}
		}
	}
}
