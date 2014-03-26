using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AwesomeControls.SystemControls
{
    /// <summary>
    /// Data type of column content
    /// </summary>
    public enum ListViewFilterDataType
    {
        String,
        Number,
        Date
    }


    /// <summary>
    /// Type of comparison requested by the filter
    /// </summary>
    public enum ListViewFilterType
    {
        Equal,
        NotEqual,
        Greater,
        GreaterEqual,
        Less,
        LessEqual
    }

    [DefaultEvent("ItemDoubleClick")]
	public class ListView : System.Windows.Forms.ListView
	{
		#region Private class data

		private bool mvarFiltered = false;              // Display column filters
		private int srt_column = 0;                  // Sort column
		private bool srt_sorder = true;               // Sort ascending/descending
		private ListViewFilterDataType mvarSortType = ListViewFilterDataType.String; // Sort data type comparison
		private bool flt_ignore = false;              // Ignore case on filter
		private Color col_scolor = Color.WhiteSmoke;   // Shade color
		private bool col_shaded = true;               // Display shade color
		private ArrayList itm_filtrs = new ArrayList();    // Array of LVFFliters active
		private ArrayList itm_filtrd = new ArrayList();    // Filtered out items
		private ArrayList itm_holder = new ArrayList();    // Held out items
		private int mnu_column = 0;                  // Filter button column
		private float cmp_float1 = float.MaxValue;     // float value for compare
		private float cmp_float2 = float.MaxValue;     // float value for compare
		private DateTime cmp_datim1 = DateTime.MaxValue;  // date value for compare
		private DateTime cmp_datim2 = DateTime.MaxValue;  // date value for compare

		/// <summary>
		/// Instance pointer to the header control interface
		/// </summary>
		private ListViewFilterHeader mvarHeader = null;

		/// <summary>
		/// Filter data structure for individual column data.
		/// </summary>
		[StructLayout(LayoutKind.Sequential)]
		internal struct LISTVIEWFILTER
		{
			internal int Column;
			internal ListViewFilterType Compare;
			internal ListViewFilterDataType Type;
			internal string Text;
		}

		/// <summary>
		/// This is the context menu and items that appear when a filter
		/// button is clicked.  This allows customizations for a column.
		/// </summary>
		private ContextMenu mnu_filter = new ContextMenu();
		private MenuItem mnu_datype = new MenuItem("&Data type");
		private MenuItem mnu_alignt = new MenuItem("&Alignment");
		private MenuItem mnu_clearf = new MenuItem("&Clear filter");
		private MenuItem mnu_ignore = new MenuItem("&Ignore case");
		private MenuItem mnu_strflt = new MenuItem("&String");
		private MenuItem mnu_nbrflt = new MenuItem("&Number");
		private MenuItem mnu_datflt = new MenuItem("&Date");
		private MenuItem mnu_alignl = new MenuItem("&Left");
		private MenuItem mnu_alignr = new MenuItem("&Right");
		private MenuItem mnu_alignc = new MenuItem("&Center");

		#endregion

		#region Windows API interfaces
		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, Internal.Windows.Constants.ListViewMessage msg, int wParam, int lParam);
		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, Internal.Windows.Constants.ListViewMessage msg, int wParam, ref Internal.Windows.Structures.LVITEM hditem);
		[DllImport("user32.dll")]
		internal static extern int SendMessage(IntPtr hWnd, Internal.Windows.Constants.ListViewMessage msg, int wParam, ref Internal.Windows.Structures.RECT hditem);
		#endregion

		#region Constructor and Dispose methods

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Constructor, nothing done here but InitializeComponent as usual.
		/// </summary>
		public ListView()
		{

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">True if really disposing?</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}


		#endregion

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// NOTE: We have modified this method to include the context
		/// menu and items creation as if this was a form control.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// ListViewFilter
			// 
			this.Name = "ListViewFilter";
			this.ListViewItemSorter = new ListViewFilterSorter(this);
			//
			// mnu_filter
			//
			this.mnu_filter.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.mnu_datype, this.mnu_alignt, this.mnu_clearf, this.mnu_ignore });
			this.mnu_filter.Popup += new System.EventHandler(this.ContextMenuPopup);
			// 
			// mnu_datype
			// 
			this.mnu_datype.Index = 0;
			this.mnu_datype.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.mnu_strflt, this.mnu_nbrflt, this.mnu_datflt });
			// 
			// mnu_alignt
			// 
			this.mnu_alignt.Index = 1;
			this.mnu_alignt.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.mnu_alignl, this.mnu_alignr, this.mnu_alignc });
			// 
			// mnu_clearf
			// 
			this.mnu_clearf.DefaultItem = true;
			this.mnu_clearf.Index = 2;
			this.mnu_clearf.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_refrsh
			// 
			this.mnu_ignore.Index = 3;
			this.mnu_ignore.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_strflt
			// 
			this.mnu_strflt.Index = 0;
			this.mnu_strflt.RadioCheck = true;
			this.mnu_strflt.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_nbrflt
			// 
			this.mnu_nbrflt.Index = 1;
			this.mnu_nbrflt.RadioCheck = true;
			this.mnu_nbrflt.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_datflt
			// 
			this.mnu_datflt.Index = 2;
			this.mnu_datflt.RadioCheck = true;
			this.mnu_datflt.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_alignl
			// 
			this.mnu_alignl.Index = 0;
			this.mnu_alignl.RadioCheck = true;
			this.mnu_alignl.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_alignr
			// 
			this.mnu_alignr.Index = 1;
			this.mnu_alignr.RadioCheck = true;
			this.mnu_alignr.Click += new System.EventHandler(this.MenuItemClick);
			// 
			// mnu_alignc
			// 
			this.mnu_alignc.Index = 2;
			this.mnu_alignc.RadioCheck = true;
			this.mnu_alignc.Click += new System.EventHandler(this.MenuItemClick);

		}


		#endregion

		#region Overridden System.Windows.Forms.Control methods

		/// <summary>
		/// When the handle changes we need to recreate the Filter
		/// Header instance.  The previous (if any) header instance
		/// was tied to a handle that is now gone.
		/// </summary>
		/// <param name="e">Event</param>
		protected override void OnHandleCreated(System.EventArgs e)
		{
			base.OnHandleCreated(e);

			// now that we have a handle, create the HeaderControl instance
			mvarHeader = new ListViewFilterHeader(this, mvarFiltered,
			  srt_column, srt_sorder);

		}

		/// <summary>
		/// When our handle is removed we give the hdr_contrl a little
		/// help by telling him now is a good time to release its handle.
		/// From now on, we do not have a hdr_contrl instance active
		/// so we set hdr_contrl to null and don't use it until reset.
		/// </summary>
		/// <param name="e">Event</param>
		protected override void OnHandleDestroyed(System.EventArgs e)
		{

			// free and then delete the ListViewFilterHeader control
			if (mvarHeader != null)
			{
				mvarHeader.ReleaseHandle();
				mvarHeader = null;
			}

			// do this AFTER we tell the ListViewFilterHeader to release
			base.OnHandleDestroyed(e);

		}


		/// <summary>
		/// Only three windows messages are trapped here: WM_ERASEBKGND 
		/// since the OnPaintBackground doesn't work for the ListView,
		/// WM_NOTIFY so that we can get all the ListViewFilterHeader
		/// notificiations, and OCM_NOTIFY for our own notifications.
		/// </summary>
		/// <param name="m">Message</param>
		protected override void WndProc(ref System.Windows.Forms.Message m)
		{
			base.WndProc(ref m);

			// determine if we want to process this message type
			switch (m.Msg)
			{

				// erase background causes us to repaint the shaded sort column.
				case (int)Internal.Windows.Constants.WindowMessage.EraseBackground:

					// there has to be a header and we have to be in Details mode
					if (col_shaded && (mvarHeader != null) && (this.View == View.Details))
					{

						// get the 'fake' size information from the header
						// column.  this is only the Width and Left (as Height).
						Size z = mvarHeader.SizeInfo[srt_column];

						// get a temporary graphics object to fill the rectangle.
						// NOTE: it would be nice to g.ReleaseHdc( m.WParam ) but
						// it doesn't work for some reason, it may not be needed...
						Graphics g = Graphics.FromHdc(m.WParam);
						g.FillRectangle(new SolidBrush(col_scolor),
						  z.Height, Top, z.Width, Height);

					}
					break;

				// notify messages can come from the header and are used 
				// for column sorting and item filtering if wanted
				case (int)Internal.Windows.Constants.WindowMessage.Notify:

					// get the notify message header from this message LParam
					Internal.Windows.Structures.NMHEADER h1 = (Internal.Windows.Structures.NMHEADER)m.GetLParam(typeof(Internal.Windows.Structures.NMHEADER));

					// process messages ONLY from our header control
					if ((mvarHeader != null) && (h1.hdr.hwndFrom == mvarHeader.Handle))
						NotifyHeaderMessage(h1);

					break;

				// internal ListView notify messages are reflected to us via OCM
				case (int)Internal.Windows.Constants.ReflectedMessage.Notify:

					// get the notify message header from this message LParam
					Internal.Windows.Structures.NMHEADER h2 = (Internal.Windows.Structures.NMHEADER)m.GetLParam(typeof(Internal.Windows.Structures.NMHEADER));
					// process ONLY messages that are to this ListView control
					if (h2.hdr.hwndFrom == Handle)
					{
						// process only specific ListView notify messages
						switch (h2.hdr.code)
						{
							// draw message, we return a result for the drawstate
							case (int)Internal.Windows.Constants.NotifyMessage.CustomDraw:
							{
								Internal.Windows.Structures.NMLVCUSTOMDRAW d = (Internal.Windows.Structures.NMLVCUSTOMDRAW)m.GetLParam(typeof(Internal.Windows.Structures.NMLVCUSTOMDRAW));
								m.Result = (IntPtr)NotifyCustomDraw(d);
								break;
							}
						}
					}
					break;

			}
		}


		#endregion

		#region Private methods

		/// <summary>
		/// NM_CUSTOMDRAW processing for the ListView control.  Here all
		/// we really want to do is redraw the shaded background for the
		/// column that is currently sorted, let the base do the work.
		/// </summary>
		/// <param name="d">NMLVCUSTOMDRAW to process</param>
		/// <returns>Return the result of the draw stage</returns>
		private Internal.Windows.Constants.CustomDrawReturnValue NotifyCustomDraw(Internal.Windows.Structures.NMLVCUSTOMDRAW d)
		{
			// determine the draw stage and set return value
			switch (d.nmcd.dwDrawStage)
			{
				// first request, ask for each item notification if we shade columns
				case (int)Internal.Windows.Constants.CustomDrawDrawStage.PrePaint:
				{
					if (col_shaded && (this.View == View.Details))
					{
						return Internal.Windows.Constants.CustomDrawReturnValue.NotifyItemDraw;
					}
					break;
				}
				// next request, ask for each sub item notification for this item
				case (int)Internal.Windows.Constants.CustomDrawDrawStage.ItemPrePaint:
				{
					return Internal.Windows.Constants.CustomDrawReturnValue.NotifySubItemDraw;
				}
				// here is the real work, and it is simply ensuring that the
				// correct backcolor is set for the shaded column.  we let the
				// regular windows control drawing do the real work.
				case (int)Internal.Windows.Constants.CustomDrawDrawStage.SubItemPrePaint:
				{
					// ensure that this is a valid item/subitem request
					if (d.nmcd.dwItemSpec < this.Items.Count)
					{

						// get a reference to the item to be rendered
						ListViewItem i = this.Items[d.nmcd.dwItemSpec];

						// is this for a a base item set it's backcolor
						if (d.iSubItem == 0)
							i.BackColor = (d.iSubItem == srt_column)
								? col_scolor : this.BackColor;

						// ensure that the subitem exits before changing it
						else if ((d.iSubItem < i.SubItems.Count) &&
							(i.SubItems[d.iSubItem] != null))
							i.SubItems[d.iSubItem].BackColor =
								(d.iSubItem == srt_column) ? col_scolor : this.BackColor;

					}
					break;
				}
			}

			// let default drawing do the actual rendering
			return Internal.Windows.Constants.CustomDrawReturnValue.Default;
		}


		/// <summary>
		/// When the header control sends a notification we need to 
		/// process the click to sort, button click for options menu,
		/// and filter change to update the items currently visible.
		/// </summary>
		/// <param name="h">NMHEADER</param>
		private void NotifyHeaderMessage(Internal.Windows.Structures.NMHEADER h)
		{
			// process only specific header notification messages
			switch (h.hdr.code)
			{
				case (int)Internal.Windows.Constants.ListViewHeaderNotificationMessage.ItemClickA:
				case (int)Internal.Windows.Constants.ListViewHeaderNotificationMessage.ItemClickW:
				{
					// a header column was clicked, do the sort
					mvarHeader.SortColumn = srt_column = h.iItem;
					srt_sorder = mvarHeader.SortOrder;
					mvarSortType = mvarHeader.DataType[srt_column];
					this.Sort();
					break;
				}
				case (int)Internal.Windows.Constants.ListViewHeaderNotificationMessage.FilterButtonClick:
				{
					// a filter button was clicked; display the popup menu
					// to handle setting filter options for the column
					mnu_column = h.iItem;
					mnu_filter.Show(this, PointToClient(MousePosition));
					break;
				}
				case (int)Internal.Windows.Constants.ListViewHeaderNotificationMessage.FilterChange:
				{
					// a filter content changed, update the items collection

					// if this is for item -1 then this is a clear all filters
					if (h.iItem < 0) itm_filtrs.Clear();

					// if we are filtered this is a real filter data change
					else if (mvarFiltered) FilterBuild(h.iItem);

					// update the items array with new filters applied
					FilterUpdate();
					break;
				}
			}
		}


		/// <summary>
		/// Create a new filter entry for the given column.  This
		/// will replace/add a LVFFilter instance for the column
		/// to the itm_filtrs array for use by FilterUpdate().
		/// </summary>
		/// <param name="c">Column number</param>
		private void FilterBuild(int c)
		{
			// if this column exists in the filters array remove it
			foreach (LISTVIEWFILTER f in itm_filtrs)
			{
				if (f.Column == c)
				{
					itm_filtrs.Remove(f);
					break;
				}
			}

			// get the filter text for this column from the header
			string s = mvarHeader.Filter[c].Trim();

			// if there is any size to it then create a new filter
			if (s.Length > 0)
			{

				// create a new filter object for this column
				LISTVIEWFILTER f = new LISTVIEWFILTER();
				f.Column = c;
				f.Text = s;
				f.Compare = ListViewFilterType.Equal;
				f.Type = mvarHeader.DataType[c];

				// check the first characters of the string to see
				// if this is not a default equality comparison
				switch (s[0])
				{
					case '=':
					{
						f.Text = s.Remove(0, 1);
						break;
					}
					case '!':
					{
						f.Compare = ListViewFilterType.NotEqual;
						f.Text = s.Remove(0, 1);
						break;
					}
					case '>':
					{
						if ((s.Length > 1) && (s[1] == '='))
						{
							f.Compare = ListViewFilterType.GreaterEqual;
							f.Text = s.Remove(0, 2);
						}
						else
						{
							f.Compare = ListViewFilterType.Greater;
							f.Text = s.Remove(0, 1);
						}
						break;
					}
					case '<':
					{
						if ((s.Length > 1) && (s[1] == '='))
						{
							f.Compare = ListViewFilterType.LessEqual;
							f.Text = s.Remove(0, 2);
						}
						else
						{
							f.Compare = ListViewFilterType.Less;
							f.Text = s.Remove(0, 1);
						}
						break;
					}
				}

				// add this to the array of filters
				itm_filtrs.Add(f);

			}
		}


		/// <summary>
		/// Check a compare result against a filter type.  We use
		/// the internal CompareData and ItemText methods to get
		/// the correct text and perform the comparison using the
		/// filter column datatype.  The result from CompareData
		/// is used to check against the Type of filtering applied.
		/// The f (LVFFilter) has been prepared with all the data
		/// about the filtering to be performed; column, text,
		/// datatype, and filtering type by FilterBuild().
		/// </summary>
		/// <param name="r">Result -1/0/1</param>
		/// <param name="f">LVFFilter</param>
		/// <returns></returns>
		private bool FilterCheck(ListViewItem i, LISTVIEWFILTER f)
		{

			// get the result of the data type comparison
			int r = (flt_ignore)
			  ? CompareData(GetItemText(i, f.Column).ToLower(), f.Text.ToLower(), f.Type, true)
			  : CompareData(GetItemText(i, f.Column), f.Text, f.Type, true);

			// compare the result against the filter comparison type
			// a true result means that it passed filtering and should
			// be left in the list of filtered items.
			switch (f.Compare)
			{
				case ListViewFilterType.Equal:
					return (r == 0);
				case ListViewFilterType.NotEqual:
					return (r != 0);
				case ListViewFilterType.Greater:
					return (r > 0);
				case ListViewFilterType.GreaterEqual:
					return (r >= 0);
				case ListViewFilterType.Less:
					return (r < 0);
				case ListViewFilterType.LessEqual:
					return (r <= 0);
				default:
					return (r == 0);
			}
		}


		/// <summary>
		/// Update the Items list since a filter has changed.  This
		/// is done whenever a filter text has changed, the filter
		/// options change (case or datatype).  This is how we remove
		/// items from the ListView.Items[] and put them into a private
		/// ArrayList of 'filtered' items.  The items moved to the
		/// itm_filtrd array will be put back when filters are changed
		/// or disabled.  There is a problem with this however.  The
		/// ListViewItemsCollection has a real hard time processing
		/// many additions to the list.  When a large number of items
		/// are added back in, it takes a long time.  If there is 
		/// another way to Add (I tried AddRange) ListViewItem(s) to
		/// the collection that is faster, I would really like to know.
		/// </summary>
		private void FilterUpdate()
		{
			// halt all updates to the list while we are working
			this.BeginUpdate();

			// set a flag to say that no change has occurred
			// so that we don't unnecessarily sort the items
			bool c = false;

			// if there are any filters applied, process them.
			// Items removed are not put into the itm_filtrd 
			// list at this time since we would then have to
			// walk them again in the second loop.  instead,
			// they are added to a holding array (itm_holder)
			// which is later copied into the itm_filtrd list.
			// NOTE: we walk the Items list first, then check
			// each itm_filtrs against that one item.  That is
			// much faster than walking the Items list for each
			// itm_filtrs entry, since there is normally only
			// one or two columns filtered at any time.
			if (itm_filtrs.Count != 0)
			{

				// we will use a listviewitem instance here
				ListViewItem l;

				// prepare the held out items array to contain items
				// removed this pass so we don't check them twice
				itm_holder.Clear();

				// check each item in the Items list to see if it
				// should be removed to the filtered list
				for (int i = 0; i < this.Items.Count; ++i)
				{

					// get the item to check against the filters
					l = this.Items[i];

					// any filter failure removes from it from the
					// Items list and adds it to the holding list.
					// NOTE: the index (i) is decremented when we
					// remove an item since the list is compressed.
					foreach (LISTVIEWFILTER f in itm_filtrs)
						if (!FilterCheck(l, f))
						{
							itm_holder.Add(l);
							this.Items.RemoveAt(i--);
							c = true;
							break;
						}
				}

				// check each item in the currently filtered array
				// to see if it should be put back in the items now.
				// NOTE: this is the slow part when many items need
				// to be put back into the ListViewItemsCollection...
				for (int i = 0; i < itm_filtrd.Count; ++i)
				{

					// get the object as a listviewitem
					l = (ListViewItem)itm_filtrd[i];

					// set no filter failure flag this item
					bool r = true;

					// all filters must pass, stop at one failure
					foreach (LISTVIEWFILTER f in itm_filtrs)
						if (!FilterCheck(l, f))
						{
							r = false;
							break;
						}

					// if we did not fail any filter put it back in Items
					// and remove it from the filtered items: itm_filtrd.
					// NOTE: the index (i) is decremented when we
					// remove an item since the list is compressed.
					if (r)
					{
						this.Items.Add(l);
						itm_filtrd.RemoveAt(i--);
						c = true;
					}
				}

				// now add all the holder items into the filtered list
				// and empty the held out items array to remove reference
				itm_filtrd.AddRange(itm_holder);
				itm_holder.Clear();

			}

			// no filters active put any filtered items back into
			// the items list and empty the filtered items array
			else
			{
				// set the do sort flag only if there are entries
				c = (itm_filtrd.Count > 0);

				// put all filtered items back and clear the list
				foreach (ListViewItem l in itm_filtrd) Items.Add(l);
				itm_filtrd.Clear();

			}

			// resort the items if the item content has changed
			if (c) this.Sort();

			// ensure that updates are re-enabled
			this.EndUpdate();
		}


		/// <summary>
		/// MenuItem click event.  Perform the requested menu action
		/// and always force a filter rebuild since all these change
		/// the filtering properties in some manner.  The sender
		/// identifies the specific menuitem clicked.
		/// </summary>
		/// <param name="sender">MenuItem</param>
		/// <param name="e">Event</param>
		private void MenuItemClick(object sender, System.EventArgs e)
		{

			// 'Clear filter', set the Header.Filter for the column
			if (sender == mnu_clearf)
				mvarHeader.Filter[mnu_column] = "";

			// 'Ignore case', toggle the flag
			else if (sender == mnu_ignore)
				flt_ignore = !flt_ignore;

			// 'String', set the datatype comparison
			else if (sender == mnu_strflt)
				mvarHeader.DataType[mnu_column] = ListViewFilterDataType.String;

			// 'Number', set the datatype comparison
			else if (sender == mnu_nbrflt)
				mvarHeader.DataType[mnu_column] = ListViewFilterDataType.Number;

			// 'Date', set the datatype comparison
			else if (sender == mnu_datflt)
				mvarHeader.DataType[mnu_column] = ListViewFilterDataType.Date;

			// 'Left', set the alignment
			else if (sender == mnu_alignl)
				mvarHeader.Alignment[mnu_column] = HorizontalAlignment.Left;

			// 'Right', set the alignment
			else if (sender == mnu_alignr)
				mvarHeader.Alignment[mnu_column] = HorizontalAlignment.Right;

			// 'Center', set the alignment
			else if (sender == mnu_alignc)
				mvarHeader.Alignment[mnu_column] = HorizontalAlignment.Center;

			// unknown, ignore this type
			else return;

			// force a filter build on the specific column
			FilterBuild(mnu_column);

			// follow with a filter update
			FilterUpdate();

			// set focus to ourself after menu action otherwise
			// focus is left in the filter edit itself...
			this.Focus();

			// if this was an alignment change then we need to invalidate
			if (((MenuItem)sender).Parent == mnu_alignt) this.Invalidate();

		}


		/// <summary>
		/// Context menu popup event.  Set context menu item states
		/// for the current column data type and case sensitivity.
		/// </summary>
		/// <param name="sender">ContextMenu</param>
		/// <param name="e">Event</param>
		private void ContextMenuPopup(object sender, System.EventArgs e)
		{

			// set the correct radio menu item based upon the data type
			// NOTE: this is bad, .NET should know how to treat radio
			// items by using the WS_GROUP style in the control...
			switch (mvarHeader.DataType[mnu_column])
			{
				case ListViewFilterDataType.Date:
					mnu_strflt.Checked = false;
					mnu_nbrflt.Checked = false;
					mnu_datflt.Checked = true;
					break;
				case ListViewFilterDataType.Number:
					mnu_strflt.Checked = false;
					mnu_nbrflt.Checked = true;
					mnu_datflt.Checked = false;
					break;
				default:
					mnu_strflt.Checked = true;
					mnu_nbrflt.Checked = false;
					mnu_datflt.Checked = false;
					break;
			}

			// disable the checked DataType menu item
			mnu_strflt.Enabled = !mnu_strflt.Checked;
			mnu_nbrflt.Enabled = !mnu_nbrflt.Checked;
			mnu_datflt.Enabled = !mnu_datflt.Checked;

			// also set the current alignment
			switch (mvarHeader.Alignment[mnu_column])
			{
				case HorizontalAlignment.Center:
					mnu_alignl.Checked = false;
					mnu_alignr.Checked = false;
					mnu_alignc.Checked = true;
					break;
				case HorizontalAlignment.Right:
					mnu_alignl.Checked = false;
					mnu_alignr.Checked = true;
					mnu_alignc.Checked = false;
					break;
				default:
					mnu_alignl.Checked = true;
					mnu_alignr.Checked = false;
					mnu_alignc.Checked = false;
					break;
			}

			// disable the checked DataType menu item
			mnu_alignl.Enabled = !mnu_alignl.Checked;
			mnu_alignr.Enabled = !mnu_alignr.Checked;
			mnu_alignc.Enabled = !mnu_alignc.Checked;

			// set the checked state of the case insensitive
			mnu_ignore.Checked = flt_ignore;

		}


		#endregion

		/// <summary>
		/// Return the Text of an item/subitem as a string.  This is done to simplify text retrieval in multiple places.
		/// </summary>
		/// <param name="i">ListViewItem</param>
		/// <param name="c">Column number</param>
		/// <returns>Text or empty string</returns>
		public string GetItemText(ListViewItem i, int c)
		{
            // ensure we have an item to work with here...
            // if not valid item/subitem return empty string
            if (i == null) return String.Empty;

            if (c == 0)
            {
                // if column 0 return the text
                return i.Text;
            }
            else if ((c < i.SubItems.Count) && (i.SubItems[c] != null))
            {
                // not 0, ensure that the subitem is valid and exists
                return i.SubItems[c].Text;
            }
            return String.Empty;
		}

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            
            ListViewHitTestInfo lvhti = HitTest(e.Location);
            if (lvhti.Item != null)
            {
                OnItemDoubleClick(new ListViewItemDoubleClickEventArgs(lvhti.Item, lvhti.SubItem));
            }
        }

        public event ListViewItemDoubleClickEventHandler ItemDoubleClick;
        protected virtual void OnItemDoubleClick(ListViewItemDoubleClickEventArgs e)
        {
            if (ItemDoubleClick != null) ItemDoubleClick(this, e);
        }


        #region Internal methods and data

        /// <summary>
		/// Compare two strings and return a -/=/+ result.  This is
		/// used for all filter checks and column sorting.  The key
		/// is that we also use the DataType to correctly compare.
		/// </summary>
		/// <param name="s1">First string</param>
		/// <param name="s2">Second string</param>
		/// <param name="type">DataType for comparison</param>
		/// <returns>Less, Equal, Greater as -1,0,1</returns>
		internal int CompareData(string s1, string s2, ListViewFilterDataType type, bool size)
		{

			// perform the requested datatype comparison.  note that
			// we put the float and datetime in a try catch
			switch (type)
			{

				// string comparison is easy.  if the size is true, this
				// is a filter comparison and only x characters count.
				case ListViewFilterDataType.String:
					if (size && (s1.Length > s2.Length))
						return s1.Substring(0, s2.Length).CompareTo(s2);
					return s1.CompareTo(s2);

				// float requires parsing the data
				case ListViewFilterDataType.Number:
					try { cmp_float1 = float.Parse(s1); }
					catch { cmp_float1 = float.MaxValue; };
					try { cmp_float2 = float.Parse(s2); }
					catch { cmp_float2 = float.MaxValue; };
					return cmp_float1.CompareTo(cmp_float2);

				// date also requires a parse
				case ListViewFilterDataType.Date:
					try { cmp_datim1 = DateTime.Parse(s1); }
					catch { cmp_datim1 = DateTime.MaxValue; };
					try { cmp_datim2 = DateTime.Parse(s2); }
					catch { cmp_datim2 = DateTime.MaxValue; };
					return DateTime.Compare(cmp_datim1, cmp_datim2);

				// by default the strings are equal
				default:
					return 0;

			}
		}


		#endregion

		#region Public properties

		[Description("The ListViewFilterHeader instance for access to the Names, Sizes, DataType, and Filter entries by column."), Browsable(false)]
		public ListViewFilterHeader Header { get { return mvarHeader; } }

		[Description("The type of sorting associated with the current column that is being sorted."), Browsable(false)]
		public ListViewFilterDataType SortType { get { return mvarSortType; } }

		/// <summary>
		/// These are all browsable and available in the property pages.
		/// NOTE: I do not know how to do two things: hide the Sorting
		/// property from the page since it causes all kinds of problems,
		/// and how to set a DefaultValue( Color.WhiteSmoke ) for the
		/// ShadeColor property...
		/// </summary>

		[Description("In Details mode, the header will display filter controls."), Category("Behavior"), DefaultValue(false)]
        public bool Filtered
		{
			get { return mvarFiltered; }
			set
			{
                if (mvarFiltered != value)
                {
					mvarFiltered = value;
					if (mvarHeader != null) mvarHeader.Filtered = mvarFiltered;
					this.Refresh();
				}
			}
		}


		[Description("Treat filter strings case-insensitive for comparison."),
		Category("Behavior"), DefaultValue(false)]
		public bool IgnoreCase
		{
			get
			{
				return flt_ignore;
			}
			set
			{
				if (flt_ignore != value)
				{
					flt_ignore = value;
					FilterUpdate();
				}
			}
		}


		[Description("In Details mode, the sorted column will be shaded."),
		Category("Appearance"), DefaultValue(true)]
		public bool Shaded
		{
			get
			{
				return col_shaded;
			}
			set
			{
				if (col_shaded != value)
				{
					col_shaded = value;
					this.Refresh();
				}
			}
		}


		[Description("Color of the shaded column in details mode"),
		Category("Appearance")]
		public Color ShadeColor
		{
			get
			{
				return col_scolor;
			}
			set
			{
				col_scolor = value;
				this.Refresh();
			}
		}

		protected virtual void ResetShadeColor()
		{
			ShadeColor = Color.WhiteSmoke;
		}


		[Description("The sorted column when in details mode."),
		Category("Behavior"), DefaultValue(0)]
		public int SortColumn
		{
			get
			{
				return srt_column;
			}
			set
			{
				if ((srt_column != value) && (value < this.Columns.Count) &&
				  (mvarHeader != null))
				{
					srt_column = mvarHeader.SortColumn = value;
					if (this.View == View.Details) this.Sort();
				}
			}
		}


		[Description("The order of the current sort. True is ascending."),
		Category("Behavior"), DefaultValue(false), Browsable(false)]
		public bool SortOrder
		{
			get
			{
				return srt_sorder;
			}
			set
			{
				if ((srt_sorder != value) && (mvarHeader != null))
				{
					srt_sorder = mvarHeader.SortOrder = value;
					if (this.View == View.Details) this.Sort();
				}
			}
		}


		#endregion

	}


	/// <summary>
	/// This class encapsulates the standard HeaderControl that is not
	/// currently implemented in the .NET Framework.  This is specific
	/// for use with a ListViewFilter control in this assembly.  The
	/// primary reason for this is to get access to the Win32 HDM_
	/// messages to get/set values that are not published by the
	/// System.Windows.Forms.ColumnHeader class in .NET.
	/// NOTE: This control works very well in XP since it uses the
	/// HDF_SORTUP/HDF_SORTDN flags to display the up/down icon. But
	/// you must have a manifest that states using CommCtrl version
	/// 6.0 or greater, since that is where it is implemented...
	/// </summary>
	public class ListViewFilterHeader : System.Windows.Forms.NativeWindow
	{
		readonly ListView hdr_lstvew;                // owning listview control
		private bool hdr_filter = false;        // show filterbar in header
		private int hdr_column = -1;           // sort column
		private bool hdr_sorder = true;         // sort order
		private Internal.Windows.Structures.HDITEM hdr_hditem = new Internal.Windows.Structures.HDITEM(); // instance container

		/// <summary>
		/// Save the owning ListView control object and get our handle assigned from it's slave controls entry 0, without it no messages are sent.
		/// </summary>
		/// <param name="lv">Owner</param>
        public ListViewFilterHeader(ListView listview, bool filtered, int column, bool order)
        {
            // save the listview instance and set initial properties
            hdr_lstvew = listview;
            hdr_sorder = order;
            hdr_column = column;

            switch (System.Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
                    // set the handle to this control.  the first dialog item
                    // for a listview is this header control...
                    AssignHandle(Internal.Windows.Methods.GetDlgItem(hdr_lstvew.Handle, 0));
                    break;
                }
            }

            // create the collection properties
            DataType = new ColumnDataTypeCollection(this);
            Filter = new ColumnFilterCollection(this);
            Names = new ColumnNamesCollection(this);
            SizeInfo = new ColumnSizeInfoCollection(this);
            Alignment = new ColumnAlignmentCollection(this);

            // last set the filtered property VIA SET{} METHOD
            Filtered = filtered;
        }

		/// <summary>
		/// When the handle changes reset the filter style.
		/// </summary>
		protected override void OnHandleChange()
		{
			base.OnHandleChange();

			// reset the filter settings if there is a handle
			if (Handle != (IntPtr)0) ChangeFiltered();
		}

		/// <summary>
		/// Set the window style to reflect the value of the Filtered
		/// property.  This is done when the Handle or property change.  
		/// </summary>
		private void ChangeFiltered()
		{
            switch (System.Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
                    // we need to set a new style value for this control to turn
                    // on/off the HDS_FILTERBAR flag.  First get the style itself.
                    const int HDS_FILTR = (int)Internal.Windows.Constants.ListViewHeaderStyle.FilterBar;
                    int style = Internal.Windows.Methods.GetWindowLong(Handle, Internal.Windows.Constants.WindowLongType.Style);

                    // now that we have the flag see if it is not what is desired
                    if (((style & HDS_FILTR) != 0) != hdr_filter)
                    {

                        // set/reset the flag for the filterbar
                        if (hdr_filter) style |= HDS_FILTR;
                        else style ^= HDS_FILTR;
                        Internal.Windows.Methods.SetWindowLong(Handle, Internal.Windows.Constants.WindowLongType.Style, style);

                        // now we have to resize this control.  we do this by sending
                        // a set item message to column 0 to change it's size.  this
                        // is a kludge but the invalidate and others just don't work.
                        hdr_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Height;
                        Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemW, 0, ref hdr_hditem);
                        hdr_hditem.cxy += (hdr_filter) ? 1 : -1;
                        Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemW, 0, ref hdr_hditem);

                    }

                    UpdateFilterTimeout();

                    // it is necessary to clear all filters which will cause a 
                    // notification be sent to the ListView with -1 column.
                    Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.ClearFilter, -1, 0);
                    break;
                }
            }
		}

		private int mvarFilterChangeTimeout = 500;
		public int FilterChangeTimeout
		{
			get { return mvarFilterChangeTimeout; }
			set
			{
				mvarFilterChangeTimeout = value;
				UpdateFilterTimeout();
			}
		}

		private void UpdateFilterTimeout()
		{
			// it can't hurt to set the filter timeout limit to .5 seconds
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
                    Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetFilterChangeTimeout, 0, mvarFilterChangeTimeout);
                    break;
                }
            }
		}


		/// <summary>
		/// Change the column and order that is sorted.  If the column
		/// is the same as the current sort column, the order is 
		/// toggled.  If it is a different column then it always sets
		/// the order to ascending by default.
		/// </summary>
		/// <param name="Column"></param>
		private void ChangeSort(int c)
		{

			// save the new sort column
			hdr_column = c;

			// setup to walk the format setting for every column
			hdr_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Format;

			// turn off the sorted flag for all but the current column
			int i = Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemCount, 0, 0);
			while (i-- > 0)
			{

				// get the item information (HDITEM structure)
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                    {
                        Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemW, i, ref hdr_hditem);
                        break;
                    }
                }

				// is this the column we are to flag as sorted
				if (i == hdr_column)
				{
					// set true when the current mode is down or none set yet
					hdr_sorder = ((hdr_hditem.fmt & Internal.Windows.Constants.ListViewHeaderFormat.SortUp) == 0);

					// set the new sort mode in the format
					hdr_hditem.fmt &= Internal.Windows.Constants.ListViewHeaderFormat.NotSorted;
					hdr_hditem.fmt |= (hdr_sorder) ? Internal.Windows.Constants.ListViewHeaderFormat.SortUp : Internal.Windows.Constants.ListViewHeaderFormat.SortDown;
				}
				// not the sort column, turn off any existing sort flag
				else if ((hdr_hditem.fmt & Internal.Windows.Constants.ListViewHeaderFormat.Sorted) != 0)
				{
					hdr_hditem.fmt &= Internal.Windows.Constants.ListViewHeaderFormat.NotSorted;
				}
				else
				{
					// not the column or any sort flag, skip the format change
					continue;
				}

				// a change is needed to this column format so set it
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                    {
                        Internal.Windows.Methods.SendMessage(Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemW, i, ref hdr_hditem);
                        break;
                    }
                }

			}

			// finally invalidate the listview so that it redraws
			hdr_lstvew.Invalidate(hdr_lstvew.Bounds);

		}

		/// <summary>
		/// Indexer to the alignment by column.
		/// </summary>
		public readonly ColumnAlignmentCollection Alignment;

		/// <summary>
		/// Indexer to the data types by column.
		/// </summary>
		public readonly ColumnDataTypeCollection DataType;

		/// <summary>
		/// Indexer to the column filter text.
		/// </summary>
		public readonly ColumnFilterCollection Filter;

		/// <summary>
		/// Indexer to the column name text.
		/// </summary>
		public readonly ColumnNamesCollection Names;

		/// <summary>
		/// Indexer to the column size (Width and Left as Height).
		/// </summary>
		public readonly ColumnSizeInfoCollection SizeInfo;

		/// <summary>
		/// When the Filtered property changes update the window style.
		/// </summary>
		public bool Filtered
		{
			get
			{
				return hdr_filter;
			}
			set
			{
				if (hdr_filter != value)
				{
					hdr_filter = value;
					ChangeFiltered();
				}
			}
		}


		/// <summary>
		/// The column number sorted.
		/// </summary>
		public int SortColumn
		{
			get
			{
				return hdr_column;
			}
			set
			{
				hdr_column = value;
				ChangeSort(hdr_column);
			}
		}


		/// <summary>
		/// Sort order (true is ascending).
		/// </summary>
		public bool SortOrder
		{
			get
			{
				return hdr_sorder;
			}
			set
			{
				if (hdr_sorder != value)
				{
					hdr_sorder = value;
					ChangeSort(hdr_column);
				}
			}
		}

		/// <summary>
		/// This is an indexer to the LVDataType values for each column.
		/// The data is stored in the lParam value of the column.
		/// </summary>
		public class ColumnAlignmentCollection
		{
			readonly ListViewFilterHeader col_hdrctl = null;         // owning header control
			private Internal.Windows.Structures.HDITEM col_hditem = new Internal.Windows.Structures.HDITEM(); // HDITEM instance

			/// <summary>
			/// Constructor this must be given the header instance for access
			/// to the Handle property so that messages can be sent to it.
			/// </summary>
			/// <param name="header">HeaderControl</param>
			public ColumnAlignmentCollection(ListViewFilterHeader header)
			{
				col_hdrctl = header;
			}


			/// <summary>
			/// Indexer method to get/set the Alignment for the column.
			/// </summary>
			public HorizontalAlignment this[int index]
			{
				get
				{
					// ensure that this is a valid column
					if (index >= this.Count) return HorizontalAlignment.Left;

					// get the current format for the column
					col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Format;

                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemW, index, ref col_hditem);
                            break;
                        }
                    }

					// return the current setting
					if ((col_hditem.fmt & Internal.Windows.Constants.ListViewHeaderFormat.Center) != 0)
					{
						return HorizontalAlignment.Center;
					}
					else if ((col_hditem.fmt & Internal.Windows.Constants.ListViewHeaderFormat.Right) != 0)
					{
						return HorizontalAlignment.Right;
					}
					else
					{
						return HorizontalAlignment.Left;
					}
				}
				set
				{

					// ensure that this is a valid column
					if (index < this.Count)
					{

						// get the current format for the column
						col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Format;
                        
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemW, index, ref col_hditem);
                                break;
                            }
                        }

						// turn off any existing alignment values
						col_hditem.fmt &= Internal.Windows.Constants.ListViewHeaderFormat.NotJustified;

						// turn on the correct alignment
						switch (value)
						{
							case HorizontalAlignment.Center:
                            {
								col_hditem.fmt |= Internal.Windows.Constants.ListViewHeaderFormat.Center;
								break;
                            }
							case HorizontalAlignment.Right:
                            {
							    col_hditem.fmt |= Internal.Windows.Constants.ListViewHeaderFormat.Right;
							    break;
                            }
							default:
                            {
							    col_hditem.fmt |= Internal.Windows.Constants.ListViewHeaderFormat.Left;
							    break;
                            }
						}

						// now update the column format
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemW, index, ref col_hditem);
                                break;
                            }
                        }
					}
				}
			}


			/// <summary>
			/// Return the number of columns in the header.
			/// </summary>
			public int Count
			{
				get
				{
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            return Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemCount, 0, 0);
                        }
                    }
                    return 0;
				}
			}

		}


		/// <summary>
		/// This is an indexer to the LVDataType values for each column.
		/// The data is stored in the lParam value of the column.
		/// </summary>
		public class ColumnDataTypeCollection
		{
			readonly ListViewFilterHeader col_hdrctl = null;         // owning header control
			private Internal.Windows.Structures.HDITEM col_hditem = new Internal.Windows.Structures.HDITEM(); // HDITEM instance

			/// <summary>
			/// Constructor this must be given the header instance for access
			/// to the Handle property so that messages can be sent to it.
			/// </summary>
			/// <param name="header">HeaderControl</param>
			public ColumnDataTypeCollection(ListViewFilterHeader header)
			{
				col_hdrctl = header;
			}


			/// <summary>
			/// Indexer method to get/set the LVDataType for the column.
			/// </summary>
			public ListViewFilterDataType this[int index]
			{
				get
				{
					// the lparam of the column header contains the datatype
					col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.LParam;
					col_hditem.lParam = (int)ListViewFilterDataType.String;

					// if it is valid the lparam is updated and then returned
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemW, index, ref col_hditem);
                            break;
                        }
                    }
					return (ListViewFilterDataType)col_hditem.lParam;
				}
				set
				{
					// ensure that this is a valid column
					if (index < this.Count)
					{
						// simply set the new LVDataType in the lparam and pass it on
						col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.LParam;
						col_hditem.lParam = (int)value;
						
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemW, index, ref col_hditem);
                                break;
                            }
                        }
					}
				}
			}


			/// <summary>
			/// Return the number of columns in the header.
			/// </summary>
			public int Count
			{
				get
				{
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            return Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemCount, 0, 0);
                        }
                    }
                    return 0;
				}
			}

		}


		/// <summary>
		/// This is an indexer to the text values for each column.
		/// </summary>
		public class ColumnNamesCollection
		{
			readonly ListViewFilterHeader col_hdrctl = null;         // owning header control
			private Internal.Windows.Structures.HDITEM col_hditem = new Internal.Windows.Structures.HDITEM(); // HDITEM instance

			/// <summary>
			/// Constructor this must be given the header instance for access
			/// to the Handle property so that messages can be sent to it.
			/// </summary>
			/// <param name="header">HeaderControl</param>
			public ColumnNamesCollection(ListViewFilterHeader header)
			{
				col_hdrctl = header;
			}

			/// <summary>
			/// Indexer method to get/set the text of the column.
			/// </summary>
			public string this[int index]
			{
				get
				{
					// set up to retrive the column header text
					col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Text;
					col_hditem.pszText = new string(new char[64]);
					col_hditem.cchTextMax = col_hditem.pszText.Length;

					// if successful the text has been retrieved and returned
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemA, index, ref col_hditem);
                            break;
                        }
                    }
					return col_hditem.pszText;
				}
				set
				{

					// this must be a valid column index
					if (index < this.Count)
					{
						// simply set the text and size in the structure and pass it on
						col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Text;
						col_hditem.pszText = value;
						col_hditem.cchTextMax = col_hditem.pszText.Length;
						
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemA, index, ref col_hditem);
                                break;
                            }
                        }
					}
				}
			}

			/// <summary>
			/// Return the number of columns in the header.
			/// </summary>
			public int Count
			{
				get
				{
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            return Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemCount, 0, 0);
                        }
                    }
                    return 0;
				}
			}
		}


		/// <summary>
		/// This is an indexer to the filter values for each column.  This
		/// is the most complex access to the HDITEM data since the filter
		/// data must be get/set via the HDTEXTFILTER structure referenced
		/// in the HDITEM structure.  It is simple to Marshall this, but
		/// figuring that out in the first place took alot of effort.
		/// </summary>
		public class ColumnFilterCollection
		{
			readonly ListViewFilterHeader col_hdrctl = null;               // owning header control
			private Internal.Windows.Structures.HDITEM col_hditem = new Internal.Windows.Structures.HDITEM();       // HDITEM instance
			private Internal.Windows.Structures.HDTEXTFILTER col_txtflt = new Internal.Windows.Structures.HDTEXTFILTER(); // HDTEXTFILTER instance

			/// <summary>
			/// Constructor this must be given the header instance for access
			/// to the Handle property so that messages can be sent to it.
			/// </summary>
			/// <param name="header">HeaderControl</param>
			public ColumnFilterCollection(ListViewFilterHeader header)
			{
				col_hdrctl = header;
			}


			/// <summary>
			/// Indexer method to get/set the filter text for the column.
			/// </summary>
			public string this[int index]
			{
				get
				{

					// if the column is invalid return nothing
					if (index >= Count) return "";

					// this is tricky since it involves marshalling pointers
					// to structures that are used as a reference in another
					// structure.  first initialize the receiving HDTEXTFILTER
					col_txtflt.pszText = new string(new char[64]);
					col_txtflt.cchTextMax = col_txtflt.pszText.Length;

					// set the HDITEM up to request the current filter content
					col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Filter;
					col_hditem.type = (uint)Internal.Windows.Constants.ListViewHeaderFilterType.String;

					// marshall memory big enough to contain a HDTEXTFILTER 
					col_hditem.pvFilter = Marshal.AllocCoTaskMem(
					  Marshal.SizeOf(col_txtflt));

					// now copy the HDTEXTFILTER structure to the marshalled memory
					Marshal.StructureToPtr(col_txtflt, col_hditem.pvFilter, false);

					// retrieve the header filter string as non-wide string
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemA, index, ref col_hditem);
                            break;
                        }
                    }

					// un-marshall the memory back into the HDTEXTFILTER structure
					col_txtflt = (Internal.Windows.Structures.HDTEXTFILTER)Marshal.PtrToStructure(
					  col_hditem.pvFilter, typeof(Internal.Windows.Structures.HDTEXTFILTER));

					// remember to free the marshalled IntPtr memory...
					Marshal.FreeCoTaskMem(col_hditem.pvFilter);

					// return the string in the text filter area
					return col_txtflt.pszText;

				}
				set
				{

					// ensure that the column exists before attempting this
					if (index < this.Count)
					{

						// this is just like the get{} except we don't have to
						// return anything and the message is HDM_SETITEMA. we
						// use the non-unicode methods for both the get and set.
						// reference the get{} method for marshalling details.
						col_txtflt.pszText = value;
						col_txtflt.cchTextMax = 64;
						col_hditem.mask = Internal.Windows.Constants.ListViewHeaderMask.Filter;
						col_hditem.type = (uint)Internal.Windows.Constants.ListViewHeaderFilterType.String;
						col_hditem.pvFilter = Marshal.AllocCoTaskMem(Marshal.SizeOf(col_txtflt));
						Marshal.StructureToPtr(col_txtflt, col_hditem.pvFilter, false);
						
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.SetItemA, index, ref col_hditem);
                                break;
                            }
                        }
						Marshal.FreeCoTaskMem(col_hditem.pvFilter);
					}
				}
			}


			/// <summary>
			/// Return the number of columns in the header.
			/// </summary>
			public int Count
			{
				get
				{
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            return Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemCount, 0, 0);
                        }
                    }
                    return 0;
				}
			}


		}


		/// <summary>
		/// This is an indexer to the READONLY Size values for a column.
		/// NOTE: The Size really contains the Width and Left position,
		/// the Left is sorted in the Height property of the Size class
		/// We do this because a Rectangle is not really necessary.
		/// </summary>
		public class ColumnSizeInfoCollection
		{
			readonly ListViewFilterHeader col_hdrctl = null;       // owning header control
			private Internal.Windows.Structures.RECT col_rectng = new Internal.Windows.Structures.RECT(); // HDITEM instance

			/// <summary>
			/// Constructor this must be given the header instance for access
			/// to the Handle property so that messages can be sent to it.
			/// </summary>
			/// <param name="header">HeaderControl</param>
			public ColumnSizeInfoCollection(ListViewFilterHeader header)
			{
				col_hdrctl = header;
			}


			/// <summary>
			/// Indexer method to get/set the Size for the column.
			/// </summary>
			public Size this[int index]
			{
				get
				{

					// if the column is valid get the rectangle
					if (index < Count)
					{
                        switch (Environment.OSVersion.Platform)
                        {
                            case PlatformID.Win32NT:
                            case PlatformID.Win32S:
                            case PlatformID.Win32Windows:
                            case PlatformID.WinCE:
                            {
                                Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewHeaderMessage.GetItemRect, index, ref col_rectng);
                                break;
                            }
                        }
						return new Size((col_rectng.right - col_rectng.left), col_rectng.left);
					}
					else
                    {
					    // return null size
                        return new Size(0, 0);
                    }
				}
			}


			/// <summary>
			/// Return the number of columns in the header.
			/// </summary>
			public int Count
			{
				get
				{
                    switch (Environment.OSVersion.Platform)
                    {
                        case PlatformID.Win32NT:
                        case PlatformID.Win32S:
                        case PlatformID.Win32Windows:
                        case PlatformID.WinCE:
                        {
                            return Internal.Windows.Methods.SendMessage(col_hdrctl.Handle, Internal.Windows.Constants.ListViewMessage.GetItemCount, 0, 0);
                        }
                    }
                    return 0;
				}
			}

		}

	}


	/// <summary>
	/// This class is used to compare ListViewItem entries by column
	/// in a particular manner for sorting the by owning ListView.
	/// The standard IComparer is extended with the Compare method
	/// </summary>
	internal class ListViewFilterSorter : IComparer
	{
		readonly ListView cmp_lstvew = null; // owning listview

		/// <summary>
		/// Constructor, we need the instance of the ListViewFilter.
		/// </summary>
		/// <param name="listview">Owner</param>
		public ListViewFilterSorter(ListView listview)
		{
			cmp_lstvew = listview;
		}

		/// <summary>
		/// Compare function for two given objects.  We use the
		/// internal ItemText and CompareData methods to do the
		/// work of getting the strings.  The listview also has
		/// the column, order, and datatype needed.
		/// </summary>
		/// <param name="o1">First object</param>
		/// <param name="o2">Second object</param>
		/// <returns>-1/0/1 result of comparison</returns>
		public int Compare(object o1, object o2)
		{

			// initialize rezult to equal objects
			int rz = 0;

			// we can do nothing without a listview and two objects
			if ((o1 != null) && (o2 != null))
			{

				// get the item/subitem text for both strings
				string s1 = cmp_lstvew.GetItemText((ListViewItem)o1, cmp_lstvew.SortColumn);
				string s2 = cmp_lstvew.GetItemText((ListViewItem)o2, cmp_lstvew.SortColumn);

				// compare the two strings using the data type
				rz = cmp_lstvew.CompareData(s1, s2, cmp_lstvew.SortType, false);

			}

			// return +/- result depending upon sort order
			return (cmp_lstvew.SortOrder) ? rz : (rz * -1);
		}
	}
}
