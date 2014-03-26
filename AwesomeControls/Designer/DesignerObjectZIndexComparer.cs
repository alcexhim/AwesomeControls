using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    internal class DesignerObjectZIndexComparer : IComparer<DesignerObject>
    {
        public int Compare(DesignerObject x, DesignerObject y)
        {
            return x.ZIndex.CompareTo(y.ZIndex);
        }
    }
}
