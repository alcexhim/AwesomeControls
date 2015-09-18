using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public abstract class RenderingExpressionItem : ICloneable
	{
		public class RenderingExpressionItemCollection
			: System.Collections.ObjectModel.Collection<RenderingExpressionItem>
		{

		}

		public abstract float Evaluate(Dictionary<string, object> variables);
		public abstract object Clone();
	}
}
