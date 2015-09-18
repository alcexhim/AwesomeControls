using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public class Rendering : ICloneable
	{
		public class RenderingCollection
			: System.Collections.ObjectModel.Collection<Rendering>
		{

		}

		private ThemeComponentStateReference.ThemeComponentStateReferenceCollection mvarStates = new ThemeComponentStateReference.ThemeComponentStateReferenceCollection();
		public ThemeComponentStateReference.ThemeComponentStateReferenceCollection States { get { return mvarStates; } }

		private RenderingAction.RenderingActionCollection mvarActions = new RenderingAction.RenderingActionCollection();
		public RenderingAction.RenderingActionCollection Actions { get { return mvarActions; } }

		public object Clone()
		{
			Rendering clone = new Rendering();
			foreach (ThemeComponentStateReference item in mvarStates)
			{
				clone.States.Add(item.Clone() as ThemeComponentStateReference);
			}
			foreach (RenderingAction item in mvarActions)
			{
				clone.Actions.Add(item.Clone() as RenderingAction);
			}
			return clone;
		}
	}
}
