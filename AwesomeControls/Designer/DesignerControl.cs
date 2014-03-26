using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.Designer
{
    [DefaultEvent("DesignerObjectMouseDoubleClick")]
    public partial class DesignerControl : UserControl
    {
        public DesignerControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            mvarAreas = new DesignerArea.DesignerAreaCollection(this);
        }

        public event DesignerObjectLocationChangingEventHandler DesignerObjectLocationChanging;
        protected virtual void OnDesignerObjectLocationChanging(DesignerObjectLocationChangingEventArgs e)
        {
            if (DesignerObjectLocationChanging != null) DesignerObjectLocationChanging(this, e);
        }
        public event DesignerObjectLocationChangedEventHandler DesignerObjectLocationChanged;
        protected virtual void OnDesignerObjectLocationChanged(DesignerObjectLocationChangedEventArgs e)
        {
            if (DesignerObjectLocationChanged != null) DesignerObjectLocationChanged(this, e);
        }
        
        public event DesignerObjectMouseEventHandler DesignerObjectMouseDoubleClick;
        protected virtual void OnDesignerObjectMouseDoubleClick(DesignerObjectMouseEventArgs e)
        {
            if (DesignerObjectMouseDoubleClick != null) DesignerObjectMouseDoubleClick(this, e);
        }

        public event DesignerObjectSelectedEventHandler DesignerObjectSelected;
        protected internal virtual void OnDesignerObjectSelected(DesignerObjectSelectedEventArgs e)
        {
            if (DesignerObjectSelected != null) DesignerObjectSelected(this, e);
        }

        private DesignerArea.DesignerAreaCollection mvarAreas = new DesignerArea.DesignerAreaCollection();
        public DesignerArea.DesignerAreaCollection Areas { get { return mvarAreas; } }

        public DesignerArea AreaHitTest(Point point)
        {
            foreach (DesignerArea area in mvarAreas)
            {
                if (point.X >= area.Left && point.X <= area.Right && point.Y >= area.Top && point.Y <= area.Bottom) return area;
            }
            return null;
        }
        public DesignerObject HitTest(Point point)
        {
            DesignerArea area = AreaHitTest(point);
            if (area == null) return null;

            List<DesignerObject> list = area.Objects.ToList<DesignerObject>();
            list.Sort(new DesignerObjectZIndexComparer());

            foreach (DesignerObject obj in list)
            {
                if (point.X >= obj.Left && point.X <= obj.Right && point.Y >= obj.Top && point.Y <= obj.Bottom)
                {
                    return obj;
                }
            }
            return null;
        }

        private Point mvarDragPoint = new Point();
        private Point mvarInitialDragPoint = new Point();
        private DesignerObject mvarDragObject = null;
        private bool mvarDragging = false;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            DesignerObject dobj = HitTest(e.Location);
            if (dobj != null)
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    dobj.Selected = !dobj.Selected;
                }
                else
                {
                    foreach (DesignerArea area in mvarAreas)
                    {
                        foreach (DesignerObject dobj1 in area.Objects)
                        {
                            dobj1.Selected = (dobj1 == dobj);
                        }
                    }
                }

                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (!dobj.Locked)
                    {
                        mvarInitialDragPoint = new Point(e.Location.X - dobj.Left, e.Location.Y - dobj.Top);
                        mvarDragPoint = new Point(e.Location.X - mvarInitialDragPoint.X, e.Location.Y - mvarInitialDragPoint.Y);
                        mvarDragObject = dobj;
                    }
                }
            }
            else
            {
                foreach (DesignerArea area in mvarAreas)
                {
                    foreach (DesignerObject dobj1 in area.Objects)
                    {
                        dobj1.Selected = false;
                    }

                    if (mvarEnableCreation && e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        mvarInitialDragPoint = new Point(e.Location.X, e.Location.Y);
                        mvarDragPoint = new Point(e.Location.X - mvarInitialDragPoint.X, e.Location.Y - mvarInitialDragPoint.Y);
                        mvarDragObject = null;
                        mvarDragging = true;
                    }
                }
            }
            Refresh();
        }

        private Rectangle mvarLastDragRect = new Rectangle();

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            DesignerObject dobj = HitTest(e.Location);
            if (dobj != null)
            {
                OnDesignerObjectMouseDoubleClick(new DesignerObjectMouseEventArgs(dobj, e.Button, e.Clicks, e.X, e.Y, e.Delta));
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            DesignerObject dobj = HitTest(e.Location);
            if (!mvarDragging)
            {
                if (dobj != null && !dobj.Locked)
                {
                    Cursor = Cursors.SizeAll;
                }
                else if (mvarEnableCreation)
                {
                    Cursor = Cursors.Cross;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mvarDragObject != null)
                {
                    mvarDragging = true;
                    mvarLastDragRect = new Rectangle(mvarDragPoint, mvarDragObject.Bounds.Size);
                    mvarLastDragRect.Inflate(2, 2);

                    mvarDragPoint = new Point(e.Location.X - mvarInitialDragPoint.X, e.Location.Y - mvarInitialDragPoint.Y);
                    Cursor = Cursors.Default;

                    Rectangle rect = new Rectangle(mvarDragPoint, mvarDragObject.Bounds.Size);
                    rect.Inflate(2, 2);
                    Invalidate(mvarLastDragRect);
                    Invalidate(rect);
                }
                else if (mvarDragging)
                {
                    Size size = new System.Drawing.Size(Math.Abs(mvarInitialDragPoint.X + mvarDragPoint.X), Math.Abs(mvarInitialDragPoint.Y + mvarDragPoint.Y));
                    mvarLastDragRect = new Rectangle(mvarDragPoint, size);
                    mvarLastDragRect.Inflate(2, 2);

                    mvarDragPoint = new Point(e.Location.X - mvarInitialDragPoint.X, e.Location.Y - mvarInitialDragPoint.Y);
                    Cursor = Cursors.Default;

                    Rectangle rect = new Rectangle(mvarDragPoint, size);
                    rect.Inflate(2, 2);
                    Invalidate(mvarLastDragRect);
                    Invalidate(rect);
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (mvarDragging)
                {
                    if (mvarDragObject != null)
                    {
                        DesignerObjectLocationChangingEventArgs dolce = new DesignerObjectLocationChangingEventArgs(mvarDragObject, mvarDragObject.Bounds.Location, mvarDragPoint);
                        DesignerObjectLocationChangedEventArgs dolde = new DesignerObjectLocationChangedEventArgs(mvarDragObject, mvarDragObject.Bounds.Location, mvarDragPoint);
                        OnDesignerObjectLocationChanging(dolce);
                        if (dolce.Cancel) return;

                        mvarDragObject.Bounds = new Rectangle(mvarDragPoint, mvarDragObject.Bounds.Size);
                        Refresh();
                        
                        OnDesignerObjectLocationChanged(dolde);
                    }
                    else if (mvarDefaultObjectClass != null)
                    {
                        DesignerObject dobjNew = new DesignerObject("Object1", mvarDefaultObjectClass);

                        Size size = new System.Drawing.Size(Math.Abs(mvarDragPoint.X), Math.Abs(mvarDragPoint.Y));
                        dobjNew.Bounds = new Rectangle(mvarInitialDragPoint, size);

                        // mvarObjects.Add(dobjNew);

                        mvarEnableCreation = false;
                        mvarDefaultObjectClass = null;
                    }
                }
            }

            mvarDragging = false;
            mvarDragObject = null;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (DesignerArea area in mvarAreas)
            {
                area.OnBeforePaint(e);

                List<DesignerObject> list = area.Objects.ToList<DesignerObject>();
                list.Sort(new DesignerObjectZIndexComparer());

                foreach (DesignerObject obj in list)
                {
                    // e.Graphics.SetClip(obj.Bounds);
                    DesignerObjectPaintEventArgs pe = new DesignerObjectPaintEventArgs(obj, e.Graphics, obj.Bounds);

                    obj.Class.RenderNonClientArea(pe);
                    obj.Class.RenderClientArea(pe);

                    if (obj.Selected)
                    {
                        // draw the selection rectangle around this object
                        Rectangle selectionRect = obj.Bounds;
                        selectionRect.Inflate(2, 2);
                        e.Graphics.DrawRectangle(DrawingTools.Pens.FocusPen, selectionRect);
                    }
                }

                if (mvarDragging)
                {
                    if (mvarDragObject != null)
                    {
                        e.Graphics.DrawRectangle(DrawingTools.Pens.DragPen, new Rectangle(mvarDragPoint, mvarDragObject.Bounds.Size));
                    }
                    else
                    {
                        Size size = new System.Drawing.Size(Math.Abs(mvarDragPoint.X), Math.Abs(mvarDragPoint.Y));
                        e.Graphics.DrawRectangle(DrawingTools.Pens.DragPen, new Rectangle(mvarInitialDragPoint, size));
                    }
                }

                area.OnAfterPaint(e);
            }
        }

        private bool mvarEnableCreation = false;
        public bool EnableCreation { get { return mvarEnableCreation; } set { mvarEnableCreation = value; } }

        private DesignerObjectClass mvarDefaultObjectClass = null;
        /// <summary>
        /// The object class of the object that is created when <see cref="EnableCreation" /> is set to True.
        /// </summary>
        public DesignerObjectClass DefaultObjectClass { get { return mvarDefaultObjectClass; } set { mvarDefaultObjectClass = value; } }


        public DesignerObject.DesignerObjectReadOnlyCollection SelectedObjects
        {
            get
            {
                List<DesignerObject> objects = new List<DesignerObject>();
                foreach (DesignerArea area in mvarAreas)
                {
                    foreach (DesignerObject obj in area.Objects)
                    {
                        if (obj.Selected) objects.Add(obj);
                    }
                }
                return new DesignerObject.DesignerObjectReadOnlyCollection(objects);
            }
        }
    }
}
