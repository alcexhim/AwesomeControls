using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
    [Flags()]
    public enum ListViewColumnAutoResizeMode
    {
        Header = 1,
        Content = 2,
        Both = Header | Content
    }
}
