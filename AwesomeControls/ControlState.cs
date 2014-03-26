using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls
{
    [Flags()]
	public enum ControlState
	{
		Normal = 0,
		Hover = 1,
		Pressed = 2,
		Disabled = 4
	}
}
