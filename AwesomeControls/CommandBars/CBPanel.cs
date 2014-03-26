// Universal Editor custom tool panel
// Copyright (C) 2011  Mike Becker
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Windows.Forms;

namespace AwesomeControls.CommandBars
{
    public class CBPanel : Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (base.ClientRectangle.Width > 0 && base.ClientRectangle.Height > 0)
            {
				System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(base.ClientRectangle, Theming.Theme.CurrentTheme.ColorTable.CommandBarPanelGradientBegin, Theming.Theme.CurrentTheme.ColorTable.CommandBarPanelGradientEnd, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(brush, base.ClientRectangle);
            }
        }
    }
}
