using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AwesomeControls.PropertyGrid
{
	public class Property : ICloneable
	{
		public class PropertyCollection
			: System.Collections.ObjectModel.Collection<Property>
		{
			private PropertyGridPanel _parent = null;
			public PropertyGridPanel Parent { get { return _parent; } 
				internal set 
				{
					_parent = value;
					foreach (Property p in this)
					{
						p.ParentControl = _parent;
					}
				} 
			}

			public PropertyCollection(PropertyGridPanel parent = null)
			{
				_parent = parent;
			}

			private Property _parentProperty = null;
			public PropertyCollection(Property parent)
			{
				_parentProperty = parent;
			}

			public Property this[string name]
			{
				get
				{
					foreach (Property p in this)
					{
						if (p.Name == name) return p;
					}
					return null;
				}
			}

			protected override void InsertItem(int index, Property item)
			{
				base.InsertItem(index, item);
				item.mvarParentControl = _parent;
				item.mvarParent = _parentProperty;
			}
			protected override void RemoveItem(int index)
			{
				this[index].mvarParentControl = null;
				this[index].mvarParent = null;
				base.RemoveItem(index);
			}
			protected override void ClearItems()
			{
				foreach (Property p in this)
				{
					p.mvarParentControl = null;
					p.mvarParent = null;
				}
				base.ClearItems();
			}

		}

		private Property()
		{
			mvarProperties = new PropertyCollection(this);
		}
		public Property(string name, object defaultValue = null, Image image = null, bool readOnly = false)
		{
			mvarName = name;
			mvarDefaultValue = defaultValue;
			mvarValue = defaultValue;

			mvarImage = image;
			mvarReadOnly = readOnly;
			mvarProperties = new PropertyCollection(this);
		}

		private bool mvarExpanded = false;
		public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; if (mvarParentControl != null) mvarParentControl.UpdatePropertyBounds(); } }	

		private Property.PropertyCollection mvarProperties = null;
		public Property.PropertyCollection Properties { get { return mvarProperties; } set { mvarProperties = value; } }

		private PropertyDataType mvarDataType = PropertyDataTypes.String;
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
					foreach (Property prop in mvarDataType.Properties)
					{
						mvarProperties.Add(prop.Clone() as Property);
					}
				}
			}
		}

		private bool mvarSetLastReadOnly = true;
		private bool mvarLastReadOnly = false;

		private bool mvarReadOnly = false;
		public bool ReadOnly
		{
			get { return mvarReadOnly; }
			set
			{
				mvarReadOnly = value;
				if (mvarSetLastReadOnly)
				{
					mvarLastReadOnly = value;
				}

				if (mvarReadOnly)
				{
					foreach (Property p in mvarProperties)
					{
						p.mvarSetLastReadOnly = false;
						p.mvarLastReadOnly = p.ReadOnly;
						p.ReadOnly = true;
						p.mvarSetLastReadOnly = true;
					}
				}
				else
				{
					foreach (Property p in mvarProperties)
					{
						p.mvarSetLastReadOnly = false;
						p.ReadOnly = p.mvarLastReadOnly;
						p.mvarSetLastReadOnly = true;
					}
				}
			}
		}
		
		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }
		
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private PropertyGridPanel mvarParentControl = null;
		public PropertyGridPanel ParentControl 
		{
			get { return mvarParentControl; }
			internal set 
			{
				mvarParentControl = value;
				foreach (Property p in mvarProperties)
				{
					p.ParentControl = mvarParentControl;
				}
			}
		}

		private bool mvarDefaultValueSet = false;
		public bool DefaultValueSet { get { return mvarDefaultValueSet; } set { mvarDefaultValueSet = value; if (!mvarDefaultValueSet) mvarDefaultValue = null; } }

		private object mvarDefaultValue = null;
		public object DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; mvarDefaultValueSet = true; } }
		
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
					mvarParentControl.Refresh();
					mvarParentControl.OnPropertyChanged(new PropertyChangedEventArgs(this));
				}
			}
		}

		public object Clone()
		{
			Property clone = new Property();
			clone.DataType = mvarDataType;
			clone.DefaultValue = mvarDefaultValue;
			clone.Expanded = mvarExpanded;
			clone.Image = mvarImage;
			clone.Name = (mvarName.Clone() as string);
			/*
			 * DON'T DO THIS - THIS IS WHAT DUPLICATES PROPERTIES
			foreach (Property prop in mvarProperties)
			{
				clone.Properties.Add(prop.Clone() as Property);
			}
			 * PROPERTIES ARE SET WHEN clone.DataType IS SET
			*/
			clone.ReadOnly = mvarReadOnly;
			clone.Value = mvarValue;
			return clone;
		}

		private Property mvarParent = null;
		public Property Parent { get { return mvarParent; } }

		public bool IsChanged
		{
			get
			{
				if (mvarDefaultValueSet)
				{
					if (mvarDefaultValue == null)
					{
						if (mvarValue != null) return true;
					}
					else
					{
						return (!mvarDefaultValue.Equals(mvarValue));
					}
				}
				return false;
			}
		}

		/// <summary>
		/// Resets the property to its default value, if set.
		/// </summary>
		/// <returns>True if the property has been reset, false if it has no default value to reset to.</returns>
		public bool Reset()
		{
			if (mvarDefaultValueSet)
			{
				Value = mvarDefaultValue;
				return true;
			}
			return false;
		}
	}
}
