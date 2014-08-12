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
		public PropertyGroup(string Name, PropertyDataType DataType)
		{
			mvarName = Name;
			this.DataType = DataType;
		}

		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private PropertyDataType mvarDataType = null;
		public PropertyDataType DataType
		{
			get { return mvarDataType; }
			set
			{
				bool changed = (mvarDataType != value);
				mvarDataType = value;
				if (changed)
				{
					mvarProperties.Clear();
					foreach (Property p in mvarDataType.Properties)
					{
						mvarProperties.Add(p.Clone() as Property);
					}
				}
			}
		}

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
				return Add(Name, PropertyDataType.Empty);
			}
			public PropertyGroup Add(string Name, PropertyDataType DataType)
			{
				PropertyGroup pg = new PropertyGroup();
				pg.Name = Name;
				pg.DataType = DataType;
				Add(pg);
				return pg;
			}
		}

		private PropertyGridPanel mvarParentControl = null;
		public PropertyGridPanel ParentControl
		{
			get { return mvarParentControl; }
			set
			{
				mvarParentControl = value;
				mvarProperties.Parent = value;
			}
		}
	}
}
