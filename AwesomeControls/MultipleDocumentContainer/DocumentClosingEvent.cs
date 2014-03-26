using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.MultipleDocumentContainer
{
    public delegate void DocumentClosingEventHandler(object sender, DocumentClosingEventArgs e);
    public class DocumentClosingEventArgs : CancelEventArgs
    {
        private Document mvarDocument = null;
        public Document Document { get { return mvarDocument; } }

        public DocumentClosingEventArgs(Document document)
        {
            mvarDocument = document;
        }
    }
}
