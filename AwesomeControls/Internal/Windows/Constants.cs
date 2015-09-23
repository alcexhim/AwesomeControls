using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeControls.Internal.Windows
{
	public static class Constants
	{
		public const int HTCAPTION = 2;
		public enum ListViewMessage : int
		{
			First = 0x1000,
			GetBackgroundColor = (First + 0),
			SetBackgroundColor = (First + 1),
			GetImageList = (First + 2),
			SetImageList = (First + 3),
			GetItemCount = (First + 4),
			LVM_GETITEMA = (First + 5),
			LVM_GETITEMW = (First + 75),
			LVM_SETITEMA = (First + 6),
			LVM_SETITEMW = (First + 76),
			LVM_INSERTITEMA = (First + 7),
			LVM_INSERTITEMW = (First + 77),
			LVM_DELETEITEM = (First + 8),
			LVM_DELETEALLITEMS = (First + 9),
			LVM_GETCALLBACKMASK = (First + 10),
			LVM_SETCALLBACKMASK = (First + 11),
			LVM_GETNEXTITEM = (First + 12),
			LVM_FINDITEMA = (First + 13),
			LVM_FINDITEMW = (First + 83),
			LVM_GETITEMRECT = (First + 14),
			LVM_SETITEMPOSITION = (First + 15),
			LVM_GETITEMPOSITION = (First + 16),
			LVM_GETSTRINGWIDTHA = (First + 17),
			LVM_GETSTRINGWIDTHW = (First + 87),
			LVM_HITTEST = (First + 18),
			LVM_ENSUREVISIBLE = (First + 19),
			LVM_SCROLL = (First + 20),
			LVM_REDRAWITEMS = (First + 21),
			LVM_ARRANGE = (First + 22),
			LVM_EDITLABELA = (First + 23),
			LVM_EDITLABELW = (First + 118),
			LVM_GETEDITCONTROL = (First + 24),
			LVM_GETCOLUMNA = (First + 25),
			LVM_GETCOLUMNW = (First + 95),
			LVM_SETCOLUMNA = (First + 26),
			LVM_SETCOLUMNW = (First + 96),
			LVM_INSERTCOLUMNA = (First + 27),
			LVM_INSERTCOLUMNW = (First + 97),
			LVM_DELETECOLUMN = (First + 28),
			LVM_GETCOLUMNWIDTH = (First + 29),
			LVM_SETCOLUMNWIDTH = (First + 30),
			LVM_GETHEADER = (First + 31),
			LVM_CREATEDRAGIMAGE = (First + 33),
			LVM_GETVIEWRECT = (First + 34),
			LVM_GETTEXTCOLOR = (First + 35),
			LVM_SETTEXTCOLOR = (First + 36),
			LVM_GETTEXTBKCOLOR = (First + 37),
			LVM_SETTEXTBKCOLOR = (First + 38),
			LVM_GETTOPINDEX = (First + 39),
			LVM_GETCOUNTPERPAGE = (First + 40),
			LVM_GETORIGIN = (First + 41),
			LVM_UPDATE = (First + 42),
			LVM_SETITEMSTATE = (First + 43),
			LVM_GETITEMSTATE = (First + 44),
			LVM_GETITEMTEXTA = (First + 45),
			LVM_GETITEMTEXTW = (First + 115),
			LVM_SETITEMTEXTA = (First + 46),
			LVM_SETITEMTEXTW = (First + 116),
			LVM_SETITEMCOUNT = (First + 47),
			LVM_SORTITEMS = (First + 48),
			LVM_SETITEMPOSITION32 = (First + 49),
			LVM_GETSELECTEDCOUNT = (First + 50),
			LVM_GETITEMSPACING = (First + 51),
			LVM_GETISEARCHSTRINGA = (First + 52),
			LVM_GETISEARCHSTRINGW = (First + 117),
			LVM_SETICONSPACING = (First + 53),
			LVM_SETEXTENDEDLISTVIEWSTYLE = (First + 54),
			LVM_GETEXTENDEDLISTVIEWSTYLE = (First + 55),
			LVM_GETSUBITEMRECT = (First + 56),
			LVM_SUBITEMHITTEST = (First + 57),
			LVM_SETCOLUMNORDERARRAY = (First + 58),
			LVM_GETCOLUMNORDERARRAY = (First + 59),
			LVM_SETHOTITEM = (First + 60),
			LVM_GETHOTITEM = (First + 61),
			LVM_SETHOTCURSOR = (First + 62),
			LVM_GETHOTCURSOR = (First + 63),
			LVM_APPROXIMATEVIEWRECT = (First + 64),
			LVM_SETWORKAREAS = (First + 65),
			LVM_GETWORKAREAS = (First + 70),
			LVM_GETNUMBEROFWORKAREAS = (First + 73),
			LVM_GETSELECTIONMARK = (First + 66),
			LVM_SETSELECTIONMARK = (First + 67),
			LVM_SETHOVERTIME = (First + 71),
			LVM_GETHOVERTIME = (First + 72),
			LVM_SETTOOLTIPS = (First + 74),
			LVM_GETTOOLTIPS = (First + 78),
			LVM_SORTITEMSEX = (First + 81),
			LVM_SETBKIMAGEA = (First + 68),
			LVM_SETBKIMAGEW = (First + 138),
			LVM_GETBKIMAGEA = (First + 69),
			LVM_GETBKIMAGEW = (First + 139),
			LVM_SETSELECTEDCOLUMN = (First + 140),
			LVM_SETTILEWIDTH = (First + 141),
			LVM_SETVIEW = (First + 142),
			LVM_GETVIEW = (First + 143),
			LVM_INSERTGROUP = (First + 145),
			LVM_SETGROUPINFO = (First + 147),
			LVM_GETGROUPINFO = (First + 149),
			LVM_REMOVEGROUP = (First + 150),
			LVM_MOVEGROUP = (First + 151),
			LVM_MOVEITEMTOGROUP = (First + 154),
			LVM_SETGROUPMETRICS = (First + 155),
			LVM_GETGROUPMETRICS = (First + 156),
			LVM_ENABLEGROUPVIEW = (First + 157),
			LVM_SORTGROUPS = (First + 158),
			LVM_INSERTGROUPSORTED = (First + 159),
			LVM_REMOVEALLGROUPS = (First + 160),
			LVM_HASGROUP = (First + 161),
			LVM_SETTILEVIEWINFO = (First + 162),
			LVM_GETTILEVIEWINFO = (First + 163),
			LVM_SETTILEINFO = (First + 164),
			LVM_GETTILEINFO = (First + 165),
			LVM_SETINSERTMARK = (First + 166),
			LVM_GETINSERTMARK = (First + 167),
			LVM_INSERTMARKHITTEST = (First + 168),
			LVM_GETINSERTMARKRECT = (First + 169),
			LVM_SETINSERTMARKCOLOR = (First + 170),
			LVM_GETINSERTMARKCOLOR = (First + 171),
			LVM_SETINFOTIP = (First + 173),
			LVM_GETSELECTEDCOLUMN = (First + 174),
			LVM_ISGROUPVIEWENABLED = (First + 175),
			LVM_GETOUTLINECOLOR = (First + 176),
			LVM_SETOUTLINECOLOR = (First + 177),
			LVM_CANCELEDITLABEL = (First + 179),
			LVM_MAPINDEXTOID = (First + 180),
			LVM_MAPIDTOINDEX = (First + 181)
		}
		public enum ListViewItemMask
		{
			Text = 0x0001,
			Image = 0x0002,
			Param = 0x0004,
			State = 0x0008,
			Indent = 0x0010,
			NoRecompute = 0x0800
		}
		public enum ListViewHeaderMask
		{
			Width = 0x0001,
			Height = Width,
			Text = 0x0002,
			Format = 0x0004,
			LParam = 0x0008,
			Bitmap = 0x0010,
			Image = 0x0020,
			DISetItem = 0x0040,
			Order = 0x0080,
			Filter = 0x0100
		}
		public enum ListViewHeaderFormat
		{
			Left = 0x0000,
			Right = 0x0001,
			Center = 0x0002,
			Justified = 0x0003,
			NotJustified = 0xFFFC,
			RtlReading = 0x0004,
			SortDown = 0x0200,
			SortUp = 0x0400,
			Sorted = 0x0600,
			NotSorted = 0xF1FF,
			Image = 0x0800,
			BitmapOnRight = 0x1000,
			Bitmap = 0x2000,
			String = 0x4000,
			OwnerDraw = 0x8000
		}
		public enum CustomDrawReturnValue
		{
			Default = 0x0000,
			NewFont = 0x0002,
			SkipDefault = 0x0004,
			NotifyPostPaint = 0x0010,
			NotifyItemDraw = 0x0020,
			NotifySubItemDraw = 0x0020,
			NotifyPostErase = 0x0040
		}
		public enum CustomDrawDrawStage
		{
			PrePaint = 0x00000001,
			PostPaint = 0x00000002,
			PreErase = 0x00000003,
			PostErase = 0x00000004,
			Item = 0x00010000,
			ItemPrePaint = (Item | PrePaint),
			ItemPostPaint = (Item | PostPaint),
			ItemPreErase = (Item | PreErase),
			ItemPostErase = (Item | PostErase),
			SubItem = 0x00020000,
			SubItemPrePaint = (SubItem | ItemPrePaint),
			SubItemPostPaint = (SubItem | ItemPostPaint),
			SubItemPreErase = (SubItem | ItemPreErase),
			SubItemPostErase = (SubItem | ItemPostErase)
		}
		public enum WindowMessage
		{
			Null = 0x0000,
			Create = 0x0001,
			Destroy = 0x0002,
			Move = 0x0003,
			Size = 0x0005,
			Activate = 0x0006,
			SetFocus = 0x0007,
			KillFocus = 0x0008,
			Enable = 0x000A,
			SetRedraw = 0x000B,
			SetText = 0x000C,
			GetText = 0x000D,
			GetTextLength = 0x000E,
			Paint = 0x000F,
			Close = 0x0010,
			QueryEndSession = 0x0011,
			Quit = 0x0012,
			QueryOpen = 0x0013,
			EraseBackground = 0x0014,
			SystemColorsChanged = 0x0015,
			EndSession = 0x0016,
			ShowWindow = 0x0018,
			CtlColor = 0x0019,
			WinINIChange = 0x001A,
			SettingsChanged = 0x001A,
			DeviceModeChanged = 0x001B,
			ActivateApp = 0x001C,
			FontChanged = 0x001D,
			TimeChanged = 0x001E,
			CancelMode = 0x001F,
			SetCursor = 0x0020,
			MouseActivate = 0x0021,
			ChildActivate = 0x0022,
			QueueSync = 0x0023,
			WM_GETMINMAXINFO = 0x0024,
			WM_PAINTICON = 0x0026,
			WM_ICONERASEBKGND = 0x0027,
			WM_NEXTDLGCTL = 0x0028,
			WM_SPOOLERSTATUS = 0x002A,
			WM_DRAWITEM = 0x002B,
			WM_MEASUREITEM = 0x002C,
			WM_DELETEITEM = 0x002D,
			WM_VKEYTOITEM = 0x002E,
			WM_CHARTOITEM = 0x002F,
			WM_SETFONT = 0x0030,
			WM_GETFONT = 0x0031,
			WM_SETHOTKEY = 0x0032,
			WM_GETHOTKEY = 0x0033,
			WM_QUERYDRAGICON = 0x0037,
			WM_COMPAREITEM = 0x0039,
			WM_GETOBJECT = 0x003D,
			WM_COMPACTING = 0x0041,
			WM_COMMNOTIFY = 0x0044,
			WM_WINDOWPOSCHANGING = 0x0046,
			WM_WINDOWPOSCHANGED = 0x0047,
			WM_POWER = 0x0048,
			WM_COPYDATA = 0x004A,
			WM_CANCELJOURNAL = 0x004B,
			Notify = 0x004E,
			WM_INPUTLANGCHANGEREQUEST = 0x0050,
			WM_INPUTLANGCHANGE = 0x0051,
			WM_TCARD = 0x0052,
			WM_HELP = 0x0053,
			WM_USERCHANGED = 0x0054,
			WM_NOTIFYFORMAT = 0x0055,
			WM_CONTEXTMENU = 0x007B,
			WM_STYLECHANGING = 0x007C,
			WM_STYLECHANGED = 0x007D,
			WM_DISPLAYCHANGE = 0x007E,
			WM_GETICON = 0x007F,
			WM_SETICON = 0x0080,
			WM_NCCREATE = 0x0081,
			WM_NCDESTROY = 0x0082,
			WM_NCCALCSIZE = 0x0083,
			WM_NCHITTEST = 0x0084,
			NonClientPaint = 0x0085,
			NonClientActivate = 0x0086,
			WM_GETDLGCODE = 0x0087,
			WM_SYNCPAINT = 0x0088,
			WM_NCMOUSEMOVE = 0x00A0,
			WM_NCLBUTTONDOWN = 0x00A1,
			WM_NCLBUTTONUP = 0x00A2,
			WM_NCLBUTTONDBLCLK = 0x00A3,
			WM_NCRBUTTONDOWN = 0x00A4,
			WM_NCRBUTTONUP = 0x00A5,
			WM_NCRBUTTONDBLCLK = 0x00A6,
			WM_NCMBUTTONDOWN = 0x00A7,
			WM_NCMBUTTONUP = 0x00A8,
			WM_NCMBUTTONDBLCLK = 0x00A9,
			WM_KEYDOWN = 0x0100,
			WM_KEYUP = 0x0101,
			WM_CHAR = 0x0102,
			WM_DEADCHAR = 0x0103,
			WM_SYSKEYDOWN = 0x0104,
			WM_SYSKEYUP = 0x0105,
			WM_SYSCHAR = 0x0106,
			WM_SYSDEADCHAR = 0x0107,
			WM_KEYLAST = 0x0108,
			WM_IME_STARTCOMPOSITION = 0x010D,
			WM_IME_ENDCOMPOSITION = 0x010E,
			WM_IME_COMPOSITION = 0x010F,
			WM_IME_KEYLAST = 0x010F,
			WM_INITDIALOG = 0x0110,
			Command = 0x0111,
			SystemCommand = 0x0112,
			Timer = 0x0113,
			HorizontalScroll = 0x0114,
			VerticalScroll = 0x0115,
			InitializeMenu = 0x0116,
			InitializeMenuPopup = 0x0117,
			MenuSelect = 0x011F,
			MenuChar = 0x0120,
			EnterIdle = 0x0121,
			WM_MENURBUTTONUP = 0x0122,
			WM_MENUDRAG = 0x0123,
			WM_MENUGETOBJECT = 0x0124,
			WM_UNINITMENUPOPUP = 0x0125,
			WM_MENUCOMMAND = 0x0126,
			ColorControl = 0x0019,
			ColorMessageBox = 0x0132,
			ColorEdit = 0x0133,
			ColorListBox = 0x0134,
			ColorButton = 0x0135,
			ColorDialog = 0x0136,
			ColorScrollbar = 0x0137,
			ColorStatic = 0x0138,
			WM_MOUSEMOVE = 0x0200,
			WM_LBUTTONDOWN = 0x0201,
			WM_LBUTTONUP = 0x0202,
			WM_LBUTTONDBLCLK = 0x0203,
			WM_RBUTTONDOWN = 0x0204,
			WM_RBUTTONUP = 0x0205,
			WM_RBUTTONDBLCLK = 0x0206,
			WM_MBUTTONDOWN = 0x0207,
			WM_MBUTTONUP = 0x0208,
			WM_MBUTTONDBLCLK = 0x0209,
			WM_MOUSEWHEEL = 0x020A,
			WM_PARENTNOTIFY = 0x0210,
			WM_ENTERMENULOOP = 0x0211,
			WM_EXITMENULOOP = 0x0212,
			WM_NEXTMENU = 0x0213,
			WM_SIZING = 0x0214,
			WM_CAPTURECHANGED = 0x0215,
			WM_MOVING = 0x0216,
			WM_DEVICECHANGE = 0x0219,
			WM_MDICREATE = 0x0220,
			WM_MDIDESTROY = 0x0221,
			WM_MDIACTIVATE = 0x0222,
			WM_MDIRESTORE = 0x0223,
			WM_MDINEXT = 0x0224,
			WM_MDIMAXIMIZE = 0x0225,
			WM_MDITILE = 0x0226,
			WM_MDICASCADE = 0x0227,
			WM_MDIICONARRANGE = 0x0228,
			WM_MDIGETACTIVE = 0x0229,
			WM_MDISETMENU = 0x0230,
			WM_ENTERSIZEMOVE = 0x0231,
			WM_EXITSIZEMOVE = 0x0232,
			WM_DROPFILES = 0x0233,
			WM_MDIREFRESHMENU = 0x0234,
			WM_IME_SETCONTEXT = 0x0281,
			WM_IME_NOTIFY = 0x0282,
			WM_IME_CONTROL = 0x0283,
			WM_IME_COMPOSITIONFULL = 0x0284,
			WM_IME_SELECT = 0x0285,
			WM_IME_CHAR = 0x0286,
			WM_IME_REQUEST = 0x0288,
			WM_IME_KEYDOWN = 0x0290,
			WM_IME_KEYUP = 0x0291,
			WM_MOUSEHOVER = 0x02A1,
			WM_MOUSELEAVE = 0x02A3,
			WM_CUT = 0x0300,
			WM_COPY = 0x0301,
			WM_PASTE = 0x0302,
			WM_CLEAR = 0x0303,
			WM_UNDO = 0x0304,
			WM_RENDERFORMAT = 0x0305,
			WM_RENDERALLFORMATS = 0x0306,
			WM_DESTROYCLIPBOARD = 0x0307,
			WM_DRAWCLIPBOARD = 0x0308,
			WM_PAINTCLIPBOARD = 0x0309,
			WM_VSCROLLCLIPBOARD = 0x030A,
			SizeClipboard = 0x030B,
			AskCBFormatName = 0x030C,
			ChangeCBChain = 0x030D,
			HScrollClipboard = 0x030E,
			QueryNewPalette = 0x030F,
			PaletteChanging = 0x0310,
			PaletteChanged = 0x0311,
			HotKey = 0x0312,
			Print = 0x0317,
			PrintClient = 0x0318,
			HandheldFirst = 0x0358,
			HandheldLast = 0x035F,
			AfxFirst = 0x0360,
			AfxLast = 0x037F,
			PenWinFirst = 0x0380,
			PenWinLast  = 0x038F,
			App = 0x8000,
			User = 0x0400,
			Reflect = User + 0x1c00
		}
		public enum ReflectedMessage
		{
			Base = WindowMessage.Reflect,
			Command = (Base + WindowMessage.Command),
			ColorButton = (Base + WindowMessage.ColorButton),
			ColorEdit = (Base + WindowMessage.ColorEdit),
			ColorDialog = (Base + WindowMessage.ColorDialog),
			ColorListBox = (Base + WindowMessage.ColorListBox),
			ColorMessageBox = (Base + WindowMessage.ColorMessageBox),
			ColorScrollbar = (Base + WindowMessage.ColorScrollbar),
			ColorStatic = (Base + WindowMessage.ColorStatic),
			ColorControl = (Base + WindowMessage.ColorControl),
			DrawItem = (Base + WindowMessage.WM_DRAWITEM),
			MeasureItem = (Base + WindowMessage.WM_MEASUREITEM),
			DeleteItem = (Base + WindowMessage.WM_DELETEITEM),
			VKeyToItem = (Base + WindowMessage.WM_VKEYTOITEM),
			CharToItem = (Base + WindowMessage.WM_CHARTOITEM),
			CompareItem = (Base + WindowMessage.WM_COMPAREITEM),
			HorizontalScroll = (Base + WindowMessage.HorizontalScroll),
			VerticalScroll = (Base + WindowMessage.VerticalScroll),
			ParentNotify = (Base + WindowMessage.WM_PARENTNOTIFY),
			Notify = (Base + WindowMessage.Notify)
		}
		public enum NotifyMessage
		{
			First = 0x0000,
			OutOfMemory = (First - 1),
			Click = (First - 2),
			DoubleClick = (First - 3),
			Return = (First - 4),
			RightClick = (First - 5),
			RightDoubleClick = (First - 6),
			SetFocus = (First - 7),
			KillFocus = (First - 8),
			CustomDraw = (First - 12),
			Hover = (First - 13),
			NonClientHitTest = (First - 14),
			KeyDown = (First - 15),
			ReleasedCapture = (First - 16),
			SetCursor = (First - 17),
			Char = (First - 18),
			TooltipsCreated = (First - 19),
			LDown = (First - 20),
			RDown = (First - 21),
			ThemeChanged = (First - 22)
		}
		public enum WindowLongType
		{
			WindowProcedure = (-4),
			InstanceHandle = (-6),
			ParentHandle = (-8),
			Style = (-16),
			ExtendedStyle = (-20),
			UserData = (-21),
			ID = (-12)
		}

		public enum ListViewHeaderMessage
		{
			First = 0x1200,
			GetItemCount = (First + 0),
			InsertItemA = (First + 1),
			DeleteItem = (First + 2),
			GetItemA = (First + 3),
			SetItemA = (First + 4),
			Layout = (First + 5),
			HitTest = (First + 6),
			GetItemRect = (First + 7),
			SetImageList = (First + 8),
			GetImageList = (First + 9),
			InsertItemW = (First + 10),
			GetItemW = (First + 11),
			SetItemW = (First + 12),
			OrderToIndex = (First + 15),
			CreateDragImage = (First + 16),
			GetOrderArray = (First + 17),
			SetOrderArray = (First + 18),
			SetHotDivider = (First + 19),
			SetBitmapMargin = (First + 20),
			GetBitmapMargin = (First + 21),
			SetFilterChangeTimeout = (First + 22),
			EditFilter = (First + 23),
			ClearFilter = (First + 24)
		}
		public enum ListViewHeaderStyle
		{
			Horizontal = 0x0000,
			Buttons = 0x0002,
			HotTracking = 0x0004,
			Hidden = 0x0008,
			DragDrop = 0x0040,
			FullDrag = 0x0080,
			FilterBar = 0x0100
		}
		public enum ListViewHeaderFilterType
		{
			String = 0x0000,
			Number = 0x0001,
			None = 0x8000
		}
		public enum ListViewHeaderNotificationMessage
		{
			First = (NotifyMessage.First - 300),
			ItemChangingA = (First - 0),
			ItemChangingW = (First - 20),
			ItemChangedA = (First - 1),
			ItemChangedW = (First - 21),
			ItemClickA = (First - 2),
			ItemClickW = (First - 22),
			ItemDoubleClickA = (First - 3),
			ItemDoubleClickW = (First - 23),
			DividerDoubleClickA = (First - 5),
			DividerDoubleClickW = (First - 25),
			BeginTrackA = (First - 6),
			BeginTrackW = (First - 26),
			EndTrackA = (First - 7),
			EndTrackW = (First - 27),
			TrackA = (First - 8),
			TrackW = (First - 28),
			GetDisplayInfoA = (First - 9),
			GetDisplayInfoW = (First - 29),
			BeginDrag = (First - 10),
			EndDrag = (First - 11),
			FilterChange = (First - 12),
			FilterButtonClick = (First - 13)
		}
		public enum WindowExtendedStyle
		{
			/// <summary>
			/// The window accepts drag-drop files.
			/// </summary>
			AcceptFiles = 0x00000010,
			/// <summary>
			/// Forces a top-level window onto the taskbar when the window is visible.
			/// </summary>
			ApplicationWindow = 0x00040000,
			/// <summary>
			/// The window has a border with a sunken edge.
			/// </summary>
			ClientEdge = 0x00000200,
			/// <summary>
			/// Paints all descendants of a window in bottom-to-top painting order using
			/// double-buffering. For more information, see Remarks. This cannot be used
			/// if the window has a class style of either CS_OWNDC or CS_CLASSDC.
			/// </summary>
			/// <remarks>Windows 2000:  This style is not supported.</remarks>
			Composited = 0x02000000,
			/// <summary>
			/// The title bar of the window includes a question mark. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window. WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
			/// </summary>
			ContextHelp = 0x00000400,
			/// <summary>
			/// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
			/// </summary>
			ControlParent = 0x00010000,
			/// <summary>
			/// The window has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
			/// </summary>
			DialogModalFrame = 0x00000001,
			/// <summary>
			/// The window is a layered window. This style cannot be used if the window has
			/// a class style of either CS_OWNDC or CS_CLASSDC. Windows 8: The
			/// WS_EX_LAYERED style is supported for top-level windows and child windows.
			/// Previous Windows versions support WS_EX_LAYERED only for top-level windows.
			/// </summary>
			Layered = 0x00080000,
			/// <summary>
			/// If the shell language is Hebrew, Arabic, or another language that supports
			/// reading order alignment, the horizontal origin of the window is on the
			/// right edge. Increasing horizontal values advance to the left.
			/// </summary>
			LayoutRTL = 0x00400000,
			/// <summary>
			/// The window has generic left-aligned properties. This is the default.
			/// </summary>
			Left = 0x00000000,
			/// <summary>
			/// If the shell language is Hebrew, Arabic, or another language that supports
			/// reading order alignment, the vertical scroll bar (if present) is to the
			/// left of the client area. For other languages, the style is ignored.
			/// </summary>
			LeftScrollBar = 0x00004000,
			/// <summary>
			/// The window text is displayed using left-to-right reading-order properties. This is the default.
			/// </summary>
			LTRReading = 0x00000000,
			/// <summary>
			/// The window is a MDI child window.
			/// </summary>
			MDIChild = 0x00000040,
			/// <summary>
			/// A top-level window created with this style does not become the foreground
			/// window when the user clicks it. The system does not bring this window to
			/// the foreground when the user minimizes or closes the foreground window. To
			/// activate the window, use the SetActiveWindow or SetForegroundWindow
			/// function.
			/// </summary>
			/// <remarks>
			/// The window does not appear on the taskbar by default. To force the window
			/// to appear on the taskbar, use the WS_EX_APPWINDOW style.
			/// </remarks>
			NoActivate = 0x08000000,
			/// <summary>
			/// The window does not pass its window layout to its child windows.
			/// </summary>
			NoInheritLayout = 0x00100000,
			/// <summary>
			/// The child window created with this style does not send the WM_PARENTNOTIFY
			/// message to its parent window when it is created or destroyed.
			/// </summary>
			NoParentNotify = 0x00000004,
			/// <summary>
			/// The window does not render to a redirection surface. This is for windows
			/// that do not have visible content or that use mechanisms other than
			/// surfaces to provide their visual.
			/// </summary>
			NoRedirectionBitmap = 0x00200000,
			/// <summary>
			/// The window is an overlapped window.
			/// </summary>
			OverlappedWindow = (WindowEdge | ClientEdge),
			/// <summary>
			/// The window is palette window, which is a modeless dialog box that presents
			/// an array of commands.
			/// </summary>
			PaletteWindow = (WindowEdge | ToolWindow | TopMost),
			/// <summary>
			/// The window has generic "right-aligned" properties. This depends on the
			/// window class. This style has an effect only if the shell language is
			/// Hebrew, Arabic, or another language that supports reading-order alignment;
			/// otherwise, the style is ignored.
			/// </summary>
			/// <remarks>
			/// Using the WS_EX_RIGHT style for static or edit controls has the same effect
			/// as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style
			/// with button controls has the same effect as using BS_RIGHT and
			/// BS_RIGHTBUTTON styles.
			/// </remarks>
			Right = 0x00001000,
			/// <summary>
			/// The vertical scroll bar (if present) is to the right of the client area.
			/// This is the default.
			/// </summary>
			RightScrollBar = 0x00000000,
			/// <summary>
			/// If the shell language is Hebrew, Arabic, or another language that supports
			/// reading-order alignment, the window text is displayed using right-to-left
			/// reading-order properties. For other languages, the style is ignored.
			/// </summary>
			RTLReading = 0x00002000,
			/// <summary>
			/// The window has a three-dimensional border style intended to be used for
			/// items that do not accept user input.
			/// </summary>
			StaticEdge = 0x00020000,
			/// <summary>
			/// The window is intended to be used as a floating toolbar. A tool window has
			/// a title bar that is shorter than a normal title bar, and the window title
			/// is drawn using a smaller font. A tool window does not appear in the taskbar
			/// or in the dialog that appears when the user presses ALT+TAB. If a tool
			/// window has a system menu, its icon is not displayed on the title bar.
			/// However, you can display the system menu by right-clicking or by typing
			/// ALT+SPACE.
			/// </summary>
			ToolWindow = 0x00000080,
			/// <summary>
			/// The window should be placed above all non-topmost windows and should stay
			/// above them, even when the window is deactivated. To add or remove this
			/// style, use the SetWindowPos function.
			/// </summary>
			TopMost = 0x00000008,
			/// <summary>
			/// The window should not be painted until siblings beneath the window (that
			/// were created by the same thread) have been painted. The window appears
			/// transparent because the bits of underlying sibling windows have already
			/// been painted. To achieve transparency without these restrictions, use the
			/// SetWindowRgn function.
			/// </summary>
			Transparent = 0x00000020,
			/// <summary>
			/// The window has a border with a raised edge.
			/// </summary>
			WindowEdge = 0x00000100
		}
	}
}
