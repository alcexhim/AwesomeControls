using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public class ThemeComponent : ICloneable
	{
		public class ThemeComponentCollection
			: System.Collections.ObjectModel.Collection<ThemeComponent>
		{
			public ThemeComponent this[Guid id]
			{
				get
				{
					foreach (ThemeComponent item in this)
					{
						if (item.ID == id) return item;
					}
					return null;
				}
			}
			public bool Contains(Guid id)
			{
				return (this[id] != null);
			}
		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private Guid mvarInheritsComponentID = Guid.Empty;
		public Guid InheritsComponentID { get { return mvarInheritsComponentID; } set { mvarInheritsComponentID = value; } }

		private ThemeComponent mvarInheritsComponent = null;
		public ThemeComponent InheritsComponent { get { return mvarInheritsComponent; } set { mvarInheritsComponent = value; } }

		private ThemeComponentState.ThemeComponentStateCollection mvarStates = new ThemeComponentState.ThemeComponentStateCollection();
		public ThemeComponentState.ThemeComponentStateCollection States { get { return mvarStates; } }

		private Rendering.RenderingCollection mvarRenderings = new Rendering.RenderingCollection();
		public Rendering.RenderingCollection Renderings { get { return mvarRenderings; } }

		public object Clone()
		{
			ThemeComponent clone = new ThemeComponent();
			clone.ID = mvarID;
			foreach (Rendering item in mvarRenderings)
			{
				clone.Renderings.Add(item.Clone() as Rendering);
			}
			return clone;
		}
	}
}
