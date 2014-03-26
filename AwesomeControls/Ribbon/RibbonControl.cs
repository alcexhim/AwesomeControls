using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.Ribbon
{
	public abstract class RibbonControl
	{
		public class RibbonControlCollection
			: System.Collections.ObjectModel.Collection<RibbonControl>
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

        private bool mvarEnabled = true;
        public bool Enabled
        {
            get { return mvarEnabled; }
            set { mvarEnabled = value; }
        }

        private RibbonControlDisplayStyle mvarDisplayStyle = RibbonControlDisplayStyle.ImageAboveText;
        public RibbonControlDisplayStyle DisplayStyle
        {
            get { return mvarDisplayStyle; }
            set { mvarDisplayStyle = value; }
        }

        private ControlState mvarControlState = ControlState.Normal;
        public ControlState ControlState
        {
            get { return mvarControlState; }
            internal set { mvarControlState = value; }
        }

		public event EventHandler Click;
		protected internal void OnClick(EventArgs e)
		{
			if (Click != null) Click(this, e);
		}

	}

	public class RibbonControlButton
		: RibbonControl
    {
        private ControlState mvarButtonState = ControlState.Normal;
        public ControlState ButtonState
        {
            get { return mvarButtonState; }
            internal set { mvarButtonState = value; }
        }

	}
	public class RibbonControlDropdownButton
		: RibbonControlButton
	{
        private RibbonMenuItem.RibbonMenuItemCollection mvarMenuItems = new RibbonMenuItem.RibbonMenuItemCollection();
        public RibbonMenuItem.RibbonMenuItemCollection MenuItems
        {
            get { return mvarMenuItems; }
        }
	}
	public class RibbonControlSplitButton
		: RibbonControlDropdownButton
    {
        private ControlState mvarDropdownState = ControlState.Normal;
        public ControlState DropdownState
        {
            get { return mvarDropdownState; }
            internal set { mvarDropdownState = value; }
        }

        private bool mvarButtonEnabled = true;
        public bool ButtonEnabled
        {
            get { return mvarButtonEnabled; }
            set { mvarButtonEnabled = value; }
        }

        private bool mvarDropdownEnabled = true;
        public bool DropdownEnabled
        {
            get { return mvarDropdownEnabled; }
            set { mvarDropdownEnabled = value; }
        }
	}
	public class RibbonControlComboBox
		: RibbonControl
	{
	}
	public class RibbonControlTextBox
		: RibbonControl
	{
	}
}
