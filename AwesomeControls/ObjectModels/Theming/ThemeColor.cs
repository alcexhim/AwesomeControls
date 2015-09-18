using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public class ThemeColor : ICloneable
	{
		public class ThemeColorCollection
			: System.Collections.ObjectModel.Collection<ThemeColor>
		{
			public ThemeColor this[string name]
			{
				get
				{
					foreach (ThemeColor item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		private String mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			ThemeColor clone = new ThemeColor();
			clone.ID = mvarID;
			clone.Name = (mvarName.Clone() as string);
			clone.Value = (mvarValue.Clone() as string);
			return clone;
		}
	}
}
