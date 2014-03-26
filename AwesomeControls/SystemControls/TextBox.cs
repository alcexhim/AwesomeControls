using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Drawing;

using TextRenderer = System.Windows.Forms.TextRenderer;

namespace AwesomeControls.SystemControls
{
	public enum TextBoxLineSeparator
	{
		Unknown = -1,
		Default = 0,
		CarriageReturnLineFeed = 1,
		CarriageReturn = 2,
		LineFeed = 3
	}
	public class TextBoxSelection
	{
		public class TextBoxSelectionCollection
			: System.Collections.ObjectModel.Collection<TextBoxSelection>
		{
			private TextBox _parent = null;
			internal TextBoxSelectionCollection(TextBox parent)
			{
				_parent = parent;
			}

			public TextBoxSelection Add(int start, int length)
			{
				Point ptStart = _parent.GetCharPositionFromCharIndex(start);
				Point ptEnd = _parent.GetCharPositionFromCharIndex(start + length);
				return Add(ptStart, ptEnd);
			}
			public TextBoxSelection Add(Point start, Point end)
			{
				TextBoxSelection sel = new TextBoxSelection();
				sel.Start = start;
				sel.End = end;
				Add(sel);
				return sel;
			}
			public TextBoxSelection Add(Point start, Size length)
			{
				TextBoxSelection sel = new TextBoxSelection();
				sel.Start = start;
				sel.Length = length;
				Add(sel);
				return sel;
			}
		}

		private Point mvarStart = Point.Empty;
		public Point Start { get { return mvarStart; } set { mvarStart = value; } }

		private Point mvarEnd = Point.Empty;
		public Point End { get { return mvarEnd; } set { mvarEnd = value; } }

