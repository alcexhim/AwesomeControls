using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.ListView
{
    public class ListViewItem
    {
        public class ListViewItemCollection
            : System.Collections.ObjectModel.Collection<ListViewItem>
        {
            private ListViewControl mvarParent = null;
            public ListViewItemCollection()
            {
                mvarParent = null;
            }
            public ListViewItemCollection(ListViewControl parent)
            {
                mvarParent = parent;
            }

            public ListViewItem Add(string text, params string[] details)
            {
                ListViewItem lvi = new ListViewItem(mvarParent);
                lvi.Text = text;
                foreach (string detail in details)
                {
                    lvi.Details.Add(detail);
                }
                Add(lvi);
                return lvi;
            }

            protected override void InsertItem(int index, ListViewItem item)
            {
                base.InsertItem(index, item);
                item.mvarParent = mvarParent;
                if (mvarParent != null) 
                {
                	mvarParent.ResetBounds();
                	mvarParent.Refresh();
                }
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarParent = null;
                base.RemoveItem(index);
                if (mvarParent != null)
                {
                	mvarParent.ResetBounds();
                	mvarParent.Refresh();
                }
            }
            protected override void ClearItems()
            {
                base.ClearItems();
                if (mvarParent != null)
                {
                	mvarParent.ResetBounds();
                	mvarParent.Refresh();
                }
            }
        }

        private ListViewControl mvarParent = null;
        public ListViewControl Parent { get { return mvarParent; } }

        public ListViewItem() : this(null) { }
        public ListViewItem(ListViewControl parent)
        {
            mvarParent = parent;
            mvarItems = new ListViewItemCollection(parent);
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private string mvarText = String.Empty;
        public string Text
        {
            get { return mvarText; }
            set
            {
                mvarText = value;
                if (mvarParent != null) mvarParent.Refresh();
            }
        }

        private Image mvarImage = null;
        public Image Image { get { return mvarImage; } set { mvarImage = value; } }

        private string mvarImageKey = null;
        public string ImageKey { get { return mvarImageKey; } set { mvarImageKey = value; } }

        private int mvarImageIndex = -1;
        public int ImageIndex { get { return mvarImageIndex; } set { mvarImageIndex = value; } }

        private object mvarData = null;
        public object Data { get { return mvarData; } set { mvarData = value; } }

        private bool mvarExpanded = false;
        public bool Expanded { get { return mvarExpanded; } set { mvarExpanded = value; } }

        private ListViewItem.ListViewItemCollection mvarItems = null;
        public ListViewItem.ListViewItemCollection Items { get { return mvarItems; } }

		private ListViewDetail.ListViewDetailCollection mvarDetails = new ListViewDetail.ListViewDetailCollection();
		public ListViewDetail.ListViewDetailCollection Details { get { return mvarDetails; } }

        private bool mvarSelected = false;
        public bool Selected
        {
            get { return mvarSelected; }
            set
            {
                if (mvarSelected == value) return;
                mvarSelected = value;
                if (mvarParent != null) mvarParent.Refresh();
            }
        }

        private Font mvarFont = null;
        public Font Font { get { return mvarFont; } set { mvarFont = value; } }

        private Color mvarForeColor = Color.Empty;
        public Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

        private Color mvarBackColor = Color.Empty;
        public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

        public void EnsureVisible()
        {
        }

        private string mvarTooltipText = String.Empty;
        /// <summary>
        /// Text to display as a tooltip when the mouse hovers over the item. Only displayed if
        /// <see cref="AwesomeControls.ListView.ListViewControl.ShowItemTooltips" /> is set to
        /// true.
        /// </summary>
        public string TooltipText { get { return mvarTooltipText; } set { mvarTooltipText = value; } }
    }
}
