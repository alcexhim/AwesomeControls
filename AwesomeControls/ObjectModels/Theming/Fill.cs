using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming
{
	public abstract class Fill : ICloneable
	{
		public abstract object Clone();
	}
	public class SolidFill : Fill
	{
		private string mvarColor = String.Empty;
		public string Color { get { return mvarColor; } set { mvarColor = value; } }

		public SolidFill()
		{

		}
		public SolidFill(string color)
		{
			mvarColor = color;
		}

		public override object Clone()
		{
			SolidFill clone = new SolidFill();
			clone.Color = (mvarColor.Clone() as string);
			return clone;
		}
	}
}