		public Size Length
		{
			get { return new Size(mvarEnd.X - mvarStart.X, mvarEnd.Y - mvarStart.Y); }
			set { mvarEnd = new Point(mvarStart.X + value.Width, mvarStart.Y + value.Height); }
		}
	}
	public class TextBoxAutoCompleteTerm
	{
		public class TextBoxAutoCompleteTermCollection
			: System.Collections.ObjectModel.Collection<TextBoxAutoCompleteTerm>
		{
			public TextBoxAutoCompleteTerm Add(string Value)
			{
				TextBoxAutoCompleteTerm act = new TextBoxAutoCompleteTerm();
				act.Value = Value;
				base.Add(act);
				return act;
			}
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }
	}
	public class TextBoxSyntaxHighlightTerm
	{
		public class TextBoxSyntaxHighlightTermCollection
			: System.Collections.ObjectModel.Collection<TextBoxSyntaxHighlightTerm>
		{
			public TextBoxSyntaxHighlightTerm Add(string Value, System.Drawing.Color ForeColor)
			{
				TextBoxSyntaxHighlightTerm term = new TextBoxSyntaxHighlightTerm();
				term.ForeColor = ForeColor;
				term.Value = Value;
				base.Add(term);
				return term;
			}
		}

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		private System.Drawing.Color mvarForeColor = System.Drawing.Color.Black;
		public System.Drawing.Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }
	}
	public class TextBoxFormatting
	{
		public class TextBoxFormattingCollection
		{
			private System.Collections.Generic.Dictionary<Point, TextBoxFormatting> collection = new Dictionary<Point, TextBoxFormatting>();

			public TextBoxFormatting this[int x, int y]
			{
				get { return this[new Point(x, y)]; }
				set { this[new Point(x, y)] = value; }
			}
			public TextBoxFormatting this[Point pt]
			{
				get
				{
					if (!collection.ContainsKey(pt)) return null;
					return collection[pt];
				}
				set
				{
					if (collection.ContainsKey(pt))
					{
						collection[pt] = value;
					}
					else
					{
						collection.Add(pt, value);
					}
				}
			}
		}

		private Color mvarForeColor = Color.FromKnownColor(KnownColor.WindowText);
		public Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

		private Color mvarBackColor = Color.Empty;
		public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

		private Font mvarFont = null;
		public Font Font { get { return mvarFont; } set { mvarFont = value; } }
	}

	partial class TextBox : System.Windows.Forms.Control
	{
		private int widthModifier = -7;


		public TextBox()
		{
			InitializeComponent();

			mvarSelections = new TextBoxSelection.TextBoxSelectionCollection(this);

			// added as a quick hack to prevent flicker, use Invalidate() when possible!
			base.DoubleBuffered = true;
		}
		
		#region Default properties
		protected override System.Windows.Forms.Cursor DefaultCursor { get { return System.Windows.Forms.Cursors.IBeam; } }
		#endregion

		#region Overwrite
		private bool mvarEnableOverwrite = false;
		public bool EnableOverwrite { get { return mvarEnableOverwrite; } set { mvarEnableOverwrite = value; } }

		private bool mvarEnableOverwriteShortcut = true;
		/// <summary>
		/// Determines whether pressing the Insert key will toggle overwrite mode.
		/// </summary>
		public bool EnableOverwriteShortcut { get { return mvarEnableOverwriteShortcut; } set { mvarEnableOverwriteShortcut = value; } }
		#endregion

		#region Line separator
		public TextBoxLineSeparator LineSeparator
		{
			get
			{
				if (mvarLineSeparatorString == Environment.NewLine) return TextBoxLineSeparator.Default;
				switch (mvarLineSeparatorString)
				{
					case "\r\n": return TextBoxLineSeparator.CarriageReturnLineFeed;
					case "\r": return TextBoxLineSeparator.CarriageReturn;
					case "\n": return TextBoxLineSeparator.LineFeed;
				}
				return TextBoxLineSeparator.Unknown;
			}
			set
			{
				switch (value)
				{
					case TextBoxLineSeparator.CarriageReturn: mvarLineSeparatorString = "\r"; return;
					case TextBoxLineSeparator.CarriageReturnLineFeed: mvarLineSeparatorString = "\r\n"; return;
					case TextBoxLineSeparator.Default: mvarLineSeparatorString = Environment.NewLine; return;
					case TextBoxLineSeparator.LineFeed: mvarLineSeparatorString = "\n"; return;
				}
				throw new InvalidOperationException();
			}
		}

		private string mvarLineSeparatorString = "\r\n";
		public string LineSeparatorString { get { return mvarLineSeparatorString; } set { mvarLineSeparatorString = value; } }
		#endregion

		private TextBoxSelection.TextBoxSelectionCollection mvarSelections = null;
		public TextBoxSelection.TextBoxSelectionCollection Selections { get { return mvarSelections; } }

		private bool m_Selecting = false;

		private bool mvarEnableSelection = true;
		public bool EnableSelection { get { return mvarEnableSelection; } set { mvarEnableSelection = value; } }

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) != System.Windows.Forms.Keys.Control)
				{
					mvarSelections.Clear();
				}
				
				Point point = GetCharPositionFromPhysicalPosition(e.Location);
				mvarSelections.Add(point, Size.Empty);
				m_Selecting = true;
			}

			m_DisplayCaret = true;
			RefreshCaret();

			Focus();
		}
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (mvarEnableSelection && m_Selecting)
			{
				TextBoxSelection sel = mvarSelections[mvarSelections.Count - 1];
				sel.End = GetCharPositionFromPhysicalPosition(e.Location);

				Refresh();
			}
		}
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);
			m_Selecting = false;

			if (e.Button == System.Windows.Forms.MouseButtons.Right && base.ContextMenuStrip == null && base.ContextMenu == null)
			{
				mnuContext.Show(this, e.Location);
			}
		}
		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(e);
			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.LWin:
				case System.Windows.Forms.Keys.RWin:
				case System.Windows.Forms.Keys.Shift:
				case System.Windows.Forms.Keys.ShiftKey:
				case System.Windows.Forms.Keys.LShiftKey:
				case System.Windows.Forms.Keys.RShiftKey:
				case System.Windows.Forms.Keys.LControlKey:
				case System.Windows.Forms.Keys.RControlKey:
				case System.Windows.Forms.Keys.Control:
				case System.Windows.Forms.Keys.ControlKey:
				case System.Windows.Forms.Keys.Alt:
				case System.Windows.Forms.Keys.Menu:
				case System.Windows.Forms.Keys.LMenu:
				case System.Windows.Forms.Keys.RMenu:
				{
					break;
				}
				case System.Windows.Forms.Keys.Enter:
				{
					// if (mvarAcceptReturn)
					// {
					e.SuppressKeyPress = true;
					e.Handled = true;
					InsertText(mvarLineSeparatorString);
					// }
					break;
				}
				case System.Windows.Forms.Keys.Back:
				{
					if (Text.Length <= 0)
					{
						// System.Media.SystemSounds.Beep.Play();
						return;
					}

					foreach (TextBoxSelection sel in mvarSelections)
					{
						int start = GetCharIndexFromCharPosition(sel.Start);
						int end = GetCharIndexFromCharPosition(sel.End);
						int length = end - start;

						if (start <= 0) return;
						DeleteText(start, length);
						sel.Start = new Point(sel.Start.X - 1, sel.Start.Y);
						sel.Length = new Size(0, 0);
					}
					break;
				}
				#region Top-row Numeric Keys
				case System.Windows.Forms.Keys.D0:
				{
					if (e.Shift)
					{
						InsertText(")");
					}
					else
					{
						InsertText("0");
					}
					break;
				}
				case System.Windows.Forms.Keys.D1:
				{
					if (e.Shift)
					{
						InsertText("!");
					}
					else
					{
						InsertText("1");
					}
					break;
				}
				case System.Windows.Forms.Keys.D2:
				{
					if (e.Shift)
					{
						InsertText("@");
					}
					else
					{
						InsertText("2");
					}
					break;
				}
				case System.Windows.Forms.Keys.D3:
				{
					if (e.Shift)
					{
						InsertText("#");
					}
					else
					{
						InsertText("3");
					}
					break;
				}
				case System.Windows.Forms.Keys.D4:
				{
					if (e.Shift)
					{
						InsertText("$");
					}
					else
					{
						InsertText("4");
					}
					break;
				}
				case System.Windows.Forms.Keys.D5:
				{
					if (e.Shift)
					{
						InsertText("%");
					}
					else
					{
						InsertText("5");
					}
					break;
				}
				case System.Windows.Forms.Keys.D6:
				{
					if (e.Shift)
					{
						InsertText("^");
					}
					else
					{
						InsertText("6");
					}
					break;
				}
				case System.Windows.Forms.Keys.D7:
				{
					if (e.Shift)
					{
						InsertText("&");
					}
					else
					{
						InsertText("7");
					}
					break;
				}
				case System.Windows.Forms.Keys.D8:
				{
					if (e.Shift)
					{
						InsertText("*");
					}
					else
					{
						InsertText("8");
					}
					break;
				}
				case System.Windows.Forms.Keys.D9:
				{
					if (e.Shift)
					{
						InsertText("(");
					}
					else
					{
						InsertText("9");
					}
					break;
				}
				#endregion
				#region OEM keys
				case System.Windows.Forms.Keys.Oemtilde:
				{
					if (e.Shift)
					{
						InsertText("~");
					}
					else
					{
						InsertText("`");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemMinus:
				{
					if (e.Shift)
					{
						InsertText("_");
					}
					else
					{
						InsertText("-");
					}
					break;
				}
				case System.Windows.Forms.Keys.Oemplus:
				{
					if (e.Shift)
					{
						InsertText("+");
					}
					else
					{
						InsertText("=");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemQuestion:
				{
					if (e.Shift)
					{
						InsertText("?");
					}
					else
					{
						InsertText("/");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemPipe:
				{
					if (e.Shift)
					{
						InsertText("|");
					}
					else
					{
						InsertText("\\");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemPeriod:
				{
					if (e.Shift)
					{
						InsertText(">");
					}
					else
					{
						InsertText(".");
					}
					break;
				}
				case System.Windows.Forms.Keys.Oemcomma:
				{
					if (e.Shift)
					{
						InsertText("<");
					}
					else
					{
						InsertText(",");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemOpenBrackets:
				{
					if (e.Shift)
					{
						InsertText("{");
					}
					else
					{
						InsertText("[");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemCloseBrackets:
				{
					if (e.Shift)
					{
						InsertText("}");
					}
					else
					{
						InsertText("]");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemQuotes:
				{
					if (e.Shift)
					{
						InsertText("\"");
					}
					else
					{
						InsertText("'");
					}
					break;
				}
				case System.Windows.Forms.Keys.OemSemicolon:
				{
					if (e.Shift)
					{
						InsertText(":");
					}
					else
					{
						InsertText(";");
					}
					break;
				}
				#endregion
				#region Navigation Keys
				case System.Windows.Forms.Keys.Insert:
				{
					// if shortcut is enabled, enable/disable overwrite mode
					if (mvarEnableOverwriteShortcut)
					{
						mvarEnableOverwrite = !mvarEnableOverwrite;
					}
					break;
				}
				case System.Windows.Forms.Keys.Delete:
				{
					// backwards backspace
					foreach (TextBoxSelection sel in mvarSelections)
					{
						/*
						int sellength = sel.Length;
						if (sellength == 0) sellength = 1;

						if (mvarSelectionStart + sellength > Text.Length) return;

						string Before = Text.Substring(0, mvarSelectionStart);
						string After = Text.Substring(mvarSelectionStart + sellength);
						Text = Before + After;
						sel.Length = Size.Empty;
						*/
					}
					break;
				}
				case System.Windows.Forms.Keys.Home:
				{
					int selstart = 0; //  mvarSelectionStart;
					if (e.Control || !Text.Contains(mvarLineSeparatorString))
					{
						// mvarSelectionStart = 0;
					}
					else
					{
						// only home to the start of the line.
						// mvarSelectionStart = Text.LastIndexOf(mvarNewLineCharacterString, 0, mvarSelectionStart);
					}

					if (e.Shift)
					{
						// mvarSelectionLength = selstart;
					}
					else
					{
						// mvarSelectionLength = 0;
					}

					m_DisplayCaret = true;
					RefreshCaret();
					break;
				}
				case System.Windows.Forms.Keys.End:
				{
					if (e.Control || !Text.Contains(mvarLineSeparatorString))
					{
						// mvarSelectionStart = Text.Length;
					}
					else
					{
						// only end to the end of the line.
						// mvarSelectionStart = Text.IndexOf(mvarNewLineCharacterString, mvarSelectionStart);
					}

					if (e.Shift)
					{
					}
					else
					{
						// mvarSelectionLength = 0;
					}

					m_DisplayCaret = true;
					RefreshCaret();
					break;
				}
				case System.Windows.Forms.Keys.PageUp:
				{
					break;
				}
				case System.Windows.Forms.Keys.PageDown:
				{
					break;
				}
				#endregion
				#region Numeric Pad Keys
				case System.Windows.Forms.Keys.NumLock:
				{
					break;
				}
				case System.Windows.Forms.Keys.NumPad0:
				{
					InsertText("0");
					break;
				}
				case System.Windows.Forms.Keys.NumPad1:
				{
					InsertText("1");
					break;
				}
				case System.Windows.Forms.Keys.NumPad2:
				{
					InsertText("2");
					break;
				}
				case System.Windows.Forms.Keys.NumPad3:
				{
					InsertText("3");
					break;
				}
				case System.Windows.Forms.Keys.NumPad4:
				{
					InsertText("4");
					break;
				}
				case System.Windows.Forms.Keys.NumPad5:
				{
					InsertText("5");
					break;
				}
				case System.Windows.Forms.Keys.NumPad6:
				{
					InsertText("6");
					break;
				}
				case System.Windows.Forms.Keys.NumPad7:
				{
					InsertText("7");
					break;
				}
				case System.Windows.Forms.Keys.NumPad8:
				{
					InsertText("8");
					break;
				}
				case System.Windows.Forms.Keys.NumPad9:
				{
					InsertText("9");
					break;
				}
				case System.Windows.Forms.Keys.Divide:
				{
					InsertText("/");
					break;
				}
				case System.Windows.Forms.Keys.Multiply:
				{
					InsertText("*");
					break;
				}
				case System.Windows.Forms.Keys.Subtract:
				{
					InsertText("-");
					break;
				}
				case System.Windows.Forms.Keys.Add:
				{
					InsertText("+");
					break;
				}
				case System.Windows.Forms.Keys.Decimal:
				{
					InsertText(".");
					break;
				}
				#endregion
				default:
				{
					if (e.Shift)
					{
						InsertText(((char)e.KeyValue).ToString().ToUpper());
					}
					else
					{
						InsertText(((char)e.KeyValue).ToString().ToLower());
					}
					break;
				}
			}

			Refresh();
		}
		protected override bool ProcessDialogKey(System.Windows.Forms.Keys keyData)
		{
			System.Windows.Forms.Keys keyCode = (keyData & System.Windows.Forms.Keys.KeyCode);
			System.Windows.Forms.Keys modifiers = (keyData & System.Windows.Forms.Keys.Modifiers);

			bool ctrl = ((modifiers & System.Windows.Forms.Keys.Control) == System.Windows.Forms.Keys.Control);
			bool alt = ((modifiers & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt);
			bool shift = ((modifiers & System.Windows.Forms.Keys.Shift) == System.Windows.Forms.Keys.Shift);

			switch (keyCode)
			{
				case System.Windows.Forms.Keys.Up:
				{
					return true;
				}
				case System.Windows.Forms.Keys.Down:
				{
					return true;
				}
				case System.Windows.Forms.Keys.Left:
				{
					/*
					if (mvarSelectionStart > 0)
					{
						mvarSelectionStart--;
						mvarSelectionLength = 0;

						m_DisplayCaret = true;
						RefreshCaret();
					}
					*/
					return true;
				}
				case System.Windows.Forms.Keys.Right:
				{
					/*
					if (mvarSelectionStart < Text.Length)
					{
						if (shift)
						{
							if (mvarSelectionStart + mvarSelectionLength < Text.Length)
							{
								mvarSelectionLength++;
							}
						}
						else
						{
							mvarSelectionStart++;
							mvarSelectionLength = 0;
						}

						m_DisplayCaret = true;
						RefreshCaret();
					}
					*/
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		#region Public API methods
		#region Position conversion
		private int posoffsetX = -6;

		public Point GetPhysicalPositionFromCharPosition(Point pt)
		{
			return GetPhysicalPositionFromCharPosition(pt.X, pt.Y);
		}
		public Point GetPhysicalPositionFromCharPosition(int row, int column)
		{
			int _x = 0, _y = 0;
			int offsetX = 2, offsetY = 2;
			int cx = offsetX, cy = offsetY;
			int lastHeight = 0;

			for (int i = 0; i < Text.Length; i++)
			{
				Font font = base.Font;
				Size sz = TextRenderer.MeasureText(Text[i].ToString(), font);
				if (lastHeight < sz.Height) lastHeight = sz.Height;

				if ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString))
				{
					// new line
					_x = 0;
					cx = offsetX;

					// TODO: get largest font height for this line
					_y++;

					// use the largest font height for this line
					cy += lastHeight;
					lastHeight = 0;
				}
				else
				{
					_x++;
					cx += sz.Width + posoffsetX;
				}
				if (_x == row && _y == column)
				{
					return new Point(cx, cy);
				}
			}
			return Point.Empty;
		}

		/// <summary>
		/// Translates a character index into physical pixel coordinates.
		/// </summary>
		/// <param name="charIndex"></param>
		/// <returns>A <see cref="System.Drawing.Point" /> representing the pixel coordinates of the specified character at that location.</returns>
		/// <remarks></remarks>
		public Point GetPhysicalPositionFromCharIndex(int charIndex)
		{
			// if (!base.Font.IsMonospace()) return Point.Empty;
			if (charIndex > Text.Length) return Point.Empty;

			Font font = base.Font;
			int x = 2, y = 2;

			string[] lines = base.Text.Split(new string[] { mvarLineSeparatorString }, StringSplitOptions.None);
			int currentLineIndex = 0;
			int greatestHeight = 0;

			for (int i = 0; i < charIndex; i++)
			{
				if (i > lines[currentLineIndex].Length)
				{
					currentLineIndex++;
					x = 2;

					int lastHeight = TextRenderer.MeasureText(Text[i].ToString(), font).Height;
					if (lastHeight > greatestHeight) greatestHeight = lastHeight;

					y += greatestHeight;
					greatestHeight = 0;
				}

				x += TextRenderer.MeasureText(Text[i].ToString(), font).Width + widthModifier;
			}

			return new System.Drawing.Point(x, y);
		}

		public int GetCharIndexFromPhysicalPosition(Point location)
		{
			return GetCharIndexFromPhysicalPosition(location.X, location.Y);
		}
		public int GetCharIndexFromPhysicalPosition(int x, int y)
		{
			// if (!base.Font.IsMonospace()) return Point.Empty;
			Font font = base.Font;
			int _x = 2, _y = 2;

			string[] lines = base.Text.Split(new string[] { mvarLineSeparatorString }, StringSplitOptions.None);
			int currentLineIndex = 0;
			int greatestHeight = 0;

			for (int i = 0; i < Text.Length; i++)
			{
				Size sz = TextRenderer.MeasureText(Text[i].ToString(), font);
				if (x >= 0 && x <= 2 && y >= _y && y <= (_y + sz.Height))
				{
					return 0;
				}
				else
				{
					if (i > lines[currentLineIndex].Length)
					{
						currentLineIndex++;
						_x = 2;

						int lastHeight = sz.Height;
						if (lastHeight > greatestHeight) greatestHeight = lastHeight;

						_y += greatestHeight;
						greatestHeight = 0;
					}

					if (x >= _x && x <= (_x + sz.Width) && y >= _y && y <= (_y + sz.Height))
					{
						return i + 1;
					}
				}
				_x += (sz.Width + widthModifier);
			}
			return lines[currentLineIndex].Length;
		}

		/// <summary>
		/// Translates row/column coordinates into physical pixel coordinates.
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns>A <see cref="System.Drawing.Point" /> representing the pixel coordinates of the specified character at that location.</returns>
		/// <remarks></remarks>
		public System.Drawing.Point CharacterCoordinateToPixel(int row, int column)
		{
			// if (!base.Font.IsMonospace()) return System.Drawing.Point.Empty;

			Font font = base.Font;
			int x = 0, y = 0;
			string[] lines = base.Text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

			if (row >= lines.Length) throw new InvalidOperationException("Row cannot be greater than number of lines of text");

			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < lines[i].Length; j++)
				{
					TextBoxFormatting formatting = mvarFormatting[i, j];
					if (formatting != null) font = formatting.Font;

					y += TextRenderer.MeasureText(lines[i][j].ToString(), font).Height;
				}
			}

			string value = lines[row];


			return new System.Drawing.Point(x, y);
		}

		public int GetCharIndexFromCharPosition(Point pt)
		{
			return GetCharIndexFromCharPosition(pt.X, pt.Y);
		}
		public int GetCharIndexFromCharPosition(int row, int column)
		{
			int _x = 0, _y = 0;
			for (int i = 0; i < Text.Length; i++)
			{
				if (_x == column && _y == row)
				{
					return i;
				}
				if ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString))
				{
					// new line
					_x = 0;

					// TODO: get largest font height for this line
					_y++;
				}
				else
				{
					_x++;
				}
			}
			return Text.Length - 1;
		}
		/// <summary>
		/// Gets the row and column location of the character at the specified
		/// physical position.
		/// </summary>
		/// <param name="pt">The physical position to search in.</param>
		/// <returns>
		/// A <see cref="System.Drawing.Point" /> representing the row and column
		/// location of the character.
		/// </returns>
		public Point GetCharPositionFromPhysicalPosition(Point pt)
		{
			return GetCharPositionFromPhysicalPosition(pt.X, pt.Y);
		}
		/// <summary>
		/// Translates physical pixel coordinates into row/column coordinates.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public Point GetCharPositionFromPhysicalPosition(int x, int y)
		{
			// if (!base.Font.IsMonospace()) return Point.Empty;
			Font font = base.Font;
			int _x = 2, _y = 2;
			int row = 0, column = 0;

			string[] lines = base.Text.Split(new string[] { mvarLineSeparatorString }, StringSplitOptions.None);
			int currentLineIndex = 0;
			int greatestHeight = 0;
			int j = 0;

			for (int i = 0; i < Text.Length; i++)
			{
				Size sz = TextRenderer.MeasureText(Text[i].ToString(), font);
				if (x >= 0 && x <= 2 && y >= _y && y <= (_y + sz.Height))
				{
					return new Point(0, 0);
				}
				else
				{
					if ((i - j) > lines[currentLineIndex].Length)
					{
						currentLineIndex++;
						_x = 2;

						int lastHeight = sz.Height;
						if (lastHeight > greatestHeight) greatestHeight = lastHeight;

						_y += greatestHeight;
						greatestHeight = 0;

						row++;
						column = 0;
						j += i;
					}
					else
					{
						column++;
					}

					if (x >= _x && x <= (_x + sz.Width) && y >= _y && y <= (_y + sz.Height))
					{
						return new Point(column, row);
					}

				}
				_x += (sz.Width + widthModifier);
			}
			return Point.Empty;
		}
		public Point GetCharPositionFromCharIndex(int charIndex)
		{
			int x = 0, y = 0;
			for (int i = 0; i < charIndex; i++)
			{
				if ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString))
				{
					// new line
					x = 0;
					y++;
				}
				else
				{
					x++;
				}
			}
			return new Point(x, y);
		}
		#endregion
		#region Text Modification
		/// <summary>
		/// Inserts text at the current location, overwriting any selected text. If overwrite is enabled,
		/// this method overwrites the next character when no text is selected.
		/// </summary>
		/// <param name="Text">The text to insert.</param>
		public void InsertText(string Text)
		{
			List<TextBoxSelection> sels = new List<TextBoxSelection>();
			foreach (TextBoxSelection sel in mvarSelections)
			{
				sels.Add(sel);
			}
			foreach (TextBoxSelection sel in sels)
			{
				int start = GetCharIndexFromCharPosition(sel.Start);
				int end = GetCharIndexFromCharPosition(sel.End);
				int length = end - start;
				InsertText(Text, start, length);
			}
		}
		/// <summary>
		/// Inserts text at the specified location. If overwrite is enabled, this method overwrites the
		/// next character.
		/// </summary>
		/// <param name="Text">The text to insert.</param>
		/// <param name="SelectionStart">The position at which to begin inserting text.</param>
		public void InsertText(string Text, int SelectionStart)
		{
			InsertText(Text, SelectionStart, 0);
		}
		/// <summary>
		/// Inserts text at the specified location, overwriting the number of characters specified in
		/// <see cref="SelectionLength" />. If overwrite is enabled and <see cref="SelectionLength" /> is
		/// zero, this method overwrites the next character.
		/// </summary>
		/// <param name="Text">The text to insert.</param>
		/// <param name="SelectionStart">The position at which to begin inserting text.</param>
		/// <param name="SelectionLength">The amount of text to overwrite starting at <see cref="SelectionStart" />.</param>
		public void InsertText(string Text, int SelectionStart, int SelectionLength)
		{
			InsertText(Text, SelectionStart, SelectionLength, true);
		}
		/// <summary>
		/// Inserts text at the specified location, overwriting the number of characters specified in
		/// <see cref="SelectionLength" />. If overwrite is enabled and <see cref="SelectionLength" /> is
		/// zero, this method overwrites the next character.
		/// </summary>
		/// <param name="Text">The text to insert.</param>
		/// <param name="SelectionStart">The position at which to begin inserting text.</param>
		/// <param name="SelectionLength">The amount of text to overwrite starting at <see cref="SelectionStart" />.</param>
		/// <param name="UpdateSelection">If true, updates the <see cref="TextBox" /> selection start and length.</param>
		public void InsertText(string Text, int SelectionStart, int SelectionLength, bool UpdateSelection)
		{
			int selStart = SelectionStart;
			if (selStart > base.Text.Length) selStart = 0;

			int selLen = SelectionLength;
			if (mvarEnableOverwrite && selLen == 0) selLen = 1;
			if (selStart + selLen > base.Text.Length) selLen = 0;

			string Before = base.Text.Substring(0, selStart);
			string After = base.Text.Substring(selStart + selLen);
			base.Text = Before + Text + After;

			if (UpdateSelection)
			{
				foreach (TextBoxSelection sel in mvarSelections)
				{
					sel.Start = new Point(sel.Start.X + Text.Length, sel.Start.Y);
					sel.Length = new Size(0, 0);
				}
			}
		}
		public void DeleteText(int SelectionStart, int SelectionLength)
		{
			string before = Text.Substring(0, SelectionStart - 1);
			string after = Text.Substring(SelectionStart + SelectionLength);
			Text = before + after;
		}
		#endregion
		#endregion

		private TextBoxFormatting.TextBoxFormattingCollection mvarFormatting = new TextBoxFormatting.TextBoxFormattingCollection();
		public TextBoxFormatting.TextBoxFormattingCollection Formatting { get { return mvarFormatting; } }

		private bool mvarEnableOutlining = false;
		public bool EnableOutlining { get { return mvarEnableOutlining; } set { mvarEnableOutlining = value; } }

		private bool mvarEnableSyntaxHighlight = false;
		public bool EnableSyntaxHighlight { get { return mvarEnableSyntaxHighlight; } set { mvarEnableSyntaxHighlight = value; } }

		private bool mvarEnableAutoComplete = false;
		public bool EnableAutoComplete { get { return mvarEnableAutoComplete; } set { mvarEnableAutoComplete = value; } }

		private TextBoxAutoCompleteTerm.TextBoxAutoCompleteTermCollection mvarAutoCompleteTerms = new TextBoxAutoCompleteTerm.TextBoxAutoCompleteTermCollection();
		public TextBoxAutoCompleteTerm.TextBoxAutoCompleteTermCollection AutoCompleteTerms { get { return mvarAutoCompleteTerms; } }

		private TextBoxSyntaxHighlightTerm.TextBoxSyntaxHighlightTermCollection mvarSyntaxHighlightTerms = new TextBoxSyntaxHighlightTerm.TextBoxSyntaxHighlightTermCollection();
		public TextBoxSyntaxHighlightTerm.TextBoxSyntaxHighlightTermCollection SyntaxHighlightTerms { get { return mvarSyntaxHighlightTerms; } }

		private bool mvarSyntaxHighlightCaseSensitive = true;
		public bool SyntaxHighlightCaseSensitive { get { return mvarSyntaxHighlightCaseSensitive; } set { mvarSyntaxHighlightCaseSensitive = value; } }

		private bool mvarMultiline = false;
		public bool Multiline { get { return mvarMultiline; } set { mvarMultiline = value; } }

		private System.Windows.Forms.RichTextBoxScrollBars mvarScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
		public System.Windows.Forms.RichTextBoxScrollBars ScrollBars { get { return mvarScrollBars; } set { mvarScrollBars = value; } }

		#region Caret
		private bool mvarEnableCaret = true;
		public bool EnableCaret { get { return mvarEnableCaret; } set { mvarEnableCaret = value; RefreshCaret(); } }

		private int mvarCaretSize = 2;
		public int CaretSize { get { return mvarCaretSize; } set { mvarCaretSize = value; RefreshCaret(); } }

		private System.Windows.Forms.Orientation mvarCaretOrientation = System.Windows.Forms.Orientation.Vertical;
		public System.Windows.Forms.Orientation CaretOrientation { get { return mvarCaretOrientation; } set { mvarCaretOrientation = value; RefreshCaret(); } }

		private bool m_DisplayCaret = false;

		private bool mvarEnableCaretBlink = true;
		public bool EnableCaretBlink { get { return mvarEnableCaretBlink; } set { mvarEnableCaretBlink = value; RefreshCaret(); } }

		private int mvarCaretBlinkInterval = System.Windows.Forms.SystemInformation.CaretBlinkTime;

		public int CaretBlinkInterval { get { return mvarCaretBlinkInterval; } set { mvarCaretBlinkInterval = value; tmrCaret.Interval = value; } }

		private System.Drawing.Point m_CaretLocation = new System.Drawing.Point(0, 0);


		private void tmrCaret_Tick(object sender, EventArgs e)
		{
			m_DisplayCaret = !m_DisplayCaret;
			RefreshCaret();
		}

		private Color mvarCaretColor = Color.Black;
		public Color CaretColor { get { return mvarCaretColor; } set { mvarCaretColor = value; } }

		/// <summary>
		/// Gets the physical location of the caret (internally defined as the character location).
		/// </summary>
		/// <returns></returns>
		public Rectangle GetCaretRectangle(TextBoxSelection sel)
		{
			Point position = GetPhysicalPositionFromCharPosition(sel.Start);
			Size size = Size.Empty;
			switch (mvarCaretOrientation)
			{
				case System.Windows.Forms.Orientation.Horizontal:
				{
					size = new Size(mvarCaretSize, base.Font.Height);
					break;
				}
				case System.Windows.Forms.Orientation.Vertical: size = new Size(mvarCaretSize, base.Font.Height); break;
			}
			position.X += size.Width;
			return new Rectangle(position, size);
		}

		public void RefreshCaret()
		{
			// Invalidate(GetCaretRectangle());
			Refresh();
		}
		#endregion

		#region Character/Word Spacing
		private int mvarCharacterSpacing = 0;
		public int CharacterSpacing { get { return mvarCharacterSpacing; } set { mvarCharacterSpacing = value; } }
		private int mvarWordSpacing = 0;
		public int WordSpacing { get { return mvarWordSpacing; } set { mvarWordSpacing = value; } }
		#endregion

		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);

			m_DisplayCaret = !mvarEnableCaretBlink;
			tmrCaret.Enabled = false;
			RefreshCaret();
		}
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);

			m_DisplayCaret = !mvarEnableCaretBlink;
			tmrCaret.Enabled = mvarEnableCaret;
			RefreshCaret();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Theming.Theme.CurrentTheme.DrawTextBoxBackground(e.Graphics, base.ClientRectangle, ControlState.Normal);

			if (mvarEnableCaret && m_DisplayCaret)
			{
				foreach (TextBoxSelection sel in mvarSelections)
				{
					e.Graphics.FillRectangle(new SolidBrush(mvarCaretColor), GetCaretRectangle(sel));
				}
			}

			Font font = Font;
			Rectangle rect = new Rectangle(2, 2, base.Width, base.Height);
			Color foreColor = ForeColor;
			Color backColor = Color.Empty;

			int pX = 0, pY = 0;
			int largestHeight = Font.Height;
			for (int i = 0; i < Text.Length; i++)
			{
				if ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString))
				{
					// new line
					pX = 0;
					pY++;

					rect.X = 2;
					rect.Y += largestHeight;
				}
				else
				{
					pX++;
				}

				Size size = TextRenderer.MeasureText(Text[i].ToString(), font);
				rect.Size = size;

				rect.X += 2;
				rect.Width += widthModifier;
				foreach (TextBoxSelection sel in mvarSelections)
				{
					if (sel.Length.Width > 0 || sel.Length.Height > 0)
					{
						if (pX >= sel.Start.X && pY >= sel.Start.Y && pX <= sel.End.X && pY <= sel.End.Y)
						{
							e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedBackground), rect);
						}
					}
				}
				rect.Width -= widthModifier;
				rect.X -= 2;

				TextRenderer.DrawText(e.Graphics, Text[i].ToString(), font, rect, foreColor, backColor, System.Windows.Forms.TextFormatFlags.Left | System.Windows.Forms.TextFormatFlags.Top);

				size.Width += widthModifier;

				rect.X += size.Width + mvarCharacterSpacing;
			}
		}

		private void mnuContext_Opening(object sender, CancelEventArgs e)
		{
			mnuContextSep3.Visible = mvarEnableOutlining;
			mnuContextOutlining.Visible = mvarEnableOutlining;
		}
	}
}
