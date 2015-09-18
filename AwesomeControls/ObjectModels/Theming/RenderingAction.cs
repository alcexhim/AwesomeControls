using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public abstract class RenderingAction : ICloneable
	{
		public class RenderingActionCollection
			: System.Collections.ObjectModel.Collection<RenderingAction>
		{

		}

		public abstract object Clone();
	}
}
