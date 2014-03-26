using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Wizard
{
	public partial class WizardPanel
	{
		private int mvarIndex = 0;
		public int Index { get { return mvarIndex; } set { mvarIndex = value; } }

		private string mvarDescription = String.Empty;
		public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

		private Control mvarControl = null;
		public Control Control { get { return mvarControl; } set { mvarControl = value; } }

		public class WizardPanelCollection
			: System.Collections.ObjectModel.Collection<WizardPanel>
		{

		}

		public class WizardPanelComparer
			: IComparer<WizardPanel>
		{
			#region IComparer<WizardPanel> Members

			public int Compare(WizardPanel x, WizardPanel y)
			{
				return (x.Index.CompareTo(y.Index));
			}

			#endregion
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
