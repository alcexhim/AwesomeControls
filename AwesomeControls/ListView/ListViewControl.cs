using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AwesomeControls.CommandBars;

namespace AwesomeControls.ListView
{
	// TODO:
	//		* Fix theming on column headers and grid lines
	//      * Scroll bars!!!!!
	//      * Wheel scrolling works, but doesn't stop when you reach the end of the list
	//      * Keyboard navigation (Up, Down, Left, Right, Home, End, with Ctrl/Shift support for multiselectable listviews)
	//      * Tree node items
	//      * Fix the arrow drawing for the sort indicator on column headers
	//      * Column repositioning
	//      * Column filtering (text-based)
	//      * Column filtering (menu-based, see Windows Explorer)
	//		* Column selection via menu (see Mozilla)

	/// <summary>
	/// A more powerful ListView control. Originally designed to quickly display a range of items from an extremely
	/// large population, this ListView has enhanced support for tree display as well as custom column editors using
	/// checkboxes, dropdown lists, textboxes, and even your own controls and/or dialog boxes.
	/// </summary>
	[DefaultEvent("SelectionChanged")]
	public partial class ListViewControl : UserControl
	{
		public event EventHandler ItemActivate;
		protected virtual void OnItemActivate(EventArgs e)
		{
			if (ItemActivate != null) ItemActivate(this, e);
		}

		public event ListViewItemDragEventHandler ItemDrag;
		public event ListViewItemDragEventHandler ItemDragComplete;

		public ListViewControl()
		{
			mvarItems = new ListViewItem.ListViewItemCollection(this);

			InitializeComponent();

			ResetBackColor();
			ResetForeColor();

			base.DoubleBuffered = true;
		}

		#region Custom mergable ContextMenuStrip
		private ContextMenuStrip mvarContextMenuStrip = null;
		public new ContextMenuStrip ContextMenuStrip { get { return mvarContextMenuStrip; } set { mvarContextMenuStrip = value; } }

		private bool mvarMergeContextMenuStrip = false;
		/// <summary>
		/// If true, this <see cref="ListViewControl" />'s associated <see cref="System.Windows.Forms.ContextMenuStrip" />
		/// control will be merged into the default <see cref="System.Windows.Forms.ContextMenuStrip" /> rather than remain
		/// standalone.
		/// </summary>
		[Description("If true, this ListViewControl's associated ContextMenuStrip control will be merged into the default ContextMenuStrip rather than remain standalone."), DefaultValue(false)]
		public bool MergeContextMenuStrip
		{
			get { return mvarMergeContextMenuStrip; }
			set { mvarMergeContextMenuStrip = value; }
		}

		private List<ToolStripItem> defaultContextMenuItems = null;
		private void DisplayContextMenuStrip(Point location)
		{
			if (defaultContextMenuItems == null)
			{
				defaultContextMenuItems = new List<ToolStripItem>();
				foreach (ToolStripItem tsi in mnuContext.Items)
				{
					defaultContextMenuItems.Add(tsi);
				}
			}

			CBContextMenu actualContextMenuStrip = new CBContextMenu();
			if (mvarContextMenuStrip == null || mvarMergeContextMenuStrip)
			{
				foreach (ToolStripItem tsi in defaultContextMenuItems)
				{
					actualContextMenuStrip.Items.Add(tsi);
				}
			}
			if (mvarContextMenuStrip != null)
			{
				if (mvarMergeContextMenuStrip) actualContextMenuStrip.Items.Add(new ToolStripSeparator());
				foreach (ToolStripItem tsi in mvarContextMenuStrip.Items)
				{
					actualContextMenuStrip.Items.Add(tsi.Clone());
				}
			}
			actualContextMenuStrip.Show(this, location);
		}
		#endregion

		private bool mvarAllowColumnResize = true;
		/// <summary>
		/// Indicates whether the user can resize columns in Details view.
		/// </summary>
		[Description("Indicates whether the user can resize columns in Details view."), DefaultValue(true)]
		public bool AllowColumnResize { get { return mvarAllowColumnResize; } set { mvarAllowColumnResize = value; } }

		private bool mvarAllowDrag = false;
		/// <summary>
		/// Indicates whether items can be dragged outside this control's boundaries.
		/// </summary>
		[Description("Indicates whether items can be dragged outside this control's boundaries."), DefaultValue(false)]
		public bool AllowDrag { get { return mvarAllowDrag; } set { mvarAllowDrag = value; } }

		private bool mvarAllowColumnReorder = true;
		/// <summary>
		/// Indicates whether the user can reorder columns in Details view.
		/// </summary>
		[Description("Indicates whether the user can reorder columns in Details view."), DefaultValue(true)]
		public bool AllowColumnReorder { get { return mvarAllowColumnReorder; } set { mvarAllowColumnReorder = value; } }

		private bool mvarEnableAutomaticInlineRenaming = true;
		/// <summary>
		/// Determines if the F2 key is automatically handled for inline renaming.
		/// </summary>
		public bool EnableAutomaticInlineRenaming { get { return mvarEnableAutomaticInlineRenaming; } set { mvarEnableAutomaticInlineRenaming = value; } }

		public override void ResetBackColor()
		{
			BackColor = Color.FromKnownColor(KnownColor.Window);
		}
		public override void ResetForeColor()
		{
			ForeColor = Color.FromKnownColor(KnownColor.WindowText);
		}

		private ListViewItem.ListViewItemCollection mvarItems = null;
		public ListViewItem.ListViewItemCollection Items { get { return mvarItems; } }

		public ListViewItem.ListViewItemCollection SelectedItems
		{
			get
			{
				ListViewItem.ListViewItemCollection lvic = new ListViewItem.ListViewItemCollection(this);
				foreach (ListViewItem lvi in mvarItems)
				{
					if (lvi.Selected)
					{
						lvic.Add(lvi);
					}
				}
				return lvic;
			}
		}

		private ImageList mvarLargeImageList = null;
		public ImageList LargeImageList { get { return mvarLargeImageList; } set { mvarLargeImageList = value; } }
		private ImageList mvarSmallImageList = null;
		public ImageList SmallImageList { get { return mvarSmallImageList; } set { mvarSmallImageList = value; } }

		private bool mvarShowGridLines = true;
		/// <summary>
		/// Determines if grid lines are drawn on the control in Details view.
		/// </summary>
		[Description("Determines if grid lines are drawn on the control in Details view."), DefaultValue(true), Category("Appearance")]
		public bool ShowGridLines { get { return mvarShowGridLines; } set { mvarShowGridLines = value; } }

		private int mvarColumnHeaderHeight = 24;
		/// <summary>
		/// Determines the height of the column headers.
		/// </summary>
		[Description("Determines the height of the column headers."), DefaultValue(24), Category("Appearance")]
		public int ColumnHeaderHeight { get { return mvarColumnHeaderHeight; } set { mvarColumnHeaderHeight = value; } }

		private int mvarRowHeaderWidth = 24;
		/// <summary>
		/// Determines the width of the row headers.
		/// </summary>
		[Description("Determines the width of the row headers."), DefaultValue(24), Category("Appearance")]
		public int RowHeaderWidth { get { return mvarRowHeaderWidth; } set { mvarRowHeaderWidth = value; } }

