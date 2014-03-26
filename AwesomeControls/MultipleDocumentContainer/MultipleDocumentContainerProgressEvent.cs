using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.MultipleDocumentContainer
{
    public delegate void MultipleDocumentContainerProgressEventHandler(object sender, MultipleDocumentContainerProgressEventArgs e);
    public class MultipleDocumentContainerProgressEventArgs : EventArgs
    {
        private double mvarValue = 0.0;
        public double Value { get { return mvarValue; } set { mvarValue = value; } }

        private string mvarText = null;
        public string Text { get { return mvarText; } set { mvarText = value; } }

        public MultipleDocumentContainerProgressEventArgs(double value, string text = null)
        {
            mvarValue = value;
            mvarText = text;
        }
    }
}
