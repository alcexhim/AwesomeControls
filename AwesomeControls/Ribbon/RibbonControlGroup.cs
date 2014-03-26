using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.Ribbon
{
	public class RibbonControlGroup
	{
		public class RibbonControlGroupCollection
			: System.Collections.ObjectModel.Collection<RibbonControlGroup>
		{
		}

		private string mvarName = String.Empty;
		public string Name
		{
			get { return mvarName; }
			set { mvarName = value; }
		}
		private string mvarText = String.Empty;
		public string Text
		{
			get { return mvarText; }
			set { mvarText = value; }
		}

		private RibbonControl.RibbonControlCollection mvarControls = new RibbonControl.RibbonControlCollection();
		public RibbonControl.RibbonControlCollection Controls
		{
			get { return mvarControls; }
		}

		#region Group Action Button
		public event EventHandler ActionButtonClicked;

		protected internal virtual void OnActionButtonClicked(EventArgs e)
		{
			if (ActionButtonClicked != null)
			{
				ActionButtonClicked(this, e);
			}
		}

		private bool mvarActionButtonVisible = false;
		public bool ActionButtonVisible
		{
			get { return mvarActionButtonVisible; }
			set { mvarActionButtonVisible = value; }
		}
		#endregion
	}
}
