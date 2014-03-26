using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AwesomeControls.MultipleDocumentContainer
{
    public delegate void DocumentClosedEventHandler(object sender, DocumentClosedEventArgs e);
    public class DocumentClosedEventArgs : EventArgs
    {
        private Document mvarDocument = null;
        public Document Document { get { return mvarDocument; } }

        public DocumentClosedEventArgs(Document document)
        {
            mvarDocument = document;
        }
    }
}
