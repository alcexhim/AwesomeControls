using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public class Outline : ICloneable
	{
		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		private float mvarWidth = 1.0f;
		public float Width { get { return mvarWidth; } set { mvarWidth = value; } }

		public object Clone()
		{
			Outline clone = new Outline();
			clone.Color = (mvarColor.Clone() as string);
			clone.Width = mvarWidth;
			return clone;
		}
	}
}
