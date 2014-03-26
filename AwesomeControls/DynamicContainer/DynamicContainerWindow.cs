using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.DynamicContainer
{
    public class DynamicContainerWindow
    {
        public class DynamicContainerWindowCollection
            : System.Collections.ObjectModel.Collection<DynamicContainerWindow>
        {
            private DynamicContainerControl mvarParent = null;
            internal DynamicContainerWindowCollection(DynamicContainerControl parent)
            {
                mvarParent = parent;
            }

            protected override void InsertItem(int index, DynamicContainerWindow item)
            {
                base.InsertItem(index, item);
                mvarParent.InsertItem(this[index]);
            }
            protected override void RemoveItem(int index)
            {
                mvarParent.RemoveItem(this[index]);
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                mvarParent.ClearItems();
                base.ClearItems();
            }


            public DynamicContainerWindow Add(string title, System.Windows.Forms.Control control, System.Drawing.Image image = null)
            {
                DynamicContainerWindow window = new DynamicContainerWindow();
                window.Title = title;
                window.Control = control;
                window.Image = image;
                Add(window);
                return window;
            }
        }

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

        private System.Drawing.Image mvarImage = null;
        public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

        private System.Windows.Forms.Control mvarControl = null;
        public System.Windows.Forms.Control Control { get { return mvarControl; } set { mvarControl = value; } }
    }
}
