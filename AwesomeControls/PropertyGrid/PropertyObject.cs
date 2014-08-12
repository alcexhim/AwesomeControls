using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public class PropertyObject
	{
		public PropertyObject()
		{
		}
		public PropertyObject(string Name)
		{
			mvarName = Name;
		}
		public PropertyObject(string Name, PropertyDataType DataType)
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

		public class PropertyObjectCollection
			: System.Collections.ObjectModel.Collection<PropertyObject>
		{
			private PropertyGridControl _parent = null;
			public PropertyObjectCollection(PropertyGridControl parent)
			{
				_parent = parent;
			}

			protected override void InsertItem(int index, PropertyObject item)
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

			public PropertyObject Add(string Name)
			{
				return Add(Name, PropertyDataType.Empty);
			}
			public PropertyObject Add(string Name, PropertyDataType DataType)
			{
				PropertyObject pg = new PropertyObject();
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
