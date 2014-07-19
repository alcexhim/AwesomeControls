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

			public Property Add(string Name)
			{
				return Add(Name, "", false);
			}
			public Property Add(string Name, string Value)
			{
				return Add(Name, Value, false);
			}
			public Property Add(string Name, string Value, bool ReadOnly)
			{
				Property p = new Property();
				p.Name = Name;
				p.Value = Value;
				p.ReadOnly = ReadOnly;
				Add(p);
				return p;
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

			public Property Add(string Name, string Value, params string[] ValidValues)
			{
				return Add(Name, Value, false, ValidValues);
			}
			public Property Add(string Name, string Value, bool ReadOnly, params string[] ValidValues)
			{
				Property p = new Property();
				p.Name = Name;
				p.Value = Value;
				foreach (string s in ValidValues)
				{
					p.ValidValues.Add(s);
				}
				base.Add(p);
				return p;
			}
		}

		private bool mvarReadOnly = false;
		public bool ReadOnly { get { return mvarReadOnly; } set { mvarReadOnly = value; } }
		
		private Image mvarImage = null;
		public Image Image { get { return mvarImage; } set { mvarImage = value; } }
		
		private string mvarName = "";
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private PropertyDataType mvarType = PropertyDataType.String;
		public PropertyDataType Type { get { return mvarType; } set { mvarType = value; } }
		
		private bool mvarExpanded = false;
		public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; } }
		
		private PropertyCollection mvarProperties = new PropertyCollection();
		public PropertyCollection Properties { get { return mvarProperties; } }
		
		private string mvarPropertyCustomTypeName = "";
		public string PropertyCustomTypeName { get { return mvarPropertyCustomTypeName; } set { mvarPropertyCustomTypeName = value; } }
		
		private System.Collections.Specialized.StringCollection mvarValidValues = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection ValidValues { get { return mvarValidValues; } }

		private PropertyGridControl mvarParentControl = null;
		
		private string mvarValue = null;
		public string Value
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
		
		private string mvarDefaultValue = null;
		public string DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }
	}
}
