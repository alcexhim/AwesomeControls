using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.MultipleDocumentContainer
{
    /// <summary>
    /// Provides an interface by which a control hosted by a Multiple Document Container window reports a change in progress.
    /// </summary>
    public interface IMultipleDocumentContainerProgressBroadcaster
    {
        event MultipleDocumentContainerProgressEventHandler Progress;
    }
}