		private bool mvarHideSelection = true;
		/// <summary>
		/// Removes highlighting from the selected item when the control does not have focus.
		/// </summary>
		[Description("Removes highlighting from the selected item when the control does not have focus."), DefaultValue(true), Category("Appearance")]
		public bool HideSelection { get { return mvarHideSelection; } set { mvarHideSelection = value; } }

		private bool mvarFullRowSelect = false;
		/// <summary>
		/// Indicates whether all Details are highlighted along with the <see cref="ListViewItem" /> when selected.
		/// </summary>
		[Description("Indicates whether all Details are highlighted along with the ListViewItem when selected."), DefaultValue(false), Category("Behavior")]
		public bool FullRowSelect { get { return mvarFullRowSelect; } set { mvarFullRowSelect = value; } }

		private bool mvarHotTracking = false;
		/// <summary>
		/// Allows items to appear as hyperlinks when the mouse hovers over them.
		/// </summary>
		[Description("Allows items to appear as hyperlinks when the mouse hovers over them."), DefaultValue(false), Category("Appearance")]
		public bool HotTracking { get { return mvarHotTracking; } set { mvarHotTracking = value; } }

		private bool mvarHoverSelection = false;
		/// <summary>
		/// Allows items to be selected by hovering over them with the mouse.
		/// </summary>
		[Description("Allows items to be selected by hovering over them with the mouse."), DefaultValue(false), Category("Behavior")]
		public bool HoverSelection { get { return mvarHoverSelection; } set { mvarHoverSelection = value; } }

		private ListViewItem mvarHoverItem = null;

		private ListViewColumn.ListViewColumnCollection mvarColumns = new ListViewColumn.ListViewColumnCollection();
		public ListViewColumn.ListViewColumnCollection Columns { get { return mvarColumns; } }

		public bool ShouldShowColumns()
		{
			return (mvarColumnBehavior == ListViewColumnBehavior.Always || (mvarColumnBehavior == ListViewColumnBehavior.DetailOnly && mvarMode == ListViewMode.Details));
		}

		#region Sorting
		private bool mvarAllowSorting = true;
		/// <summary>
		/// When true, automatically sorts columns by their respective specified comparer (or the default comparer
		/// if unspecified) when the user clicks on the column header.
		/// </summary>
		public bool AllowSorting { get { return mvarAllowSorting; } set { mvarAllowSorting = value; } }

		private ListViewColumn mvarSortColumn = null;
		public ListViewColumn SortColumn { get { return mvarSortColumn; } set { mvarSortColumn = value; } }
		#endregion

		private bool mvarMultiSelect = false;
		/// <summary>
		/// Determines whether multiple items in the list can be selected at once.  This automatically enables
		/// Ctrl+ and Shift+clicking functionality, as well as click-and-drag selection.
		/// </summary>
		[Description("Determines whether multiple items in the list can be selected at once.  This automatically enables Ctrl+ and Shift+clicking functionality, as well as click-and-drag selection."), DefaultValue(false), Category("Behavior")]
		public bool MultiSelect { get { return mvarMultiSelect; } set { mvarMultiSelect = value; } }

		private bool mvarEnableFilter = false;
		/// <summary>
		/// When true, places a text box below each column by which to filter the items in the <see cref="ListViewControl" />.
		/// </summary>
		[Description("When true, places a text box below each column by which to filter the items in the ListViewControl."), DefaultValue(false), Category("Behavior")]
		public bool EnableFilter { get { return mvarEnableFilter; } set { mvarEnableFilter = value; } }

		private ListViewColumnBehavior mvarColumnBehavior = ListViewColumnBehavior.DetailOnly;
		/// <summary>
		/// Determines whether the <see cref="ListViewControl" /> will never display columns, only display columns in Detail view, or always display
		/// columns.
		/// </summary>
		[Description("Determines whether the ListViewControl will never display columns, only display columns in Detail view, or always display columns."), DefaultValue(ListViewColumnBehavior.DetailOnly), Category("Behavior")]
		public ListViewColumnBehavior ColumnBehavior { get { return mvarColumnBehavior; } set { mvarColumnBehavior = value; } }

		#region Rectangle Select
		private bool m_RectangleSelect = false;
		private Point m_RectangleSelectPosStart = Point.Empty;
		private Point m_RectangleSelectPosEnd = Point.Empty;
		#endregion

		#region Pressed Column
		private ListViewColumn m_PressedColumn = null;
		private int m_PressedColumnOriginX = 0;
		private int m_PressedColumnOriginalX = 0;
		private int m_PressedColumnX = 0;
		#endregion

