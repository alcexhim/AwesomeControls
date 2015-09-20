// DockingWindows container for Windows Forms
// Copyright (C) 2010-2012  Mike Becker
// 
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

using System;
using System.Collections.Generic;
using System.Drawing;

namespace AwesomeControls.DockingWindows
{
	/// <summary>
	/// The top-level docking window host that resides in a Windows Forms form.
	/// </summary>
	public partial class DockingContainerControl : System.Windows.Forms.UserControl
	{
		private DockingWindow.DockingWindowCollection mvarWindows = null;
		public DockingWindow.DockingWindowCollection Windows { get { return mvarWindows; } }

		private DockingArea.DockingAreaCollection mvarAreas = null;
		public DockingArea.DockingAreaCollection Areas { get { return mvarAreas; } }

		public bool IsActive
		{
			get
			{
				bool active = FindForm().ContainsFocus;
				return active;
			}
		}

		public DockingContainerControl()
		{
			InitializeComponent();


			base.DoubleBuffered = true;
			mvarWindows = new DockingWindow.DockingWindowCollection(this);
			mvarAreas = new DockingArea.DockingAreaCollection();
			messageFilter = new DockingContainerMessageFilter(this);
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Theming.Theme.CurrentTheme.DrawContentAreaBackground(e.Graphics, new System.Drawing.Rectangle(0, 0, base.Width, base.Height));

			DrawDockPanelAreas(e.Graphics, mvarAreas, new Rectangle(0, 0, Width, Height));
		}

		private void DrawDockPanelAreas(Graphics g, DockingArea.DockingAreaCollection areas, Rectangle rectParent)
		{
			if (areas.Count == 0) return;

			#region Bottom
			if (areas.Contains(DockPosition.Bottom))
			{
				Rectangle rect = new Rectangle(rectParent.Left, rectParent.Top, rectParent.Width, rectParent.Height);
				DrawDockPanelArea(g, areas[DockPosition.Bottom], rect);
			}
			#endregion
			#region Center
			if (areas.Contains(DockPosition.Center))
			{
				Rectangle rect = new Rectangle(rectParent.Left, rectParent.Top, rectParent.Width, rectParent.Height);
				DrawDockPanelArea(g, areas[DockPosition.Center], rect);
			}
			#endregion
			#region Left
			if (areas.Contains(DockPosition.Left))
			{
				Rectangle rect = new Rectangle(rectParent.Left, rectParent.Top, rectParent.Width, rectParent.Height);
				DrawDockPanelArea(g, areas[DockPosition.Left], rect);
			}
			#endregion
			#region Right
			if (areas.Contains(DockPosition.Right))
			{
				Rectangle rect = new Rectangle(rectParent.Left, rectParent.Top, rectParent.Width, rectParent.Height);
				DrawDockPanelArea(g, areas[DockPosition.Right], rect);
			}
			#endregion
			#region Top
			if (areas.Contains(DockPosition.Top))
			{
				Rectangle rect = new Rectangle(rectParent.Left, rectParent.Top, rectParent.Width, rectParent.Height);
				DrawDockPanelArea(g, areas[DockPosition.Top], rect);
			}
			#endregion
		}

		private DockingContainerMessageFilter messageFilter = null;
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			System.Windows.Forms.Application.AddMessageFilter(messageFilter);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			System.Windows.Forms.Application.RemoveMessageFilter(messageFilter);
		}

