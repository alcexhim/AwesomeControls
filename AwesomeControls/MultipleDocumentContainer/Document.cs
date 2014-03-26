using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.MultipleDocumentContainer
{
    public class Document
    {
        public class DocumentCollection
            : System.Collections.ObjectModel.Collection<Document>
        {
            private MultipleDocumentContainerControl mvarParent = null;
            internal DocumentCollection(MultipleDocumentContainerControl parent)
            {
                mvarParent = parent;
            }

            protected override void ClearItems()
            {
                base.ClearItems();
                mvarParent.ClearItems();
            }
            protected override void InsertItem(int index, Document item)
            {
                base.InsertItem(index, item);
                item.mvarParent = mvarParent;
                mvarParent.InsertItem(item);
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarParent = null;
                mvarParent.RemoveItem(this[index]);
                base.RemoveItem(index);
            }

            public Document Add(string title, System.Windows.Forms.Control control)
            {
                return Add(title, title, control);
            }
            public Document Add(string title, string description, System.Windows.Forms.Control control)
            {
                Document doc = new Document(title, control);
                doc.ToolTipText = description;
                Add(doc);
                return doc;
            }

            public Document this[System.Windows.Forms.Control hostedControl]
            {
                get
                {
                    foreach (Document d in this)
                    {
                        if (d.Control == hostedControl) return d;
                    }
                    return null;
                }
            }

            public bool Contains(string title)
            {
                foreach (Document doc in this)
                {
                    if (doc.Title == title) return true;
                }
                return false;
            }
        }

        private MultipleDocumentContainerControl mvarParent = null;

        private string mvarTitle = String.Empty;
        public string Title { get { return mvarTitle; } set { mvarTitle = value; if (mvarParent != null) mvarParent.UpdateItem(this); } }

        private string mvarToolTipText = String.Empty;
        public string ToolTipText { get { return mvarToolTipText; } set { mvarToolTipText = value; } }

        private System.Drawing.Image mvarImage = null;
        public System.Drawing.Image Image { get { return mvarImage; } set { mvarImage = value; } }

        private System.Windows.Forms.Control mvarControl = null;
        public System.Windows.Forms.Control Control { get { return mvarControl; } set { mvarControl = value; } }

        public Document(string title, System.Windows.Forms.Control control)
        {
            mvarTitle = title;
            mvarControl = control;
        }
    }
}
