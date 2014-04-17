using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PieMenu
{
    public static class PieMenuManager
    {
        private static string mvarTitle = String.Empty;
        public static string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private static PieMenuItemGroup.PieMenuItemGroupCollection mvarGroups = new PieMenuItemGroup.PieMenuItemGroupCollection();
        public static PieMenuItemGroup.PieMenuItemGroupCollection Groups { get { return mvarGroups; } }

        static PieMenuManager()
        {
            System.Windows.Forms.Application.AddMessageFilter(PieMenu.PieMenuMessageFilter.Instance);
        }
    }
}