		private void DrawDockPanelArea(Graphics g, DockingArea area, Rectangle rect)
		{
			Rectangle rectTitle = rect;
			
			DockingArea.DockingAreaCollection parentAreas = null;
			if (area.ParentArea != null) 
			{
				parentAreas = area.ParentArea.Areas;
			}
			else
			{
				parentAreas = mvarAreas;
			}

			switch (area.Position)
			{
				#region Bottom
				case DockPosition.Bottom:
				{
					if (area.IsDocked)
					{
						int bottom = rect.Bottom;
						rect.Y = rect.Bottom - area.Size - 20;
						rect.Height = area.Size + 20;

						if (parentAreas[DockPosition.Top].Windows.Count > 0 && parentAreas[DockPosition.Top].IsDocked)
						{
							rect.Y += parentAreas[DockPosition.Top].Size;
						}
						if (parentAreas[DockPosition.Left].Windows.Count > 0 && parentAreas[DockPosition.Left].IsDocked)
						{
							rect.X += parentAreas[DockPosition.Left].Size;
						}

						rectTitle = rect;
						rectTitle.Y = rect.Bottom - 20;
						rectTitle.Width = rect.Width;
						rectTitle.Height = 20;
					}
					else
					{
						rect.Y = rect.Bottom - 20;
						rect.Height = 20;
						if (parentAreas[DockPosition.Top].Windows.Count > 0 && parentAreas[DockPosition.Top].IsDocked)
						{
							rect.Y += area.ParentArea.Areas[DockPosition.Top].Size;
						}
						if (parentAreas[DockPosition.Left].Windows.Count > 0 && parentAreas[DockPosition.Left].IsDocked)
						{
							rect.X += parentAreas[DockPosition.Left].Size;
						}
						rectTitle = rect;
						rectTitle.Width = rect.Width;
						rectTitle.Height = 20;
					}
					break;
				}
				#endregion
				#region Center
				case DockPosition.Center:
				{
					rect.X += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabScrollArrowSize;
					if (parentAreas[DockPosition.Top].Windows.Count > 0)
					{
						if (parentAreas[DockPosition.Top].IsDocked)
						{
							rect.Y = area.ParentArea.Areas[DockPosition.Top].Size;
						}
						else
						{
							rect.Y = 20;
						}
					}
					if (parentAreas[DockPosition.Left].Windows.Count > 0)
					{
						if (parentAreas[DockPosition.Left].IsDocked)
						{
							rect.X = parentAreas[DockPosition.Left].Size;
						}
						else
						{
							rect.X = 20;
						}
					}

					rectTitle = rect;
					rectTitle.Width = rect.Width;
					rectTitle.Height = 20;

					// TODO: FIX THIS!!
					if (area.ParentArea != null && area.ParentArea.Position == DockPosition.Center)
					{
						rect.X -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabScrollArrowSize;
						g.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackgroundSelected), rect.X, rect.Top + 20, rect.Width, 2);
						rect.X += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabScrollArrowSize;
					}
					break;
				}
				#endregion
				#region Left
				case DockPosition.Left:
				{
					if (area.IsDocked)
					{
						rect.Width = area.Size;
						rectTitle = rect;
						rectTitle.Height = 20;
					}
					else
					{
						rect.Width = area.Size;
						rectTitle = rect;
						rectTitle.Width = 20;
						rectTitle.Height = rect.Height;
					}
					break;
				}
				#endregion
				#region Right
				case DockPosition.Right:
				{
					rect.X = rect.Right - area.Size;
					rect.Width = area.Size;

					rectTitle = rect;
					if (area.IsDocked)
					{
						rectTitle.Width = rect.Width;
						rectTitle.Height = 20;
						rectTitle.Y = rect.Bottom - rectTitle.Height;
					}
					else
					{
						rectTitle.Width = 20;
						rectTitle.Height = rect.Height;
					}
					break;
				}
				#endregion
				#region Top
				case DockPosition.Top:
				{
					if (area.IsDocked)
					{
						// rectTitle.Width = Width;
						// rectTitle.Height = 20;
						if (area.Windows.Count > 0)
						{
							rectTitle.Height = area.Size;
						}
						rectTitle.Y = rectTitle.Bottom - Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}
					else
					{
						rect.Height = area.Size;

						rectTitle = rect;
						rectTitle.Width = rect.Width;
						rectTitle.Height = 20;
					}
					break;
				}
				#endregion
			}

			if (area.Windows.Count > 0)
			{
				// Theming.Theme.CurrentTheme.DrawDockPanelTitleBarBackground(g, rectTitle, true);

				if (area.ParentArea != null && area.ParentArea.Areas.Contains(DockPosition.Right))
				{
					rect.Width -= area.ParentArea.Areas[DockPosition.Right].Size; 
				}

				foreach (DockingWindow window in area.Windows)
				{
					Color BackColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabBackgroundNormalGradientBegin;
					Color ForeColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabTextNormal;

					Font Font = Theming.Theme.CurrentTheme.FontTable.Default;
					if (Font == null) Font = this.Font;

					if (area.IsDocked && area.Position != DockPosition.Center)
					{
						Theming.Theme.CurrentTheme.DrawDockPanelTitleBarBackground(g, new Rectangle(rect.X, rect.Y, rect.Width, Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize), true);
					}

					if (area.Position == DockPosition.Center)
					{
						if (window.Selected)
						{
							BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackgroundSelected;
							ForeColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabTextSelected;

							Font = Theming.Theme.CurrentTheme.FontTable.DocumentTabTextSelected;
							if (Font == null) Font = this.Font;
						}
						else if (window.TabState == ControlState.Hover)
						{
							BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackgroundHover;
							ForeColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabTextHover;
						}
						else
						{
							BackColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabBackground;
							ForeColor = Theming.Theme.CurrentTheme.ColorTable.DocumentTabText;
						}
					}
					else
					{
						if (!window.Selected)
						{
							if (window.TabState == ControlState.Hover)
							{
								BackColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabBackgroundHoverGradientBegin;
								ForeColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabTextHover;
							}

							else
							{
								BackColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabBackgroundGradientBegin;
								ForeColor = Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabText;
							}
						}
					}

					Size sz = System.Windows.Forms.TextRenderer.MeasureText(window.TabTitle, Font);
					Rectangle rectTitleText = rectTitle;
					rectTitleText.Y += 3;
					rectTitleText.Size = sz;
					rectTitle.Width = sz.Width;
					rectTitle.Height = Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;

					if (area.Position == DockPosition.Center)
					{
						Theming.Theme.CurrentTheme.DrawDocumentTabBackground(g, rectTitle, window.TabState, area.Position, window.Selected, ActiveControl == window.Control);
					}
					else
					{
						Theming.Theme.CurrentTheme.DrawDockPanelTabBackground(g, rectTitle, window.TabState, area.Position, window.Selected, ActiveControl == window.Control);
					}

					if (area.Position == DockPosition.Top || area.Position == DockPosition.Bottom || area.Position == DockPosition.Center)
					{
						rectTitle.Width = rectTitleText.Width;
						rectTitle.Height = Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}
					else if (area.Position == DockPosition.Left || area.Position == DockPosition.Right)
					{
						rectTitle.Height = rectTitleText.Width;
						rectTitle.Width = Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}

					TabBounds[window] = rectTitle;

					if (area.IsDocked)
					{
						System.Windows.Forms.TextRenderer.DrawText(g, window.TabTitle, Font, rectTitleText, ForeColor);
						rectTitle.X += rectTitleText.Width + 1;
					}
					else
					{
						// TODO: figure out how to understand this...
						// http://stackoverflow.com/questions/4460258/c-rotated-text-align
						g.TranslateTransform(rectTitleText.X, rectTitleText.Y + (sz.Height * 4));
						g.RotateTransform(270);
						g.DrawString(window.TabTitle, Font, new SolidBrush(ForeColor), new RectangleF(0, 0, sz.Width, sz.Height));
						g.ResetTransform();

						rectTitle.Y += rectTitleText.Width + 1;
					}
				}
			}
			else
			{
				DrawDockPanelAreas(g, area.Areas, rect);
			}
		}

		private void RenderDockable(DockingWindow wnd, System.Drawing.Graphics g)
		{
			int x = 0, y = 6;
			RenderDockable(wnd, g, ref x, ref y);
		}
		private void RenderDockable(DockingWindow wnd, System.Drawing.Graphics g, ref int x, ref int y)
		{
			int w = 0, h = 21;

			#region Docking Window
			#region Common
			DockingWindow dw = (wnd as DockingWindow);
			if (!dw.Visible) return;

			System.Drawing.Size sz = System.Windows.Forms.TextRenderer.MeasureText(dw.TabTitle, Font);
			w += (sz.Width + 2);
			if (dw.Image != null)
			{
				w += 18;
			}
			#endregion
			/*
			if (dw.ParentDock != null)
			{
				#region Left Docking
				if (dw.ParentDock.DockPosition == DockPosition.Left)
				{
					if (dw.Behavior == DockBehavior.AutoHide)
					{
						System.Drawing.Rectangle rectTab = new System.Drawing.Rectangle(x, y, h, w);

						Theming.Theme.CurrentTheme.DrawDockPanelTabBackground(g, rectTab, dw.TabState, DockPosition.Left, dw.Selected, dw.ParentDock.Focused);
						System.Drawing.Rectangle rectText = new System.Drawing.Rectangle(rectTab.Left - 5, rectTab.Top, rectTab.Width, rectTab.Height);
						g.DrawText(dw.TabTitle, Font, rectText, Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabText, System.Drawing.RotateFlipType.Rotate90FlipNone);

						UpdateTabBounds(dw, rectTab);

						y += (w + 1);
					}
					else
					{
						System.Drawing.Rectangle rectTab = new System.Drawing.Rectangle(x, base.Height - h, w, h);

						Theming.Theme.CurrentTheme.DrawDockPanelTabBackground(g, rectTab, dw.TabState, DockPosition.Left, dw.Selected, dw.ParentDock.Focused);
						System.Drawing.Rectangle rectText = new System.Drawing.Rectangle(rectTab.Left, rectTab.Top, rectTab.Width, rectTab.Height);
						g.DrawText(dw.TabTitle, Font, rectText, Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabText, System.Drawing.RotateFlipType.RotateNoneFlipNone);

						UpdateTabBounds(dw, rectTab);
					}
				}
				#endregion
				#region Center Docking
				else if (dw.ParentDock.DockPosition == DockPosition.Center)
				{
					if (dw.Image != null)
					{
						w += 18;
					}

					if (dw.EnableClose)
					{
						w += 20;
					}

					int leftSize = -1;
					int topSize = -1;
					int rightSize = -1;
					int bottomSize = -1;
					CalculateSizes(dw, out leftSize, out topSize, out rightSize, out bottomSize);

					if (leftSize > -1 && leftSize >= x) x += leftSize;
					if (topSize > -1 && topSize >= y) y += topSize;
					if (rightSize > -1) w -= rightSize;
					if (bottomSize > -1) h -= bottomSize;

					System.Drawing.Rectangle rectTab = new System.Drawing.Rectangle(x, y, w, h);

					#region Tab Background
					Theming.Theme.CurrentTheme.DrawDockPanelTabBackground(g, rectTab, dw.TabState, DockPosition.Center, dw.Selected, dw.ParentDock.Focused);
					System.Drawing.Rectangle rectText = new System.Drawing.Rectangle(rectTab.Left + 1, rectTab.Top + 4, sz.Width, sz.Height);
					#endregion
					#region Tab Image
					if (dw.Image != null)
					{
						g.DrawImage(dw.Image, new System.Drawing.Rectangle(rectTab.Left + 1, rectTab.Top + 1, 16, 16));
						rectText.X += 16;
					}
					#endregion
					#region Tab Text
					System.Drawing.Color color = Theming.Theme.CurrentTheme.ColorTable.DocumentTabText;
					if (dw.Selected)
					{
						color = Theming.Theme.CurrentTheme.ColorTable.DocumentTabTextSelected;
					}
					g.DrawText(dw.TabTitle, Font, rectText, color, System.Drawing.RotateFlipType.RotateNoneFlipNone);
					#endregion
					#region Tab Close Button
					if (dw.EnableClose)
					{
						if (dw.Selected)
						{
							System.Drawing.Point ptCloseUpperLeftCorner = new System.Drawing.Point(rectTab.Right - 13, rectTab.Top + 7);
							System.Drawing.Point ptCloseLowerRightCorner = new System.Drawing.Point(rectTab.Right - 6, rectTab.Bottom - 7);
							System.Drawing.Point ptCloseLowerLeftCorner = new System.Drawing.Point(rectTab.Right - 13, rectTab.Bottom - 7);
							System.Drawing.Point ptCloseUpperRightCorner = new System.Drawing.Point(rectTab.Right - 6, rectTab.Top + 7);

							System.Drawing.Pen pen1 = new System.Drawing.Pen(System.Drawing.Color.FromArgb(117, 99, 61));
							System.Drawing.Pen pen2 = new System.Drawing.Pen(System.Drawing.Color.FromArgb(186, 172, 139));
								
							// inner most X
							g.DrawLine(pen1, ptCloseUpperLeftCorner, ptCloseLowerRightCorner);
							g.DrawLine(pen1, ptCloseLowerLeftCorner, ptCloseUpperRightCorner);

							// outer left
							g.DrawLine(pen2, ptCloseUpperLeftCorner.X - 1, ptCloseUpperLeftCorner.Y, ptCloseUpperLeftCorner.X + 2, ptCloseUpperLeftCorner.Y + 3);
							g.DrawLine(pen2, ptCloseLowerLeftCorner.X - 1, ptCloseLowerLeftCorner.Y, ptCloseLowerLeftCorner.X + 2, ptCloseLowerLeftCorner.Y - 3);
							// outer bottom
							g.DrawLine(pen2, ptCloseLowerLeftCorner.X + 1, ptCloseLowerLeftCorner.Y, ptCloseLowerLeftCorner.X + 3, ptCloseLowerLeftCorner.Y - 2);
							g.DrawLine(pen2, ptCloseLowerRightCorner.X - 1, ptCloseLowerRightCorner.Y, ptCloseLowerRightCorner.X - 3, ptCloseLowerRightCorner.Y - 2);
							// outer right
							g.DrawLine(pen2, ptCloseUpperRightCorner.X + 1, ptCloseUpperRightCorner.Y, ptCloseUpperRightCorner.X - 2, ptCloseUpperRightCorner.Y + 3);
							g.DrawLine(pen2, ptCloseLowerRightCorner.X + 1, ptCloseLowerRightCorner.Y, ptCloseLowerRightCorner.X - 2, ptCloseLowerRightCorner.Y - 3);
							// outer top
							g.DrawLine(pen2, ptCloseUpperRightCorner.X - 1, ptCloseUpperRightCorner.Y, ptCloseUpperRightCorner.X - 3, ptCloseUpperRightCorner.Y + 2);
							g.DrawLine(pen2, ptCloseUpperLeftCorner.X + 1, ptCloseUpperLeftCorner.Y, ptCloseUpperLeftCorner.X + 3, ptCloseUpperLeftCorner.Y + 2);
						}
					}
					#endregion

					#region Tab Bottom Strip
					color = Theming.Theme.CurrentTheme.ColorTable.DockingWindowInactiveTabBackgroundGradientEnd;
					if (dw.ParentDock.Focused)
					{
						color = Theming.Theme.CurrentTheme.ColorTable.DockingWindowActiveTabBackgroundGradientEnd;
					}
					g.FillRectangle(new System.Drawing.SolidBrush(color), new System.Drawing.Rectangle(rectTab.Left, rectTab.Bottom, base.Width - (rightSize > -1 ? rightSize : 0), 4));
					#endregion

					UpdateTabBounds(dw, rectTab);
					x += (w + 1);
				}
				#endregion
				#region Top Docking
				else if (dw.ParentDock.DockPosition == DockPosition.Top)
				{
					w += (sz.Width + 2);
					if (dw.Image != null)
					{
						w += 18;
					}

					int leftSize = -1;
					int topSize = -1;
					int rightSize = -1;
					int bottomSize = -1;
					CalculateSizes(dw, out leftSize, out topSize, out rightSize, out bottomSize);

					System.Drawing.Rectangle rectTab = new System.Drawing.Rectangle(x, y, w, h);

					Theming.Theme.CurrentTheme.DrawDockPanelTabBackground(g, rectTab, dw.TabState, DockPosition.Left, dw.Selected, dw.ParentDock.Focused);
					System.Drawing.Rectangle rectText = new System.Drawing.Rectangle(rectTab.Left + 5, rectTab.Top, sz.Height, sz.Width);
					g.DrawText(dw.TabTitle, Font, rectText, System.Drawing.Color.White, System.Drawing.RotateFlipType.RotateNoneFlipNone);

					UpdateTabBounds(dw, rectTab);

					y += (w + 1);
				}
				#endregion
			}
			*/
			#endregion
		}

		private void CalculateSizes(DockingWindow dw, out int leftSize, out int topSize, out int rightSize, out int bottomSize)
		{
			leftSize = -1;
			topSize = -1;
			rightSize = -1;
			bottomSize = -1;

			/*
			if (dw.ParentDock.Parent == null) return;
			foreach (IDockable idck in dw.ParentDock.Parent.Windows)
			{
				if (idck is DockingWindowContainer)
				{
					DockingWindowContainer dwc = (idck as DockingWindowContainer);
					switch (dwc.DockPosition)
					{
						case DockPosition.Left:
						{
							leftSize = 18;
							int notVisible = 0;
							foreach (IDockable idck1 in dwc.Windows)
							{
								if (idck1 is DockingWindow)
								{
									DockingWindow dww = (idck1 as DockingWindow);
									if (dww.Behavior == DockBehavior.Dock)
									{
										leftSize = dwc.Size;
										break;
									}
									else if (!dww.Visible)
									{
										notVisible++;
									}
								}
							}

							if (notVisible == dwc.Windows.Count)
							{
								leftSize = 7;
							}
							break;
						}
					}
				}
			}
			*/
		}
		private System.Windows.Forms.Padding GetTabbedDocumentMargins()
		{
			// will always have a top margin of at least 31px
			int left = 7, top = 31, right = 7, bottom = 7;

			return new System.Windows.Forms.Padding(left, top, right, bottom);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);
			
			DockingWindow dw = HitTest(e.Location);
			if (dw != null)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Middle)
				{
					// close tab
					CloseWindow(dw);
					return;
				}

				System.Drawing.Rectangle rect = TabBounds[dw];
				if (!dw.ParentArea.IsDocked)
				{
					if (e.X >= (rect.Right - 24) && e.X <= (rect.Right - 4))
					{
						CloseWindow(dw);
						return;
					}
				}
				SwitchTab(dw);
			}

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				// pop up the context menu
				ShowContextMenu(dw, e.Location);
			}
			Invalidate();
		}

		private void ShowContextMenu(DockingWindow dw, Point location)
		{
			if (dw != null)
			{
				/*
				Pages.EditorPage editor = (dw.Control as Pages.EditorPage);
				if (editor != null)
				{
					mnuContextTabSave.Visible = true;
					mnuContextTabSave.Text = "&Save " + System.IO.Path.GetFileName(editor.FileName);
				}
				else
				{
					mnuContextTabSave.Visible = false;
				}
				*/
				mnuContextTabSave.Visible = true;
				mnuContextTabClose.Visible = true;
				mnuContextTabCloseAllButThis.Visible = true;
				mnuContextTabCloseAllDocuments.Visible = (dw.ParentArea.Windows.Count > 0);
				mnuContextTabSep1.Visible = true;

				mnuContextTabCopyFullPath.Visible = true;
				mnuContextTabOpenContainingFolder.Visible = true;
				mnuContextTabSep2.Visible = true;

				mnuContextTabFloat.Visible = (dw.Behavior != DockBehavior.Float);
				mnuContextTabFloatAll.Visible = (dw.ParentArea.Windows.Count > 1);
				mnuContextTabDockAsTabbedDocument.Visible = (dw.Behavior != DockBehavior.Dock);
				mnuContextTabSep3.Visible = (mnuContextTabFloat.Visible || mnuContextTabFloatAll.Visible || mnuContextTabDockAsTabbedDocument.Visible);

				mnuContextTabPinTab.Visible = true;
				mnuContextTabSep4.Visible = true;

				mnuContextTabNewHorizontalTabGroup.Visible = (dw.ParentArea.Windows.Count > 1);
				mnuContextTabNewVerticalTabGroup.Visible = (dw.ParentArea.Windows.Count > 1);
				mnuContextTabSep5.Visible = (mnuContextTabNewHorizontalTabGroup.Visible || mnuContextTabNewVerticalTabGroup.Visible);
			}
			else
			{
				mnuContextTabSave.Visible = false;
				mnuContextTabClose.Visible = false;
				mnuContextTabCloseAllButThis.Visible = false;
				mnuContextTabCloseAllDocuments.Visible = (mvarAreas[DockPosition.Center].Areas[DockPosition.Center].Windows.Count > 0);
				mnuContextTabSep1.Visible = mnuContextTabCloseAllDocuments.Visible;

				mnuContextTabCopyFullPath.Visible = false;
				mnuContextTabOpenContainingFolder.Visible = false;
				mnuContextTabSep2.Visible = false;

				mnuContextTabFloat.Visible = false;
				mnuContextTabFloatAll.Visible = false;
				mnuContextTabDockAsTabbedDocument.Visible = false;
				mnuContextTabSep3.Visible = false;

				mnuContextTabPinTab.Visible = false;
				mnuContextTabSep4.Visible = false;

				mnuContextTabNewHorizontalTabGroup.Visible = false;
				mnuContextTabNewVerticalTabGroup.Visible = false;
				mnuContextTabSep5.Visible = false;
			}

			mnuContextTab.Show(this, location);
		}

		public void SwitchTab(DockingWindow dw)
		{
			// if (dw.Selected) return;
			if (dw == null) return;

			if (dw.ParentArea != null)
			{
				foreach (DockingWindow dw0 in dw.ParentArea.Windows)
				{
					if (dw0 != dw)
					{
						dw0.Selected = false;
						dw0.Control.Visible = false;
						dw0.Control.Enabled = false;
					}
				}
			}
			
			UpdateControlMetrics();
			if (dw.Behavior == DockBehavior.Dock)
			{
				if (!this.Controls.Contains(dw.Control))
				{
					this.Controls.Add(dw.Control);
				}

				dw.Control.Dock = System.Windows.Forms.DockStyle.None;
				dw.Control.Enabled = true;
				dw.Control.Visible = true;
			}

			dw.Selected = true;
			mvarSelectedWindow = dw;

			OnSelectedWindowChanged(EventArgs.Empty);
			Invalidate();
			dw.Control.Focus();
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			// Switch to the selected window when we're visible
			if (Visible) SwitchTab(mvarSelectedWindow);
		}

		public event EventHandler SelectedWindowChanged;
		protected virtual void OnSelectedWindowChanged(EventArgs e)
		{
			if (SelectedWindowChanged != null) SelectedWindowChanged(this, e);
		}

		public event WindowClosingEventHandler WindowClosing;
		protected virtual void OnWindowClosing(WindowClosingEventArgs e)
		{
			if (WindowClosing != null) WindowClosing(this, e);
		}
		public event WindowClosedEventHandler WindowClosed;
		protected virtual void OnWindowClosed(WindowClosedEventArgs e)
		{
			if (WindowClosed != null) WindowClosed(this, e);
		}

		internal void UpdateControlMetrics()
		{
			Rectangle rect = new Rectangle(0, 0, Width, Height);
			UpdateControlMetrics(mvarAreas[DockPosition.Bottom], rect);
			UpdateControlMetrics(mvarAreas[DockPosition.Center], rect);
			UpdateControlMetrics(mvarAreas[DockPosition.Left], rect);
			UpdateControlMetrics(mvarAreas[DockPosition.Right], rect);
			UpdateControlMetrics(mvarAreas[DockPosition.Top], rect);
		}
		internal void UpdateControlMetrics(DockingArea area, Rectangle rect)
		{
			if (area.Areas.Count == 0 && area.Windows.Count == 0) return;
			// if (area.Areas.Count != 0 && area.Windows.Count != 0) throw new InvalidOperationException("Area cannot contain both areas and windows");

			DockingArea.DockingAreaCollection parentAreas = mvarAreas;
			if (area.ParentArea != null) parentAreas = area.ParentArea.Areas;

			Rectangle rect2 = rect;
			switch (area.Position)
			{
				case DockPosition.Bottom:
				{
					// Bottom docking panel snaps to left, bottom, right areas
					rect2 = new Rectangle(rect.X, rect.Bottom - area.Size, Width, area.Size - Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize);
					if (area.Windows.Count > 0)
					{
						// Move the rectangle up to make room for the tabs, which are not visible when there is
						// only one window in the container
						// rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}

					if (area.ParentArea != null)
					{
						if (area.ParentArea.Position == DockPosition.Center)
						{
							// Get rid of space on the top, left, bottom, and right sides of the window
							if (mvarAreas[DockPosition.Left].Areas.Count > 0)
							{
								int size = 0;
								if (mvarAreas[DockPosition.Left].IsDocked)
								{
									size = mvarAreas[DockPosition.Left].Size;
								}
								else
								{
									size = Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
								}
								// rect2.X += size;
								rect2.Width -= size;
							}
							if (mvarAreas[DockPosition.Right].Areas.Count > 0)
							{
								int size = 0;
								if (mvarAreas[DockPosition.Right].IsDocked)
								{
									size = mvarAreas[DockPosition.Right].Size;
								}
								else
								{
									size = Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
								}
								rect2.Width -= size;
							}
						}
					}

					// Add space for the gripper
					rect2.Y += Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					break;
				}
				case DockPosition.Center:
				{
					// Center docking container always has its tabs showing
					rect2 = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
					if (area.Windows.Count > 0)
					{
						rect2.Y += (Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize + 2);
						rect2.Height -= (Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize + 2);
					}

					if (parentAreas[DockPosition.Left].IsDocked)
					{
						rect2.X += parentAreas[DockPosition.Left].Size;
						rect2.Width -= parentAreas[DockPosition.Left].Size;
					}
					else if (parentAreas[DockPosition.Left].Windows.Count > 0)
					{
						rect2.X += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
						rect2.Width -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}

					if (parentAreas[DockPosition.Right].IsDocked)
					{
						rect2.Width -= parentAreas[DockPosition.Right].Size;
					}
					if (parentAreas[DockPosition.Top].IsDocked)
					{
						rect2.Y += (parentAreas[DockPosition.Top].Size + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize);
						rect2.Height -= (parentAreas[DockPosition.Top].Size + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize);
					}
					else if (parentAreas[DockPosition.Top].Windows.Count > 0)
					{
						rect2.Y += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
						rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}

					if (parentAreas[DockPosition.Bottom].IsDocked)
					{
						rect2.Height -= (parentAreas[DockPosition.Bottom].Size + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize);
					}
					else if (parentAreas[DockPosition.Bottom].Windows.Count > 0)
					{
						rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}
					break;
				}
				case DockPosition.Left:
				{
					// Left docking panel snaps to left, top, bottom areas
					rect2 = new Rectangle(rect.X, rect.Y + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize, area.Size, rect.Height - Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize);
					if (area.Windows.Count > 1)
					{
						// Move the rectangle up to make room for the tabs, which are not visible when there is
						// only one window in the container
						rect2.Y -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}

					// Add space for the gripper
					rect2.Width -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					break;
				}
				case DockPosition.Right:
				{
					// Right docking panel snaps to right, top, bottom areas
					rect2 = new Rectangle(rect.Right - area.Size, rect.Y, area.Size, rect.Height);
					if (area.IsDocked)
					{
						rect2.Y += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize;
						rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize;
					}
					if (area.Windows.Count > 1)
					{
						// Move the rectangle up to make room for the tabs, which are not visible when there is
						// only one window in the container
						rect2.Y -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
					}


					// Add space for the gripper
					rect2.X += Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					rect2.Width -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					break;
				}
				case DockPosition.Top:
				{
					// Top docking panel snaps to top, left, right areas
					rect2 = new Rectangle(rect.X, rect.Y, rect.Width, area.Size);
					if (area.IsDocked)
					{
						rect2.Y -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
						rect2.Y += Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize;
						rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize;
						rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowTitlebarSize;
					}

					// Add space for the gripper
					rect2.Height -= Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize;
					break;
				}
			}

			foreach (DockingWindow window in area.Windows)
			{
				UpdateControlMetrics(window, rect2);
			}

			UpdateControlMetrics(area.Areas[DockPosition.Bottom], rect2);
			UpdateControlMetrics(area.Areas[DockPosition.Center], rect2);
			UpdateControlMetrics(area.Areas[DockPosition.Left], rect2);
			UpdateControlMetrics(area.Areas[DockPosition.Right], rect2);
			UpdateControlMetrics(area.Areas[DockPosition.Top], rect2);
		}

		internal void UpdateControlMetrics(DockingWindow dw, Rectangle rect)
		{
			bool visible = dw.Control.Visible;
			dw.Control.Visible = false;

			dw.Control.Left = rect.X;
			dw.Control.Top = rect.Y;
			dw.Control.Width = rect.Width;
			dw.Control.Height = rect.Height;
			dw.Control.Dock = System.Windows.Forms.DockStyle.None;

			switch (dw.ParentArea.Position)
			{
				case DockPosition.Bottom:
				{
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
					break;
				}
				case DockPosition.Center:
				{
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
					break;
				}
				case DockPosition.Left:
				{
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top;
					break;
				}
				case DockPosition.Right:
				{
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
					break;
				}
				case DockPosition.Top:
				{
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
					break;
				}
			}
			
			/*
			if (dw.ParentArea == null) return;
			if (dw.ParentArea.Position == DockPosition.Left && dw.ParentArea.IsDocked)
			{
				dw.Control.Left = 0;
				dw.Control.Top = 20;
				dw.Control.Width = parentAreas[DockPosition.Left].Size;
				dw.Control.Height = Height - 20;
				dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Left;
			}
			else if (dw.ParentArea.Position == DockPosition.Bottom && dw.ParentArea.IsDocked)
			{
				dw.Control.Left = 0;
				dw.Control.Top = this.Height - parentAreas[DockPosition.Right].Size;
				dw.Control.Height = parentAreas[DockPosition.Bottom].Size;
				dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			}
			else if (dw.ParentArea.Position == DockPosition.Right && dw.ParentArea.IsDocked)
			{
				dw.Control.Left = this.Width - parentAreas[DockPosition.Right].Size;
				dw.Control.Top = 0;
				dw.Control.Width = parentAreas[DockPosition.Right].Size;
				dw.Control.Height = Height - 20;
				dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom;
			}
			else if (dw.ParentArea.Position == DockPosition.Top && dw.ParentArea.IsDocked)
			{
				dw.Control.Left = 20;
				dw.Control.Top = 20;
				dw.Control.Width = Width - 40;
				dw.Control.Height = parentAreas[DockPosition.Top].Size;
				dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			}
			else if (dw.ParentArea.Position == DockPosition.Center)
			{
				dw.Control.Left = 0;
				dw.Control.Top = 20;
				dw.Control.Width = Width;
				dw.Control.Height = Height - 20;
				dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			}

			if (dw.ParentArea.Position != DockPosition.Left)
			{
				if (parentAreas[DockPosition.Left].IsDocked)
				{
					dw.Control.Left += parentAreas[DockPosition.Left].Size;
				}
				else if (parentAreas[DockPosition.Left].Windows.Count > 0)
				{
					dw.Control.Left += 20;
				}
			}
			if (dw.ParentArea.Position != DockPosition.Top)
			{
				if (parentAreas[DockPosition.Top].IsDocked)
				{
					dw.Control.Top += parentAreas[DockPosition.Top].Size;
				}
				else if (parentAreas[DockPosition.Top].Windows.Count > 0)
				{
					dw.Control.Top += 20;
				}
			}
			if (dw.ParentArea.Position != DockPosition.Bottom)
			{
				if (parentAreas[DockPosition.Bottom].IsDocked)
				{
					dw.Control.Height -= parentAreas[DockPosition.Bottom].Size;
					dw.Control.Height -= 20;
				}
				else if (parentAreas[DockPosition.Bottom].Windows.Count > 0)
				{
					dw.Control.Height -= 20;
				}
			}
			if (dw.ParentArea.Position != DockPosition.Right)
			{
				if (parentAreas[DockPosition.Right].IsDocked)
				{
					// Leave room for the title bar
					dw.Control.Top += 20;
					dw.Control.Height -= 20;
					dw.Control.Width -= parentAreas[DockPosition.Bottom].Size;
				}
				else if (parentAreas[DockPosition.Right].Windows.Count > 0)
				{
					dw.Control.Width -= 20;
				}
			}
			*/

			dw.Control.Visible = visible;
		}

		internal bool CloseWindow(DockingWindow dw)
		{
			if (mvarWindows.Contains(dw))
			{
				mvarWindows.Remove(dw);
				return true;
			}

			WindowClosingEventArgs wce = new WindowClosingEventArgs(dw);
			OnWindowClosing(wce);
			if (wce.Cancel) return false;

			dw.Control.Hide();
			Controls.Remove(dw.Control);
			TabBounds.Remove(dw);

			DockingArea parentArea = dw.ParentArea;

			if (parentArea != null)
			{
				int previndex = parentArea.Windows.IndexOf(dw);

				parentArea.Windows.Remove(dw);
				if (parentArea.Windows.Count > 0)
				{
					if (previndex >= parentArea.Windows.Count)
					{
						SwitchTab(parentArea.Windows[parentArea.Windows.Count - 1] as DockingWindow);
					}
					else
					{
						SwitchTab(parentArea.Windows[previndex] as DockingWindow);
					}
				}
			}
			Invalidate();

			if (parentArea.Windows.Count == 0) mvarSelectedWindow = null;
			OnWindowClosed(new WindowClosedEventArgs(dw));
			return true;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			foreach (DockingWindow dw0 in mvarWindows)
			{
				RecursiveSetTabState(dw0, ControlState.Normal);
			}
			Cursor = System.Windows.Forms.Cursors.Default;
			Invalidate();
		}
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			foreach (DockingWindow dw0 in mvarWindows)
			{
				RecursiveSetTabState(dw0, ControlState.Normal);
			}

			DockingWindow dw = HitTest(e.Location);
			if (dw != null)
			{
				dw.TabState = ControlState.Hover;

				if (dw.ToolTipText != null)
				{
					if (tip.Tag == null)
					{
						tip.Show(dw.ToolTipText, this, e.X, e.Y + System.Windows.Forms.Cursor.Current.Size.Height);
						tip.Tag = true;
					}
				}
				else
				{
					tip.Hide(this);
					tip.Tag = null;
				}

				if (dw.ParentArea.Position != DockPosition.Center)
				{
					if (dw.Behavior == DockBehavior.AutoHide)
					{
						if (popoutPanels.ContainsKey(dw))
						{
							if (popoutPanels[dw].Visible) return;

							foreach (System.Windows.Forms.Panel panel in popoutPanels.Values)
							{
								panel.Visible = false;
							}

							{
								PopoutPanel panel = popoutPanels[dw];
								panel.Text = dw.Title;
								panel.Icon = dw.Image;

								if (dw.ParentArea.Position == DockPosition.Left)
								{
									panel.Left = 20;
									panel.Top = 0;
									panel.Width = dw.Size;
									panel.Height = base.Height;
								}
								else if (dw.ParentArea.Position == DockPosition.Top)
								{
									panel.Left = 0;
									panel.Top = 20;
									panel.Width = base.Width;
									panel.Height = dw.Size;
								}
								else if (dw.ParentArea.Position == DockPosition.Bottom)
								{
									panel.Left = 0;
									panel.Top = base.Height - 20 - dw.Size;
									panel.Width = base.Width;
									panel.Height = dw.Size;
								}
								panel.Visible = true;
							}
						}
					}
				}
			}
			else
			{
				tip.Hide(this);
				tip.Tag = null;
				foreach (System.Windows.Forms.Panel panel in popoutPanels.Values)
				{
					panel.Visible = false;
				}

				#region Update the mouse cursor when we go to size a panel
				Cursor = System.Windows.Forms.Cursors.Default;
				foreach (DockingWindow dw0 in mvarWindows)
				{
					switch (dw0.ParentArea.Position)
					{
						case DockPosition.Top:
						{
							if (e.Y >= dw0.Control.Bounds.Bottom + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize && e.Y <= dw0.Control.Bounds.Bottom + Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize + Theming.Theme.CurrentTheme.MetricTable.DockingWindowTabSize)
							{
								Cursor = System.Windows.Forms.Cursors.SizeNS;
							}
							break;
						}
						case DockPosition.Bottom:
						{
							if (e.Y <= dw0.Control.Bounds.Top && e.Y >= dw0.Control.Bounds.Top + Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize)
							{
								Cursor = System.Windows.Forms.Cursors.SizeNS;
							}
							break;
						}
						case DockPosition.Left:
						{
							if (e.X >= dw0.Control.Bounds.Right && e.X <= dw0.Control.Bounds.Right + Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize)
							{
								Cursor = System.Windows.Forms.Cursors.SizeWE;
							}
							break;
						}
						case DockPosition.Right:
						{
							if (e.X <= dw0.Control.Bounds.Left && e.X >= dw0.Control.Bounds.Left - Theming.Theme.CurrentTheme.MetricTable.DockingWindowSplitterSize)
							{
								Cursor = System.Windows.Forms.Cursors.SizeWE;
							}
							break;
						}
					}
				}
				#endregion
			}
			Invalidate(); //new System.Drawing.Rectangle(0, 0, base.Width, 30));
		}

		private DockingWindow mvarSelectedWindow = null;
		public DockingWindow SelectedWindow
		{
			get { return mvarSelectedWindow; }
			set { SwitchTab(value); }
		}

		private void RecursiveSetTabState(DockingWindow dw, ControlState controlState)
		{
			dw.TabState = controlState;
		}
		private void RecursiveSetSelected(DockingWindow dw, bool selected)
		{
			if (dw == null) return;

			dw.Selected = selected;

			if (dw.ParentArea != null)
			{
				if (dw.ParentArea.Position == DockPosition.Center)
				{
					if (!Controls.Contains(dw.Control))
					{
						Controls.Add(dw.Control);

						dw.Control.MouseEnter += new EventHandler(Control_MouseEnter);
					}

					System.Windows.Forms.Padding margins = GetTabbedDocumentMargins();
					dw.Control.Top = margins.Top; // 32
					dw.Control.Left = margins.Left; // 25
					dw.Control.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

					dw.Control.Width = base.Width - dw.Control.Left - margins.Right;
					dw.Control.Height = base.Height - dw.Control.Top - margins.Bottom;
					dw.Control.Visible = selected;
				}
			}
			if (dw.Selected) mvarSelectedWindow = dw;
		}

		void Control_MouseEnter(object sender, EventArgs e)
		{
			HideAllDockingWindows();
		}

		public void HideAllDockingWindows()
		{
			// If the left mouse button is pressed, don't hide the window - someone may be dragging...
			if ((System.Windows.Forms.Control.MouseButtons & System.Windows.Forms.MouseButtons.Left) == System.Windows.Forms.MouseButtons.Left) return;

			foreach (PopoutPanel panel in popoutPanels.Values)
			{
				panel.Visible = false;
			}
		}


		private void RecursiveSetFocused(DockingWindow dw, bool focused)
		{
			dw.Focused = focused;
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			UpdateControlMetrics();
			Invalidate();
		}

		private System.Windows.Forms.ToolTip tip;

		private Dictionary<DockingWindow, System.Drawing.Rectangle> TabBounds = new Dictionary<DockingWindow, System.Drawing.Rectangle>();

		private DockingWindow HitTest(System.Drawing.Point point)
		{
			foreach (DockingWindow dw in popoutPanels.Keys)
			{
				if (TabBounds.ContainsKey(dw))
				{
					if (point.X >= TabBounds[dw].Left && point.X <= TabBounds[dw].Right)
					{
						if (point.Y >= TabBounds[dw].Top && point.Y <= TabBounds[dw].Bottom)
						{
							return dw;
						}
					}
				}
			}
			return null;
		}

		private void UpdateTabBounds(DockingWindow dw, System.Drawing.Rectangle bounds)
		{
			if (TabBounds.ContainsKey(dw))
			{
				TabBounds[dw] = bounds;
			}
			else
			{
				TabBounds.Add(dw, bounds);
			}
		}

		private System.Collections.Generic.Dictionary<DockingWindow, PopoutPanel> popoutPanels = new Dictionary<DockingWindow, PopoutPanel>();

		/// <summary>
		/// Updates the dockable items contained within.
		/// </summary>
		internal void UpdateDockableItems()
		{
			// UpdateDockableItems does not currently remove any DockingWindow PopoutPanels
			// from the dictionary when a Window gets removed from the collection. Fix?

			foreach (DockingWindow dw in mvarWindows)
			{
				LoadPopoutPanel(dw);
			}
			UpdateControlMetrics();

			Invalidate();
		}

		private void LoadPopoutPanel(DockingWindow dw)
		{
			if (!popoutPanels.ContainsKey(dw))
			{
				PopoutPanel popoutPanel = new PopoutPanel();

				popoutPanel.Visible = false;
				popoutPanel.ChildControl = dw.Control;

				popoutPanels.Add(dw, popoutPanel);

				Controls.Add(popoutPanel);
			}
			if (dw.Behavior == DockBehavior.Dock)
			{
				if (!Controls.Contains(dw.Control))
				{
					dw.Control.Visible = false;
					Controls.Add(dw.Control);
					dw.Control.MouseEnter += new EventHandler(Control_MouseEnter);
				}
			}
		}

		private Dialogs.WindowListPopupDialog dlgWindowListPopup = null;
		public void ShowWindowListPopupDialog()
		{
			if (dlgWindowListPopup == null) dlgWindowListPopup = new Dialogs.WindowListPopupDialog(this);
			if (dlgWindowListPopup.IsDisposed) dlgWindowListPopup = new Dialogs.WindowListPopupDialog(this);
			if (dlgWindowListPopup.Visible)
			{
				dlgWindowListPopup.Hide();
			}
			dlgWindowListPopup.SelectedWindow = SelectedWindow;
			dlgWindowListPopup.CycleWindows((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Shift) == System.Windows.Forms.Keys.Shift);

			// dlgWindowListPopup.Documents.Clear();

			/*
			foreach (Document d in mvarDocuments)
			{
				dlgWindowList.Documents.Add(d);
			}
			int index = mvarDocuments.IndexOf(mvarCurrentDocument);
			if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)
			{
				index--;
				if (index < 0) index = mvarDocuments.Count - 1;
			}
			else
			{
				index++;
				if (index > mvarDocuments.Count - 1) index = 0;
			}
			dlgWindowListPopup.SelectedDocument = mvarDocuments[index];
			*/
			dlgWindowListPopup.ShowDialog();

			SwitchTab(dlgWindowListPopup.SelectedWindow);
		}
		public void HideWindowListPopupDialog()
		{
			dlgWindowListPopup.Close();
		}

		private Dialogs.WindowListDialog dlgWindowList = null;
		public void DisplayWindowListDialog()
		{
			if (dlgWindowList == null) dlgWindowList = new Dialogs.WindowListDialog(this);
			if (dlgWindowList.IsDisposed) dlgWindowList = new Dialogs.WindowListDialog(this);
			if (dlgWindowList.Visible)
			{
				dlgWindowList.Hide();
			}
			dlgWindowList.SelectedWindow = SelectedWindow;
			if (dlgWindowList.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{

			}
		}

		private bool mvarIsWindowListPopupDialogVisible = false;
		public bool IsWindowListPopupDialogVisible { get { return (dlgWindowListPopup != null && dlgWindowListPopup.Visible); } }

		public void CycleWindowListPopupDialog(bool reverse)
		{
			if (dlgWindowListPopup != null && !dlgWindowListPopup.IsDisposed)
			{
				dlgWindowListPopup.CycleWindows(reverse);
			}
		}

		#region Context Menu
		private void mnuContextTabSave_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabClose_Click(object sender, EventArgs e)
		{
			if (mvarSelectedWindow != null)
			{
				CloseWindow(mvarSelectedWindow);
			}
		}

		private void mnuContextTabCloseAllDocuments_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < mvarWindows.Count; i++)
			{
				DockingWindow window = mvarWindows[i];
				if (window.ParentArea != null && window.ParentArea.Position == DockPosition.Center)
				{
					CloseWindow(window);
				}
			}
		}

		private void mnuContextTabCloseAllButThis_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabCopyFullPath_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabOpenContainingFolder_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabFloat_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabFloatAll_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabDockAsTabbedDocument_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabPinTab_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabNewHorizontalTabGroup_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabNewVerticalTabGroup_Click(object sender, EventArgs e)
		{

		}

		private void mnuContextTabCustomize_Click(object sender, EventArgs e)
		{

		}
		#endregion
	}
}