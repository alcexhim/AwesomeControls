using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.PropertyGrid
{
    public class PropertyCustomType
    {
        private PropertyButtonType mvarButtonType = PropertyButtonType.DropDown;
        public PropertyButtonType ButtonType { get { return mvarButtonType; } set { mvarButtonType = value; } }
    }
}
