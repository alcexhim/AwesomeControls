using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ObjectModels.Theming.Metrics
{
	/// <summary>
	/// A metric that defines top, left, right, and bottom components of padding or margin.
	/// </summary>
	public class PaddingMetric : ThemeMetric
	{
		private float mvarTop = 0.0f;
		public float Top { get { return mvarTop; } set { mvarTop = value; } }
		private float mvarBottom = 0.0f;
		public float Bottom { get { return mvarBottom; } set { mvarBottom = value; } }
		private float mvarLeft = 0.0f;
		public float Left { get { return mvarLeft; } set { mvarLeft = value; } }
		private float mvarRight = 0.0f;
		public float Right { get { return mvarRight; } set { mvarRight = value; } }
	}
}
