using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public class DesignerObject
    {
        public class DesignerObjectCollection
            : System.Collections.ObjectModel.Collection<DesignerObject>
        {
            private DesignerControl mvarParent = null;
            internal DesignerControl Parent
            {
                get { return mvarParent; }
                set
                {
                    mvarParent = value;
                    foreach (DesignerObject obj in this)
                    {
                        obj.mvarDesigner = mvarParent;
                    }
                }
            }

            public DesignerObjectCollection(DesignerControl parent = null)
            {
                mvarParent = parent;
            }

            protected override void InsertItem(int index, DesignerObject item)
            {
                base.InsertItem(index, item);
                item.mvarDesigner = mvarParent;
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarDesigner = null;
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                foreach (DesignerObject item in this)
                {
                    item.mvarDesigner = null;
                }
                base.ClearItems();
            }
        }

        public class DesignerObjectReadOnlyCollection
            : System.Collections.ObjectModel.ReadOnlyCollection<DesignerObject>
        {
            public DesignerObjectReadOnlyCollection(IList<DesignerObject> items)
                : base(items)
            {
            }
        }

        public DesignerObject(string name, DesignerObjectClass objectClass)
        {
            mvarName = name;
            mvarClass = objectClass;
        }

        private string mvarName = String.Empty;
        /// <summary>
        /// The name of this <see cref="DesignerObject" /> instance.
        /// </summary>
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private DesignerObjectClass mvarClass = null;
        public DesignerObjectClass Class { get { return mvarClass; } set { mvarClass = value; } }

        private DesignerControl mvarDesigner = null;

        private bool mvarSelected = false;
        public bool Selected
        {
            get { return mvarSelected; }
            set
            {
                mvarSelected = value;
                if (mvarDesigner != null && value)
                {
                    mvarDesigner.OnDesignerObjectSelected(new DesignerObjectSelectedEventArgs(this));
                }
            }
        }

        public System.Drawing.Rectangle Bounds
        {
            get { return new System.Drawing.Rectangle(mvarLeft, mvarTop, mvarWidth, mvarHeight); }
            set
            {
                mvarLeft = value.Left;
                mvarTop = value.Top;
                mvarWidth = value.Width;
                mvarHeight = value.Height;
            }
        }

        private int mvarLeft = 0;
        /// <summary>
        /// The distance, in pixels, from the left edge of the parent <see cref="DesignerControl" />.
        /// </summary>
        public int Left { get { return mvarLeft; } set { mvarLeft = value; } }
        
        private int mvarTop = 0;
        /// <summary>
        /// The distance, in pixels, from the top edge of the parent <see cref="DesignerControl" />.
        /// </summary>
        public int Top { get { return mvarTop; } set { mvarTop = value; } }

        private int mvarWidth = 0;
        /// <summary>
        /// The width, in pixels, of the <see cref="DesignerObject" />.
        /// </summary>
        public int Width { get { return mvarWidth; } set { mvarWidth = value; } }

        private int mvarHeight = 0;
        /// <summary>
        /// The height, in pixels, of the <see cref="DesignerObject" />.
        /// </summary>
        public int Height { get { return mvarHeight; } set { mvarHeight = value; } }

        private int mvarZIndex = 0;
        /// <summary>
        /// The index in the Z-order
        /// </summary>
        public int ZIndex { get { return mvarZIndex; } set { mvarZIndex = value; } }

        public int Right { get { return mvarWidth + mvarLeft; } set { mvarWidth = value - mvarLeft; } }
        public int Bottom { get { return mvarHeight + mvarTop; } set { mvarHeight = value - mvarTop; } }

        private bool mvarLocked = false;
        /// <summary>
        /// Determines whether the user can move or resize the <see cref="DesignerObject" />.
        /// </summary>
        public bool Locked { get { return mvarLocked; } set { mvarLocked = value; } }

        private Dictionary<string, object> mvarProperties = new Dictionary<string, object>();
        public Dictionary<string, object> Properties { get { return mvarProperties; } }
    }
}
