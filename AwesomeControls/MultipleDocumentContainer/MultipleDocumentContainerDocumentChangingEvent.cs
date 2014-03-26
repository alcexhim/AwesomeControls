using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace AwesomeControls.MultipleDocumentContainer
{
    public delegate void MultipleDocumentContainerDocumentChangingEventHandler(object sender, MultipleDocumentContainerDocumentChangingEventArgs e);
    public class MultipleDocumentContainerDocumentChangingEventArgs : CancelEventArgs
    {
        private Document mvarOldDocument = null;
        public Document OldDocument { get { return mvarOldDocument; } }

        private Document mvarNewDocument = null;
        public Document NewDocument { get { return mvarNewDocument; } set { mvarNewDocument = value; } }

        public MultipleDocumentContainerDocumentChangingEventArgs(Document oldDocument, Document newDocument)
        {
            mvarOldDocument = oldDocument;
            mvarNewDocument = newDocument;
        }
    }
}