		#region Focus Changes
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			Refresh();
		}
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			EndLabelEdit(true);
			Refresh();
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);

			EndLabelEdit(true);
		}
		#endregion

		public event ListViewItemSelectionChangingEventHandler SelectionChanging;
		protected virtual void OnSelectionChanging(ListViewItemSelectionChangingEventArgs e)
		{
			if (SelectionChanging != null) SelectionChanging(this, e);
		}
		public event EventHandler SelectionChanged;
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (SelectionChanged != null) SelectionChanged(this, e);
		}

		private void RecursiveSetSelected(ListViewItem lvi, bool selected)
		{
			lvi.Selected = selected;
			foreach (ListViewItem lvi1 in lvi.Items)
			{
				RecursiveSetSelected(lvi1, selected);
			}
		}

		private int mvarDefaultItemHeight = 24;
		public int DefaultItemHeight { get { return mvarDefaultItemHeight; } set { mvarDefaultItemHeight = value; } }

		private bool m_DraggingItem = false;

		private ListViewColumn m_ResizeColumn = null;
		private ListViewColumn m_HoverColumn = null;
		private ListViewColumn m_DraggingColumn = null;
		private int m_ResizeColumnOldWidth = 0;
		private int m_ResizeColumnOriginX = 0;


		#region Events
		#region Mouse
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			m_HoverColumn = null;
			mvarHoverItem = null;
			Refresh();
		}
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				ListViewItem lvi = HitTest(e.Location);
				if ((mvarMode == ListViewMode.Details || mvarMode == ListViewMode.List) &&
					(lvi != null && lvi.Items.Count > 0 && !(e.X >= 8 && e.X <= 8 + 8)))
				{
					lvi.Expanded = !lvi.Expanded;
					ResetBounds();
					Refresh();
				}
				else if (mvarMode == ListViewMode.Details)
				{
					ListViewColumn lvc = m_ResizeColumn;
					if (lvc != null)
					{
						AutoResizeColumn(lvc);
					}
				}
				
				if (lvi != null)
				{
					OnItemActivate(EventArgs.Empty);
				}
			}
		}

		private Timer _labelEditTimer = null;

		private bool wasControlKeyPressed = false;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if ((Control.ModifierKeys & Keys.Control) == Keys.Control) wasControlKeyPressed = true;
			ListViewItem lvi = HitTest(e.Location);
			
			bool selectionChanged = true;

			if (lvi != null)
			{
				if (lvi.Items.Count > 0 && e.X >= 8 && e.X <= 8 + 8)
				{
					lvi.Expanded = !lvi.Expanded;
					ResetBounds();
					Refresh();
				}
				else
				{
					OnSelectionChanging(new ListViewItemSelectionChangingEventArgs(lvi));
					if (!mvarMultiSelect || ((((((Control.ModifierKeys & Keys.Control) != Keys.Control) && SelectedItems.Count <= 1)
						|| (mvarMultiSelect && ((Control.ModifierKeys & Keys.Control) != Keys.Control) && !SelectedItems.Contains(lvi)))
						&& ((mvarMultiSelect && ((Control.ModifierKeys & Keys.Shift) != Keys.Shift) && !SelectedItems.Contains(lvi))))))
					{
						foreach (ListViewItem lvi1 in mvarItems)
						{
							RecursiveSetSelected(lvi1, false);
						}
					}
					if (mvarMultiSelect && ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) && SelectedItems.Count > 0)
					{
						int index1 = mvarItems.IndexOf(SelectedItems[SelectedItems.Count - 1]);
						int index2 = mvarItems.IndexOf(lvi);

						int start = Math.Min(index1, index2);
						int end = Math.Max(index1, index2);
						for (int i = start; i <= end; i++)
						{
							mvarItems[i].Selected = true;
						}
					}

					if (lvi.Selected && ((Control.ModifierKeys & Keys.Control) == Keys.Control) && ((Control.ModifierKeys & Keys.Shift) != Keys.Shift))
					{
						lvi.Selected = false;
					}
					else
					{
						if (lvi.Selected)
						{
							// start the timeout
							if (mvarEnableAutomaticInlineRenaming)
							{
								_labelEditTimer = TimerMethods.SetTimeout(1000, delegate(object[] paramz)
								{
									BeginLabelEdit(paramz[0] as ListViewItem);
								}, lvi);
							}
							selectionChanged = false;
						}
						else
						{
							lvi.Selected = true;
						}
					}
					Refresh();

					if (selectionChanged)
					{
						OnSelectionChanged(EventArgs.Empty);
					}

					if (mvarAllowDrag) m_DraggingItem = true;
				}
			}
			else
			{
				if (SelectedItems.Count == 0) selectionChanged = false;

				if (ShouldShowColumns() && e.Y < mvarColumnHeaderHeight)
				{
					int x = 0;
					if (e.Button == System.Windows.Forms.MouseButtons.Left)
					{
						foreach (ListViewColumn lvc in mvarColumns)
						{
							if ((e.X >= (x + lvc.Width - 4) && e.X <= (x + lvc.Width + 2)) && mvarAllowColumnResize)
							{
								m_ResizeColumn = lvc;
								m_ResizeColumnOldWidth = m_ResizeColumn.Width;
								m_ResizeColumnOriginX = e.X;
								break;
							}
							else if ((e.X >= x && e.X <= x + lvc.Width) && (mvarAllowSorting || mvarAllowColumnReorder))
							{
								m_PressedColumn = lvc;
								m_PressedColumnOriginalX = x;
								m_PressedColumnOriginX = e.X;
								m_PressedColumnX = x;
								Refresh();
								break;
							}
							x += lvc.Width;
						}
					}
				}
				else
				{
					if (!mvarMultiSelect || (((Control.ModifierKeys & Keys.Control) != Keys.Control) && SelectedItems.Count <= 1) || (mvarMultiSelect && ((Control.ModifierKeys & Keys.Control) != Keys.Control) && !SelectedItems.Contains(lvi)))
					{
						foreach (ListViewItem lvi1 in mvarItems)
						{
							RecursiveSetSelected(lvi1, false);
						}
					}
					if (mvarMultiSelect)
					{
						m_RectangleSelect = true;
						m_RectangleSelectPosStart = e.Location;
						m_RectangleSelectPosEnd = e.Location;
						Refresh();
					}
				}
				OnSelectionChanged(EventArgs.Empty);
			}

			if (selectionChanged) EndLabelEdit(true);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (m_ResizeColumn != null)
				{
					m_ResizeColumn.Width = m_ResizeColumnOldWidth + (e.X - m_ResizeColumnOriginX);
					Refresh();
				}
				else
				{
					m_DraggingColumn = m_PressedColumn;
					m_PressedColumnX = (m_PressedColumnOriginalX + (e.X - m_PressedColumnOriginX));
					Refresh();
				}
			}
			else
			{
				m_HoverColumn = ColumnHitTest(e.Location);
				Refresh();
			}

			if (ShouldShowColumns() && (e.Y < mvarColumnHeaderHeight))
			{
				m_RectangleSelectPosEnd = new Point(e.X, mvarColumnHeaderHeight);
				Refresh();

				#region VSplit cursor
				if (m_DraggingColumn == null)
				{
					int x = 0;
					bool found = false;
					foreach (ListViewColumn col in mvarColumns)
					{
						if (e.X >= (x + col.Width - 4) && e.X <= (x + col.Width + 2))
						{
							Cursor = Cursors.VSplit;
							found = true;
							break;
						}
						x += col.Width;
					}
					if (!found)
					{
						Cursor = Cursors.Default;
					}
				}
				#endregion
			}
			else
			{
				if (m_ResizeColumn == null) Cursor = Cursors.Default;
				if (m_RectangleSelect && e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					m_RectangleSelectPosEnd = e.Location;
					Refresh();
				}
			}

			ListViewItem lvi1 = HitTest(e.Location);
			ListViewItem lvi2 = mvarHoverItem;
			mvarHoverItem = lvi1;

			if (m_RectangleSelect)
			{
				Rectangle rect = GetSelectionRectangle();

				int width = (int)((double)this.Width / (double)GetItemBounds().Width);
				int start = -ScrollOffsetY * width;
				
				ListViewItem[] lvis = itemBounds.GetIntersectedItems(rect, wasControlKeyPressed, start);
				foreach (ListViewItem lvi in mvarItems)
				{
					if (Array.IndexOf<ListViewItem>(lvis, lvi) > -1)
					{
						lvi.Selected = true;
					}
					else
					{
						lvi.Selected = false;
					}
				}
			}
			else
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left && lvi1 != null)
				{
					if (mvarAllowDrag && m_DraggingItem)
					{
						ListViewItemDragEventArgs ea = new ListViewItemDragEventArgs();
						OnItemDrag(ea);
						if (ea.DataObject != null)
						{
							DoDragDrop(ea.DataObject, ea.Effects);
							OnItemDragComplete(ea);
						}
					}
				}
			}

			if (lvi2 == mvarHoverItem) return;
			if (!mvarItems.Contains(lvi2)) lvi2 = null;

			if (lvi2 != null)
			{
				Invalidate(itemBounds.GetBounds(lvi2));
			}
			if (mvarHoverItem != null) Invalidate(itemBounds.GetBounds(mvarHoverItem));
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			m_DraggingItem = false;
			if (e.Button == System.Windows.Forms.MouseButtons.Left && m_DraggingColumn == null)
			{
				if (ShouldShowColumns() && (e.Y < mvarColumnHeaderHeight))
				{
					ListViewColumn lvc = ColumnHitTest(e.Location);
					if (lvc == null) return;

					if (mvarSortColumn == lvc)
					{
						if (mvarSortDirection == ListSortDirection.Ascending)
						{
							mvarSortDirection = ListSortDirection.Descending;
						}
						else
						{
							mvarSortDirection = ListSortDirection.Ascending;
						}
					}
					else
					{
						mvarSortColumn = lvc;
						mvarSortDirection = ListSortDirection.Ascending;
					}
					Refresh();
				}
			}
				
			m_ResizeColumn = null;
			m_PressedColumn = null;
			m_DraggingColumn = null;

			m_RectangleSelect = false;
			Refresh();

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (ShouldShowColumns() && (e.Y < mvarColumnHeaderHeight))
				{
					mnuColumnContext.Show(this, e.Location);
				}
				else
				{
					DisplayContextMenuStrip(e.Location);
				}
			}
		}

		private int GetScrollOffsetMaxY()
		{
			Size sz = GetItemBounds().Size;
			int itemsPerRow = (int)(Math.Ceiling((double)this.Width / sz.Width)) - 1;
			int itemsPerColumn = (int)(Math.Ceiling((double)this.Height / sz.Height)) - 1;
			int itemsPerPage = itemsPerRow * itemsPerColumn;
			int pages = (int)(Math.Ceiling((double)mvarItems.Count / itemsPerPage));

			return (int)((mvarItems.Count / itemsPerRow) - itemsPerColumn);
			// return (int)(Math.Ceiling((double)itemsPerRow - itemsPerPage));
			// return (itemsPerPage * pages);
		}

		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (e.Delta > 0)
			{
				// scroll up
				ScrollOffsetY += SystemInformation.MouseWheelScrollLines;
			}
			else if (e.Delta < 0)
			{
				// scroll down
				ScrollOffsetY -= SystemInformation.MouseWheelScrollLines;
			}
			if (ScrollOffsetY > 0) ScrollOffsetY = 0;

			int ScrollOffsetMaxY = GetScrollOffsetMaxY();
			if (-ScrollOffsetY > ScrollOffsetMaxY)
			{
				ScrollOffsetY = -ScrollOffsetMaxY;
			}
			ResetBounds();
			Refresh();
		}
		#endregion
		
		#region Keyboard
		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Up:
				{
					if (SelectedItems.Count == 0) break;

					Rectangle rect = GetItemBounds();
					int width = (int)((double)this.Width / (double)rect.Width);
					
					int index = mvarItems.IndexOf(SelectedItems[0]);
					index -= width;
					if (index < 0) index = 0;

					SelectedItems[0].Selected = false;
					mvarItems[index].Selected = true;

					Rectangle rect1 = GetItemBounds(mvarItems[index]);
					if (rect1.Y < 0)
					{
						ScrollOffsetY ++;
						ResetBounds();
					}

					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					return true;
				}
				case Keys.Down:
				{
					if (SelectedItems.Count == 0) break;

					Rectangle rect = GetItemBounds();
					int width = (int)((double)this.Width / (double)rect.Width);

					int index = mvarItems.IndexOf(SelectedItems[0]);
					index += width;
					if (index >= mvarItems.Count) index = mvarItems.Count - 1;

					SelectedItems[0].Selected = false;
					mvarItems[index].Selected = true;

					Rectangle rect1 = GetItemBounds(mvarItems[index]);
					if (rect1.Bottom > base.Height)
					{
						ScrollOffsetY--;
						ResetBounds();
					}

					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					return true;
				}
				case Keys.Left:
				{
					if (SelectedItems.Count == 0) break;

					if (SelectedItems[0].Items.Count > 0 && SelectedItems[0].Expanded)
					{
						SelectedItems[0].Expanded = false;
						ResetBounds();
						Refresh();
						return true;
					}

					int index = mvarItems.IndexOf(SelectedItems[0]);
					index--;
					if (index < 0) index = 0;

					SelectedItems[0].Selected = false;
					mvarItems[index].Selected = true;

					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					return true;
				}
				case Keys.Right:
				{
					if (SelectedItems.Count == 0) break;

					if (SelectedItems[0].Items.Count > 0 && !SelectedItems[0].Expanded)
					{
						SelectedItems[0].Expanded = true;
						ResetBounds();
						Refresh();
						return true;
					}

					int index = mvarItems.IndexOf(SelectedItems[0]);
					index++;
					if (index >= mvarItems.Count) index = mvarItems.Count - 1;

					SelectedItems[0].Selected = false;
					mvarItems[index].Selected = true;

					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			switch (e.KeyCode)
			{
				case Keys.F2:
				{
					if (mvarEnableAutomaticInlineRenaming)
					{
						if (SelectedItems.Count == 1)
						{
							BeginLabelEdit(SelectedItems[0]);
						}
						e.Handled = true;
						e.SuppressKeyPress = true;
						return;
					}
					break;
				}
				case Keys.Home:
				{
					if (mvarItems.Count == 0) return;

					foreach (ListViewItem lvi in mvarItems)
					{
						lvi.Selected = false;
					}
					mvarItems[0].Selected = true;
					ScrollOffsetY = 0;
					ResetBounds();

					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					break;
				}
				case Keys.End:
				{
					if (mvarItems.Count == 0) return;

					foreach (ListViewItem lvi in mvarItems)
					{
						lvi.Selected = false;
					}
					mvarItems[mvarItems.Count - 1].Selected = true;
					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					break;
				}
				case Keys.PageDown:
				{
					Rectangle rect = GetItemBounds();
					int times = (int)((double)this.Height / (double)rect.Height);
					int width = (int)((double)this.Width / (double)rect.Width);

					ScrollOffsetY -= times;
					foreach (ListViewItem lvi in mvarItems)
					{
						RecursiveSetSelected(lvi, false);
					}

					int index = -ScrollOffsetY * width;
					if (index >= mvarItems.Count) index = mvarItems.Count - 1;
					mvarItems[index].Selected = true;

					if (ScrollOffsetY > 0) ScrollOffsetY = 0;
					ResetBounds();
					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					break;
				}
				case Keys.PageUp:
				{
					Rectangle rect = GetItemBounds();
					int times = (int)((double)this.Height / (double)rect.Height);
					int width = (int)((double)this.Width / (double)rect.Width);

					ScrollOffsetY += times;
					foreach (ListViewItem item in mvarItems)
					{
						item.Selected = false;
					}

					if (ScrollOffsetY > 0) ScrollOffsetY = 0;

					int index = -ScrollOffsetY * width;
					if (index > mvarItems.Count - 1)
					{
						index = mvarItems.Count - 1;
						ScrollOffsetY = (index / width) * -1;
					}
					mvarItems[index].Selected = true;

					ResetBounds();
					Refresh();
					OnSelectionChanged(EventArgs.Empty);
					break;
				}
				case Keys.Enter:
				{
					if (SelectedItems.Count > 0)
					{
						OnItemActivate(EventArgs.Empty);
					}
					break;
				}
				default:
				{
					if ((e.KeyValue >= (int)'a' && e.KeyValue <= (int)'z') || (e.KeyValue >= (int)'A' && e.KeyValue <= (int)'Z') || (e.KeyValue >= (int)'0' && e.KeyValue <= (int)'9'))
					{
						foreach (ListViewItem lvi in mvarItems)
						{
							if (String.IsNullOrEmpty(lvi.Text)) continue;

							if (lvi.Text.StartsWith(((char)e.KeyValue).ToString()))
							{
								/*
								Rectangle rectDefault = GetItemBounds();
								Rectangle rect = GetItemBounds(lvi);
								int times = (int)((double)this.Height / (double)(rectDefault.Height)) + 1;
								int width = (int)((double)this.Width / (double)rect.Width);

								ScrollOffsetY -= times;

								lvi.Selected = true;
								ResetBounds();
								Refresh();
								*/

								// need a ScrollToItem method!
								break;
							}
						}
						OnSelectionChanged(EventArgs.Empty);
					}
					break;
				}
			}
		}

		public event ListViewItemLabelEditingEventHandler ItemLabelEditing;
		protected virtual void OnItemLabelEditing(ListViewItemLabelEditingEventArgs e)
		{
			if (ItemLabelEditing != null) ItemLabelEditing(this, e);
		}

		public event ListViewItemLabelEditedEventHandler ItemLabelEdited;
		protected virtual void OnItemLabelEdited(ListViewItemLabelEditedEventArgs e)
		{
			if (ItemLabelEdited != null) ItemLabelEdited(this, e);
		}

		private void BeginLabelEdit(ListViewItem lvi)
		{
			ListViewItemLabelEditingEventArgs e = new ListViewItemLabelEditingEventArgs(lvi, lvi.Text);
			OnItemLabelEditing(e);
			if (e.Cancel) return;

			Rectangle bounds = GetItemBounds(lvi);

			switch (mvarMode)
			{
				case ListViewMode.List:
				case ListViewMode.Details:
				{
					bounds.X += 25;
					bounds.Y += 2;
					if (mvarColumns.Count > 0)
					{
						bounds.Width = mvarColumns[0].Width - bounds.X;
					}
					break;
				}
				case ListViewMode.Tiles:
				{
					bounds.X += 55;
					bounds.Y += 1;
					bounds.Height = 18;
					bounds.Width -= 56;
					break;
				}
			}

			txtRename.Location = bounds.Location;
			txtRename.Size = bounds.Size;

			txtRename.Tag = lvi;
			txtRename.Text = e.Label;
			txtRename.Enabled = true;
			txtRename.Visible = true;

			inhibitEndLabelEdit = true;
			txtRename.Focus();
			inhibitEndLabelEdit = false;
		}
		private bool inhibitEndLabelEdit = false;
		private void EndLabelEdit(bool cancel)
		{
			// if (!this.FindForm().Focused) return;
			if (inhibitEndLabelEdit) return;

			if (cancel && _labelEditTimer != null)
			{
				TimerMethods.ClearTimeout(_labelEditTimer);
				_labelEditTimer = null;
			}

			ListViewItem lvi = (txtRename.Tag as ListViewItem);
			if (!cancel) lvi.Text = txtRename.Text;

			txtRename.Visible = false;
			txtRename.Enabled = false;

			if (txtRename.Focused) this.Focus();

			if (!cancel) OnItemLabelEdited(new ListViewItemLabelEditedEventArgs(lvi));
		}
		#endregion
		#endregion

		private int ScrollOffsetX = 0, ScrollOffsetY = 0;

		protected virtual void OnItemDrag(ListViewItemDragEventArgs e)
		{
			if (ItemDrag != null) ItemDrag(this, e);
		}
		protected virtual void OnItemDragComplete(ListViewItemDragEventArgs e)
		{
			if (ItemDragComplete != null) ItemDragComplete(this, e);
		}

		private ListViewColumn ColumnHitTest(Point pt)
		{
			if (pt.Y > mvarColumnHeaderHeight) return null;

			int x = 0;
			foreach (ListViewColumn lvc in mvarColumns)
			{
				if (pt.X >= x && pt.X <= (x + lvc.Width))
				{
					return lvc;
				}
				x += lvc.Width;
			}
			return null;
		}

		private bool mvarUseThemeBackground = true;
		/// <summary>
		/// Determines if the current theme should be used to draw the <see cref="ListViewControl" /> background.
		/// </summary>
		[Description("Determines if the current theme should be used to draw the ListViewControl background."), DefaultValue(true)]
		public bool UseThemeBackground { get { return mvarUseThemeBackground; } set { mvarUseThemeBackground = value; } }

		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);

			if (mvarUseThemeBackground)
			{
				Theming.Theme.CurrentTheme.DrawListViewBackground(e.Graphics, new Rectangle(0, 0, base.Width, base.Height));
			}
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (ShouldShowColumns())
			{
				Rectangle daaaRect = new Rectangle(0, 0, base.Width, mvarColumnHeaderHeight);
				Theming.Theme.CurrentTheme.DrawListColumnBackground(e.Graphics, daaaRect, ControlState.Normal, false);

				Rectangle columnRect = new Rectangle(0, 0, 0, mvarColumnHeaderHeight);
				int x = 0;
				foreach (ListViewColumn col in mvarColumns)
				{
					RenderColumn(e.Graphics, col, ref columnRect);

					if (mvarShowGridLines && mvarMode == ListViewMode.Details)
					{
						// draw grid lines for the column
						e.Graphics.DrawLine(DrawingTools.Pens.ControlPen, x, 0, x, base.Height);
					}
					x += col.Width;
				}
				if (m_DraggingColumn != null) RenderColumn(e.Graphics, m_DraggingColumn, ref columnRect);
			}

			List<ListViewItem> items = mvarItems.ToList<ListViewItem>();
			if (mvarSortColumn != null)
			{
				items.Sort(new Comparison<ListViewItem>(listViewItemSorter_Sort));
				ResetBounds();
			}

			int lastItemRectBottom = 0;

			int width = (int)((double)this.Width / (double)GetItemBounds().Width);
			int height = (int)((double)this.Height / (double)GetItemBounds().Height);
			int start = -ScrollOffsetY * width;
			if (start < 0) start = 0;

			int end = items.Count - 1; // start + height;
			if (end >= items.Count) end = items.Count - 1;

			for (int i = start; i <= end; i++)
			{
				ListViewItem lvi = items[i];

				// TODO: fix this for offsets when scroll bar is implemented
				// also, this is a GREAT boon to large lists (like Heroes 3 LOD files!)
				Rectangle rect = GetItemBounds(lvi);
				if (rect.X >= ClientRectangle.Right || rect.Y >= ClientRectangle.Bottom) break;

				RenderItem(e.Graphics, lvi, 0, ref lastItemRectBottom);
			}

			if (mvarShowGridLines && mvarMode == ListViewMode.Details)
			{
				// draw grid lines for other items
				for (int i = lastItemRectBottom; i < base.Height; i += mvarDefaultItemHeight)
				{
					e.Graphics.DrawLine(new Pen(Color.FromKnownColor(KnownColor.Control)), 0, i + 1, base.Width - 1, i + 1);
				}
			}

			if (m_RectangleSelect)
			{
				Rectangle selRect = GetSelectionRectangle();
				Theming.Theme.CurrentTheme.DrawListSelectionRectangle(e.Graphics, selRect);
			}
		}

		private void RenderItem(Graphics g, ListViewItem item, int level, ref int lastItemRectBottom)
		{
			if (Updating) return;

			Rectangle itemRect = GetItemBounds(item);

			itemRect.Width--;
			itemRect.Height--;

			Font font = item.Font;
			if (font == null) font = base.Font;
			Size sz = TextRenderer.MeasureText(item.Text, font);

			Color foreColor = (item.ForeColor == Color.Empty ? (Theming.Theme.CurrentTheme.ColorTable.WindowForeground == Color.Empty ? base.ForeColor : Theming.Theme.CurrentTheme.ColorTable.WindowForeground) : item.ForeColor);
			Color backColor = (item.BackColor == Color.Empty ? Color.Empty : item.BackColor);

			Rectangle iconRect = new Rectangle(itemRect.X, itemRect.Y, 32, 32);
			Rectangle textRect = new Rectangle(itemRect.X, itemRect.Bottom - sz.Height, itemRect.Width, sz.Height);

			TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis;

			switch (mvarMode)
			{
				case ListViewMode.ExtraLargeIcons:
				case ListViewMode.LargeIcons:
				case ListViewMode.MediumIcons:
				case ListViewMode.SmallIcons:
				{
					if (sz.Width > itemRect.Width)
					{
						if (item.Selected)
						{
							flags = TextFormatFlags.EndEllipsis;
							textRect.Y -= 20;
							textRect.Height += 32;
							itemRect.Height += 18;
						}
					}
					break;
				}
			}

			if (item == mvarHoverItem)
			{
				Theming.Theme.CurrentTheme.DrawListItemBackground(g, itemRect, ControlState.Hover, item.Selected, Focused);
			}
			else
			{
				Theming.Theme.CurrentTheme.DrawListItemBackground(g, itemRect, ControlState.Normal, item.Selected, Focused);
			}

			bool useGdi = false;

			switch (mvarMode)
			{
				case ListViewMode.SmallIcons:
				case ListViewMode.Details:
				case ListViewMode.List:
				{
					if (mvarSmallImageList != null)
					{
						iconRect.Width = mvarSmallImageList.ImageSize.Width;
						iconRect.Height = mvarSmallImageList.ImageSize.Height;
					}
					else
					{
						iconRect.Width = 16;
						iconRect.Height = 16;
					}

					textRect = new Rectangle(itemRect.X + 6, itemRect.Y + 4, itemRect.Width - 6, itemRect.Height - 6);
					if ((item.ImageIndex > -1 || item.ImageKey != null) && mvarSmallImageList != null)
					{
						if (mvarMode == ListViewMode.SmallIcons)
						{
							textRect.X += iconRect.Width;
						}
						textRect.Width -= iconRect.Width;
					}
					flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
					break;
				}
				case ListViewMode.ExtraLargeIcons:
				case ListViewMode.LargeIcons:
				case ListViewMode.MediumIcons:
				{
					if (sz.Width > itemRect.Width)
					{
						if (item.Selected)
						{
							flags = TextFormatFlags.EndEllipsis;
							useGdi = true;
						}
					}

					if (item.Image != null)
					{
						iconRect.X = itemRect.X + (int)(((double)itemRect.Width - item.Image.Width) / 2);
						iconRect.Y = itemRect.Y + (int)(((double)(itemRect.Height - textRect.Height) - item.Image.Height) / 2);
					}
					break;
				}
				case ListViewMode.Tiles:
				{
					iconRect = new Rectangle(itemRect.X + 2, itemRect.Y + 2, 48, 48);
					textRect = new Rectangle(itemRect.X + 54, itemRect.Y + 2, itemRect.Width - 57, 15);
					flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
					break;
				}
			}
			flags |= TextFormatFlags.EndEllipsis;

			if (mvarMode == ListViewMode.List || mvarMode == ListViewMode.Details)
			{
				int offset = (20 * level);
				textRect.X += offset;
				textRect.Width -= offset;
			}

			if (item.Image != null)
			{
				g.DrawImage(item.Image, iconRect);
			}
			else if (item.ImageKey != null)
			{
				EnsureImageListsExist();
				if (primaryImageList.Images.ContainsKey(item.ImageKey))
				{
					g.DrawImage(primaryImageList.Images[item.ImageKey], iconRect);
				}
				else if (secondaryImageList.Images.ContainsKey(item.ImageKey))
				{
					g.DrawImage(secondaryImageList.Images[item.ImageKey], iconRect);
				}
			}
			else if (item.ImageIndex > -1)
			{
				EnsureImageListsExist();
				if (item.ImageIndex < primaryImageList.Images.Count)
				{
					g.DrawImage(primaryImageList.Images[item.ImageIndex], iconRect);
				}
				else if (item.ImageIndex < secondaryImageList.Images.Count)
				{
					g.DrawImage(secondaryImageList.Images[item.ImageIndex], iconRect);
				}
			}

			if (useGdi)
			{
				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;
				sf.LineAlignment = StringAlignment.Far;
				sf.Trimming = StringTrimming.None;
				g.DrawString(item.Text, font, new SolidBrush(foreColor), textRect, sf);
			}
			else
			{
				if (mvarColumns.Count > 0 && mvarMode == ListViewMode.Details)
				{
					textRect.Width = mvarColumns[0].Width - textRect.Left - iconRect.Width;
				}

				if (mvarMode == ListViewMode.List || mvarMode == ListViewMode.Details)
				{
					if (item.Items.Count > 0)
					{
						Rectangle rectTree = textRect;
						rectTree.Width = 16;

						Theming.Theme.CurrentTheme.DrawListViewTreeGlyph(g, rectTree, ControlState.Normal, item.Expanded);
					}

					textRect.X += 18;
				}

				TextRenderer.DrawText(g, item.Text, font, textRect, foreColor, item.BackColor, flags);

				if (mvarMode == ListViewMode.Details)
				{
					textRect.X -= 18;
				}

				#region Tiles
				if (mvarMode == ListViewMode.Tiles)
				{
					int maxdetails = 2;
					Rectangle textRect2 = textRect;
					textRect2.Y += textRect2.Height + 2;
					for (int i = 0; i < item.Details.Count; i++)
					{
						foreColor = (item.Details[i].ForeColor == Color.Empty ? (item.ForeColor == Color.Empty ? (Theming.Theme.CurrentTheme.ColorTable.WindowForeground == Color.Empty ? base.ForeColor : Theming.Theme.CurrentTheme.ColorTable.WindowForeground) : item.ForeColor) : item.Details[i].ForeColor);
						backColor = (item.Details[i].BackColor == Color.Empty ? Color.Empty : item.Details[i].BackColor);

						if (item.Details[i] is ListViewDetailLabel)
						{
							TextRenderer.DrawText(g, (item.Details[i] as ListViewDetailLabel).Text, font, textRect2, foreColor, backColor, TextFormatFlags.Left);
						}
						textRect2.Y += textRect2.Height + 2;
						if ((i + 1) >= maxdetails) break;
					}
				}
				#endregion
				#region Details
				else if (mvarMode == ListViewMode.Details)
				{
					int x = 0;
					foreach (ListViewColumn column in mvarColumns)
					{
						int ic = mvarColumns.IndexOf(column) - 1;
						if (ic > -1)
						{
							if (ic < item.Details.Count)
							{
								int w = column.Width - textRect.Left;

								Rectangle rect = new Rectangle(textRect.Left + x, textRect.Top, w, textRect.Height);
								if (item.Details[ic].ForeColor != Color.Empty) foreColor = item.Details[ic].ForeColor;
								if (item.Details[ic].BackColor != Color.Empty) backColor = item.Details[ic].BackColor;


								if (item.Details[ic] is ListViewDetailLabel)
								{
									TextRenderer.DrawText(g, (item.Details[ic] as ListViewDetailLabel).Text, font, rect, foreColor, backColor, TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
								}
								x += w;
							}
						}
						else
						{
							x += column.Width;
						}
					}
				}
				#endregion
			}

			if (mvarShowGridLines && mvarMode == ListViewMode.Details)
			{
				// draw the grid line below this item
				g.DrawLine(new Pen(Color.FromKnownColor(KnownColor.Control)), 0, itemRect.Bottom + 1, base.Width - 1, itemRect.Bottom + 1);
			}
			lastItemRectBottom = itemRect.Bottom;

			if ((mvarMode == ListViewMode.List || mvarMode == ListViewMode.Details) && item.Expanded)
			{
				foreach (ListViewItem item1 in item.Items)
				{
					RenderItem(g, item1, level + 1, ref lastItemRectBottom);
				}
			}
		}

		private void EnsureImageListsExist()
		{
			if (primaryImageList == null || secondaryImageList == null)
			{
				switch (mvarMode)
				{
					case ListViewMode.Details:
					case ListViewMode.List:
					case ListViewMode.SmallIcons:
					{
						primaryImageList = mvarSmallImageList;
						secondaryImageList = mvarLargeImageList;
						break;
					}
					case ListViewMode.ExtraLargeIcons:
					case ListViewMode.LargeIcons:
					case ListViewMode.MediumIcons:
					case ListViewMode.Tiles:
					{
						primaryImageList = mvarLargeImageList;
						secondaryImageList = mvarSmallImageList;
						break;
					}
				}
			}
		}

		private void RenderColumn(Graphics g, ListViewColumn col, ref Rectangle rect)
		{
			if (col == m_DraggingColumn)
			{
				rect.X = m_PressedColumnX;
			}
			rect.Width = col.Width;

			ControlState state = ControlState.Normal;
			if (col == m_PressedColumn)
			{
				state = ControlState.Pressed;
			}
			else if (col == m_HoverColumn)
			{
				state = ControlState.Hover;
			}

			Theming.Theme.CurrentTheme.DrawListColumnBackground(g, rect, state, col == mvarSortColumn);
			Font font = base.Font;
			Color foreColor = ForeColor;

			Rectangle textRect = rect;
			textRect.Y -= 1;
			textRect.X += 2;

			Size sz = TextRenderer.MeasureText(col.Text, font);
			sz.Height = mvarColumnHeaderHeight;
			textRect.Size = sz;

			switch (state)
			{
				case ControlState.Normal:
				{
					foreColor = Theming.Theme.CurrentTheme.ColorTable.ListViewColumnHeaderForegroundNormal;
					break;
				}
				case ControlState.Hover:
				{
					foreColor = Theming.Theme.CurrentTheme.ColorTable.ListViewColumnHeaderForegroundHover;
					break;
				}
				case ControlState.Pressed:
				{
					foreColor = Theming.Theme.CurrentTheme.ColorTable.ListViewColumnHeaderForegroundSelected;
					break;
				}
			}

			TextRenderer.DrawText(g, col.Text, font, textRect, foreColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

			if (col == mvarSortColumn)
			{
				switch (mvarSortDirection)
				{
					case ListSortDirection.Ascending:
					{
						DrawingTools.DrawArrow(g, Theming.Theme.CurrentTheme.ColorTable.ListViewColumnHeaderArrowNormal, DrawingTools.Direction.Up, textRect.Right + 4, (mvarColumnHeaderHeight / 2) - 2, 4);
						break;
					}
					case ListSortDirection.Descending:
					{
						DrawingTools.DrawArrow(g, Theming.Theme.CurrentTheme.ColorTable.ListViewColumnHeaderArrowNormal, DrawingTools.Direction.Down, textRect.Right + 4, (mvarColumnHeaderHeight / 2) - 2, 4);
						break;
					}
				}
			}

			if (mvarColumns.IndexOf(col) < mvarColumns.Count - 1)
			{
				if (col == m_DraggingColumn)
				{
					rect.X = m_PressedColumnOriginalX + rect.Width;
				}
				else
				{
					rect.X += rect.Width;
				}
			}
		}

		private Rectangle GetSelectionRectangle()
		{
			int x, y, w, h = 0;
			if (m_RectangleSelectPosStart.X < m_RectangleSelectPosEnd.X)
			{
				x = m_RectangleSelectPosStart.X;
				w = m_RectangleSelectPosEnd.X - m_RectangleSelectPosStart.X;
			}
			else
			{
				x = m_RectangleSelectPosEnd.X;
				w = m_RectangleSelectPosStart.X - m_RectangleSelectPosEnd.X;
			}
			if (m_RectangleSelectPosStart.Y < m_RectangleSelectPosEnd.Y)
			{
				y = m_RectangleSelectPosStart.Y;
				h = m_RectangleSelectPosEnd.Y - m_RectangleSelectPosStart.Y;
			}
			else
			{
				y = m_RectangleSelectPosEnd.Y;
				h = m_RectangleSelectPosStart.Y - m_RectangleSelectPosEnd.Y;
			}
			return new Rectangle(x, y, w, h);
		}

		private ListViewItemBoundsCollection itemBounds = new ListViewItemBoundsCollection();

		private int _updating = 0;
		public void BeginUpdate()
		{
			_updating++;
		}
		public void EndUpdate()
		{
			_updating--;

			ResetBounds();
			Refresh();
		}
		public bool Updating
		{
			get { return (_updating != 0); }
		}

		public Rectangle GetItemBounds()
		{
			Point ptOffset = new Point(15, 6);
			switch (mvarMode)
			{
				case ListViewMode.List:
				case ListViewMode.Details:
				{
					ptOffset = new Point(4, 2);
					break;
				}
			}

			if (ShouldShowColumns())
			{
				ptOffset.Y += mvarColumnHeaderHeight;
			}

			Size szExtraLargeIcons = new Size(271, 276);
			Size szLargeIcons = new Size(105, 116);   // large icons
			Size szMediumIcons = new Size(74, 68);
			Size szSmallIcons = new Size(230, 20);
			Size szTiles = new System.Drawing.Size(250, 52);

			Rectangle bounds = new Rectangle(ptOffset, szTiles);
			switch (mvarMode)
			{
				case ListViewMode.ExtraLargeIcons:
				{
					bounds.Size = szExtraLargeIcons;
					break;
				}
				case ListViewMode.LargeIcons:
				{
					bounds.Size = szLargeIcons;
					break;
				}
				case ListViewMode.MediumIcons:
				{
					bounds.Size = szMediumIcons;
					break;
				}
				case ListViewMode.SmallIcons:
				{
					bounds.Size = szSmallIcons;
					break;
				}
				case ListViewMode.Tiles:
				{
					bounds.Size = szTiles;
					break;
				}
				case ListViewMode.Details:
				{
					bounds.Height = szSmallIcons.Height;
					if (mvarFullRowSelect)
					{
						bounds.Width = base.Width - 8;
					}
					break;
				}
				case ListViewMode.List:
				{
					bounds.Height = szSmallIcons.Height;
					if (mvarFullRowSelect)
					{
						bounds.Width = base.Width - 8;
					}
					break;
				}
			}
			return bounds;
		}
		public Rectangle GetItemBounds(ListViewItem lvi)
		{
			if (!itemBounds.Contains(lvi))
			{
				ResetBounds();
			}
			return itemBounds.GetBounds(lvi);
		}

		public ListViewItem HitTest(Point point)
		{
			int width = (int)((double)this.Width / (double)GetItemBounds().Width);
			int start = -ScrollOffsetY * width;
			return itemBounds.GetItemAtPoint(point, start);
		}

		private ListViewMode mvarMode = ListViewMode.Tiles;
		public ListViewMode Mode
		{
			get { return mvarMode; }
			set
			{
				mvarMode = value;

				primaryImageList = null;
				secondaryImageList = null;
				ResetBounds();
				Refresh();
			}
		}

		private ImageList primaryImageList = null;
		private ImageList secondaryImageList = null;

		private ListSortDirection mvarSortDirection = ListSortDirection.Ascending;

		internal void ResetBounds()
		{
			if (Updating) return;

			itemBounds.Clear();
			Rectangle bounds = GetItemBounds();

			List<ListViewItem> items = mvarItems.ToList<ListViewItem>();
			if (mvarSortColumn != null)
			{
				items.Sort(new Comparison<ListViewItem>(listViewItemSorter_Sort));
			}

			foreach (ListViewItem lvi in items)
			{
				RecursiveCalculateItemBounds(lvi, ref bounds, 0);
			}

			if (!scrolling)
			{
				vsc.Minimum = 0;
				if (this.Height > 0)
				{
					vsc.Maximum = (int)((double)bounds.Bottom / (double)this.Height);
				}
				else
				{
					vsc.Maximum = 0;
				}
				vsc.Value = 0;
			}
		}

		private void RecursiveCalculateItemBounds(ListViewItem lvi, ref Rectangle bounds, int level)
		{
			Font font = lvi.Font;
			if (font == null) font = base.Font;
			Size sz = TextRenderer.MeasureText(lvi.Text, font);

			switch (mvarMode)
			{
				case ListViewMode.ExtraLargeIcons:
				case ListViewMode.LargeIcons:
				case ListViewMode.MediumIcons:
				case ListViewMode.SmallIcons:
				case ListViewMode.Tiles:
				{
					if (lvi.Text != null)
					{
						if (lvi.Text.Contains(" ") || lvi.Selected)
						{
							if (sz.Width > bounds.Width)
							{
								// what's the reason for this?
								// bounds.Height += 20;
							}
						}
					}
					
					int offsetX = 0;
					if (vsc.Visible) offsetX += vsc.Width;
					int offsetY = 0;
					if (hsc.Visible) offsetY += hsc.Height;

					if (bounds.Right > (Width + offsetX))
					{
						bounds.X = 15;
						bounds.Y += bounds.Height;
					}
					break;
				}
				case ListViewMode.List:
				{
					if (!mvarFullRowSelect)
					{
						bounds.Width = sz.Width + 6;
						if ((lvi.ImageIndex > -1 || lvi.ImageKey != null) && mvarSmallImageList != null)
						{
							bounds.Width += mvarSmallImageList.ImageSize.Width;
						}
					}
					break;
				}
			}

			if (mvarMode == ListViewMode.List || mvarMode == ListViewMode.Details)
			{
				bounds.X = 4;
			}

			bounds.Y += (ScrollOffsetY * bounds.Height);
			itemBounds.Add(lvi, bounds);
			bounds.Y -= (ScrollOffsetY * bounds.Height);

			switch (mvarMode)
			{
				case ListViewMode.ExtraLargeIcons:
				case ListViewMode.LargeIcons:
				case ListViewMode.MediumIcons:
				case ListViewMode.SmallIcons:
				case ListViewMode.Tiles:
				{
					bounds.X += bounds.Width + 2;
					break;
				}
				case ListViewMode.Details:
				case ListViewMode.List:
				{
					bounds.Y += bounds.Height;
					break;
				}
			}

			if (lvi.Expanded)
			{
				foreach (ListViewItem lvi1 in lvi.Items)
				{
					RecursiveCalculateItemBounds(lvi1, ref bounds, level + 1);
				}
			}
		}

		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			ResetBounds();
			Refresh();

			/*
			foreach (Rectangle in itemBounds.GetAllBounds())
			{
				if (bound.Bounds.Right >= ClientRectangle.Right - 17 || bound.Bounds.Bottom >= ClientRectangle.Bottom - 17)
				{
					hsc.Visible = true;
					vsc.Visible = true;
					scrl.Visible = true;
					return;
				}
			}
			*/
		}

		private Color mvarShadeColor = Color.FromKnownColor(KnownColor.WhiteSmoke);
		
		private int listViewItemSorter_Sort(ListViewItem lvi1, ListViewItem lvi2)
		{
			int cmp = 0;
			if (mvarSortColumn != null)
			{
				int index = mvarColumns.IndexOf(mvarSortColumn) - 1;
				if (index > -1)
				{
					if (index < lvi1.Details.Count && index < lvi2.Details.Count)
					{
						cmp = lvi1.Details[index].CompareTo(lvi2.Details[index]);
						if (mvarSortDirection == ListSortDirection.Descending) cmp *= -1;
						return cmp;
					}
				}
			}
			cmp = lvi1.Text.CompareTo(lvi2.Text);
			if (mvarSortDirection == ListSortDirection.Descending) cmp *= -1;
			return cmp;
		}

		public Color ShadeColor { get { return mvarShadeColor; } set { mvarShadeColor = value; } }

		private void mnuContextViewType_Click(object sender, EventArgs e)
		{
			foreach (ToolStripMenuItem tsmi in mnuContextView.DropDownItems)
			{
				if (tsmi == sender)
				{
					tsmi.Checked = true;
					if (tsmi == mnuContextViewTiles)
					{
						Mode = ListViewMode.Tiles;
					}
					else if (tsmi == mnuContextViewIcons)
					{
						Mode = ListViewMode.MediumIcons;
					}
					else if (tsmi == mnuContextViewDetails)
					{
						Mode = ListViewMode.Details;
					}
					else if (tsmi == mnuContextViewList)
					{
						Mode = ListViewMode.List;
					}
				}
				else
				{
					tsmi.Checked = false;
				}
			}
		}

		private bool scrolling = false;
		private void vsc_Scroll(object sender, ScrollEventArgs e)
		{
			scrolling = true;
			ScrollOffsetY = -vsc.Value;
			ResetBounds();
			Refresh();
			scrolling = false;
		}

		public void Clear()
		{
			mvarColumns.Clear();
			mvarItems.Clear();
		}


		public void AutoResizeColumn(ListViewColumn lvc, ListViewColumnAutoResizeMode mode = ListViewColumnAutoResizeMode.Both)
		{
			int index = mvarColumns.IndexOf(lvc) - 1;
			int width = 4;
			foreach (ListViewItem lvi1 in mvarItems)
			{
				Font font = lvi1.Font;
				if (font == null) font = Font;

				if (index > -1)
				{
					ListViewDetail detail = lvi1.Details[index];
					int testWidth = 0;
					if (detail is ListViewDetailLabel)
					{
						testWidth = TextRenderer.MeasureText((detail as ListViewDetailLabel).Text, font).Width;
					}
					else if (detail is ListViewDetailChoice)
					{
						testWidth = mvarColumns[lvi1.Details.IndexOf(detail)].Width;
					}
					if (testWidth > width) width = testWidth;
				}
				else
				{
					int testWidth = TextRenderer.MeasureText(lvi1.Text, font).Width;
					if (testWidth > width) width = testWidth;
				}
			}
			lvc.Width = width + 32;
		}
		public void AutoResizeColumns(ListViewColumnAutoResizeMode mode = ListViewColumnAutoResizeMode.Both)
		{
			foreach (ListViewColumn lvc in mvarColumns)
			{
				AutoResizeColumn(lvc, mode);
			}
		}

		private void txtRename_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				(txtRename.Tag as ListViewItem).Text = txtRename.Text;
				EndLabelEdit(false);

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
			else if (e.KeyCode == Keys.Escape)
			{
				EndLabelEdit(true);

				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}
	}
}
