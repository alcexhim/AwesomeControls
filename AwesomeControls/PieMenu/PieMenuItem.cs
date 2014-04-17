using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.PieMenu
{
    public class PieMenuItem
    {
        public class PieMenuItemCollection
            : System.Collections.ObjectModel.Collection<PieMenuItem>
        {
            public PieMenuItem Add(string name, string title, System.Drawing.Image image = null, EventHandler onclick = null)
            {
                PieMenuItem item = new PieMenuItem();
                item.Name = name;
                item.Title = title;
                item.Image = image;
                item.Click += onclick;
                Add(item); 
                return item;
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private System.Drawing.Image mvarImage = null;
        public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

        internal bool Hover = false;

        public event EventHandler Click;

        internal void OnClick(EventArgs e)
        {
            if (Click != null) Click(this, e);
        }
    }
}
