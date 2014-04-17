using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PieMenu
{
    public enum PieMenuItemGroupPosition
    {
        Above,
        Below
    }
    public class PieMenuItemGroup
    {
        public class PieMenuItemGroupCollection
            : System.Collections.ObjectModel.Collection<PieMenuItemGroup>
        {
            public PieMenuItemGroup Add(string title, PieMenuItemGroupPosition position)
            {
                PieMenuItemGroup group = new PieMenuItemGroup();
                group.Title = title;
                group.Position = position;
                Add(group);
                return group;
            }
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private PieMenuItemGroupPosition mvarPosition = PieMenuItemGroupPosition.Below;
        public PieMenuItemGroupPosition Position { get { return mvarPosition; } set { mvarPosition = value; } }

        private PieMenuItem.PieMenuItemCollection mvarItems = new PieMenuItem.PieMenuItemCollection();
        public PieMenuItem.PieMenuItemCollection Items { get { return mvarItems; } }

    }
}
