﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
	public interface IPropertyGridItem
	{
		string Title { get; }
		string Description { get; }
	}
}
