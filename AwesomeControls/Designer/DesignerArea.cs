using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    public abstract class DesignerArea
    {
        public class DesignerAreaCollection
            : System.Collections.ObjectModel.Collection<DesignerArea>
        {
            private DesignerControl mvarParent = null;
            public DesignerAreaCollection(DesignerControl parent = null)
            {
                mvarParent = parent;
            }

            protected override void InsertItem(int index, DesignerArea item)
            {
                base.InsertItem(index, item);
                item.mvarObjects.Parent = mvarParent;
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarObjects.Parent = null;
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                foreach (DesignerArea area in this)
                {
                    area.mvarObjects.Parent = mvarParent;
                }
                base.ClearItems();
            }
        }

        private bool mvarMovable = false;
        /// <summary>
        /// Determines whether this <see cref="DesignerArea" /> can be moved.
        /// </summary>
        public bool Movable { get { return mvarMovable; } set { mvarMovable = value; } }

        private bool mvarSizable = false;
        /// <summary>
        /// Determines whether this <see cref="DesignerArea" /> can be resized.
        /// </summary>
        public bool Sizable { get { return mvarSizable; } set { mvarSizable = value; } }

        private DesignerObject.DesignerObjectCollection mvarObjects = new DesignerObject.DesignerObjectCollection();
        public DesignerObject.DesignerObjectCollection Objects { get { return mvarObjects; } set { mvarObjects = value; } }

        private int mvarLeft = 0;
        public int Left { get { return mvarLeft; } set { mvarLeft = value; } }
        private int mvarTop = 0;
        public int Top { get { return mvarTop; } set { mvarTop = value; } }
        private int mvarWidth = 0;
        public int Width { get { return mvarWidth; } set { mvarWidth = value; } }
        private int mvarHeight = 0;
        public int Height { get { return mvarHeight; } set { mvarHeight = value; } }
        public int Right { get { return mvarWidth + mvarLeft; } set { mvarWidth = value - mvarLeft; } }
        public int Bottom { get { return mvarHeight + mvarTop; } set { mvarHeight = value - mvarTop; } }

        public System.Drawing.Rectangle ClientRectangle
        {
            get { return new System.Drawing.Rectangle(mvarLeft, mvarTop, mvarWidth, mvarHeight); }
            set { mvarLeft = value.X; mvarTop = value.Y; mvarWidth = value.Width; mvarHeight = value.Height; }
        }

        protected internal virtual void OnBeforePaint(System.Windows.Forms.PaintEventArgs e)
        {
        }
        protected internal virtual void OnAfterPaint(System.Windows.Forms.PaintEventArgs e)
        {
        }
    }
}
