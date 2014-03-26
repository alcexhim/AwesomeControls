using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls.Designer
{
    /// <summary>
    /// Provides the abstract base class for a <see cref="DesignerObject" /> object class, which defines how a
    /// particular <see cref="DesignerObject" /> instance is rendered on the <see cref="DesignerControl" />.
    /// </summary>
    public abstract class DesignerObjectClass
    {
        public class DesignerObjectClassCollection
            : System.Collections.ObjectModel.Collection<DesignerObjectClass>
        {
        }

        /// <summary>
        /// Renders the non-client (i.e. common to all instances) area of the associated <see cref="DesignerObject" />.
        /// </summary>
        /// <param name="e"></param>
        protected internal abstract void RenderNonClientArea(DesignerObjectPaintEventArgs e);

        /// <summary>
        /// Renders the client (i.e. instance-dependent) area of the associated <see cref="DesignerObject" />.
        /// </summary>
        /// <param name="e"></param>
        protected internal abstract void RenderClientArea(DesignerObjectPaintEventArgs e);

        private System.Windows.Forms.Padding mvarClientAreaPadding = new System.Windows.Forms.Padding();
        /// <summary>
        /// The amount of space between the non-client area and the client area.
        /// </summary>
        protected internal virtual System.Windows.Forms.Padding ClientAreaPadding
        {
            get { return mvarClientAreaPadding; }
        }
    }
}
