using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AwesomeControls.SmartTag
{
	public partial class SmartTagComponent : Component
	{
		public SmartTagComponent()
		{
			InitializeComponent();
		}

		public SmartTagComponent(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}
	}
}
