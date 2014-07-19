using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AwesomeControls.PropertyGrid
{
	public class Property
	{
		public class PropertyCollection
			: System.Collections.ObjectModel.Collection<Property>
		{
			private PropertyGridControl _parent = null;
			public PropertyCollection(PropertyGridControl parent = null)
			{
				_parent = parent;
			}

			protected override void InsertItem(int index, Property item)
			{
				base.InsertItem(index, item);
				item.mvarParentControl = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].mvarParentControl = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				foreach (Property p in this)
				{
					p.mvarParentControl = null;
				}
				base.ClearItems();
			}

		}

		public Property(string name, object defaultValue = null, Image image = null, bool readOnly = false)
		{
			mvarName = name;
			mvarDefaultValue = defaultValue;
			mvarValue = defaultValue;

			mvarImage = image;
			mvarReadOnly = readOnly;
		}

		private PropertyDataType mvarDataType = PropertyDataTypes.String;
		public PropertyDataType DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		public virtual object GetDefaultValue()
		{
			return null;
		}

		private bool mvarReadOnly = false;
		public bool ReadOnly { get { return mvarReadOnly; } set { mvarReadOnly = value; } }
		
		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }
		
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private PropertyGridControl mvarParentControl = null;

		private object mvarDefaultValue = null;
		public object DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }
		
		private object mvarValue = null;
		public object Value
		{
			get { return mvarValue; }
			set
			{
				PropertyChangingEventArgs e = new PropertyChangingEventArgs(this, value);
				if (mvarParentControl != null)
				{
					mvarParentControl.OnPropertyChanging(e);
					if (e.Cancel) return;
				}

				mvarValue = value;
				if (mvarParentControl != null)
				{
					mvarParentControl.OnPropertyChanged(new PropertyChangedEventArgs(this));
				}
			}
		}

		public virtual string GetDefaultValueDisplayString()
		{
			object defaultValue = GetDefaultValue();
			if (defaultValue == null) return String.Empty;
			return defaultValue.ToString();
		}
	}
	public class GroupProperty : Property
	{
		private string mvarDisplayString = String.Empty;
		public string DisplayString { get { return mvarDisplayString; } set { mvarDisplayString = value; } }

		private bool mvarExpanded = false;
		public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; } }

		private Property.PropertyCollection mvarProperties = new Property.PropertyCollection();
		public Property.PropertyCollection Properties { get { return mvarProperties; } set { mvarProperties = value; } }
		
		public GroupProperty(string name, Image image = null, bool readOnly = false, Property[] properties = null)
			: base(name, null, image, readOnly)
		{
			base.Name = name;
			base.Image = image;
			base.ReadOnly = readOnly;
			if (properties != null)
			{
				foreach (Property property in properties)
				{
					mvarProperties.Add(property);
				}
			}
		}
	}
}
