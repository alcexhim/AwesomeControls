using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using System.Drawing;

using TextRenderer = System.Windows.Forms.TextRenderer;
using SystemInformation = System.Windows.Forms.SystemInformation;

// TODO: fix the selection when there is more than one line...
// TODO: fix the caret positioning when there is more than one line...

namespace AwesomeControls.TextBox
{
	[DefaultEvent("TextChanged")]
	partial class TextBoxControl : System.Windows.Forms.Control
	{
		private int widthModifier = -7;

		private bool mvarReplaceTabsWithSpaces = true;
		public bool ReplaceTabsWithSpaces { get { return mvarReplaceTabsWithSpaces; } set { mvarReplaceTabsWithSpaces = value; } }

		private int mvarTabSize = 4;
		public int TabSize { get { return mvarTabSize; } set { mvarTabSize = value; } }

		private TextBoxSyntaxGroup.TextBoxSyntaxHighlightGroupCollection mvarSyntaxHighlightGroups = new TextBoxSyntaxGroup.TextBoxSyntaxHighlightGroupCollection();
		public TextBoxSyntaxGroup.TextBoxSyntaxHighlightGroupCollection SyntaxHighlightGroups { get { return mvarSyntaxHighlightGroups; } }

		public TextBoxControl()
		{
			InitializeComponent();
			
			base.BackColor = Theming.Theme.CurrentTheme.ColorTable.WindowBackground;
			base.ForeColor = Theming.Theme.CurrentTheme.ColorTable.WindowForeground;

			mvarSelections = new TextBoxSelection.TextBoxSelectionCollection(this);

			// added as a quick hack to prevent flicker, use Invalidate() when possible!
			base.DoubleBuffered = true;

			mvarWordBreakingSequences.Add(" ");
			mvarWordBreakingSequences.Add(mvarLineSeparatorString);

			AutoSuggestAcceptKeys.Add(System.Windows.Forms.Keys.Enter);
			AutoSuggestAcceptKeys.Add(System.Windows.Forms.Keys.Space);

			AutoSuggestInhibitKeys.Add(System.Windows.Forms.Keys.Enter);
		}


		#region Default properties
		protected override System.Windows.Forms.Cursor DefaultCursor { get { return System.Windows.Forms.Cursors.IBeam; } }
		public override void ResetBackColor()
		{
			base.ResetBackColor();
			base.BackColor = Theming.Theme.CurrentTheme.ColorTable.WindowBackground;
		}
		public override void ResetForeColor()
		{
			base.ResetForeColor();
			base.ForeColor = Theming.Theme.CurrentTheme.ColorTable.WindowForeground;
		}
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

		private TextBoxSelectionMode m_Selecting = TextBoxSelectionMode.None;

		#region Auto-Suggest
		private TextBoxAutoSuggestWindow wndAC = new TextBoxAutoSuggestWindow();

		private List<System.Windows.Forms.Keys> mvarAutoSuggestAcceptKeys = new List<System.Windows.Forms.Keys>();
		/// <summary>
		/// The list of keys that cause the AutoSuggest system to accept the suggestion. The default includes SPACE
		/// and ENTER. You can add new keys to the default, or Clear() the list to specify your own.
		/// </summary>
		public List<System.Windows.Forms.Keys> AutoSuggestAcceptKeys { get { return mvarAutoSuggestAcceptKeys; } }

		private List<System.Windows.Forms.Keys> mvarAutoSuggestInhibitKeys = new List<System.Windows.Forms.Keys>();
		/// <summary>
		/// The list of keys that cause further key event processing to stop after the AutoSuggest system has 
		/// processed the key event. The default includes ENTER. You can add new keys to the default, or Clear() the
		/// list to specify your own.
		/// </summary>
		public List<System.Windows.Forms.Keys> AutoSuggestInhibitKeys { get { return mvarAutoSuggestInhibitKeys; } }

		private List<System.Windows.Forms.Keys> mvarAutoSuggestDisplayKeys = new List<System.Windows.Forms.Keys>();
		/// <summary>
		/// The list of keys that cause the AutoSuggest window to be displayed. Not implemented.
		/// </summary>
		public List<System.Windows.Forms.Keys> AutoSuggestDisplayKeys { get { return mvarAutoSuggestDisplayKeys; } }

		private bool mvarAutoSuggestFilter = true;
		/// <summary>
		/// When true, hides all items in the AutoSuggest list that do not match the text currently
		/// typed. When false, only selects the matching item in the list, does not hide any items.
		/// </summary>
		public bool AutoSuggestFilter { get { return mvarAutoSuggestFilter; } set { mvarAutoSuggestFilter = value; } }

		private TextBoxAutoSuggestMode mvarAutoSuggestMode = TextBoxAutoSuggestMode.None;
		/// <summary>
		/// Determines how options in the AutoSuggest list will be presented.
		/// </summary>
		public TextBoxAutoSuggestMode AutoSuggestMode { get { return mvarAutoSuggestMode; } set { mvarAutoSuggestMode = value; } }

		private TextBoxAutoSuggestTerm.TextBoxAutoSuggestTermCollection mvarAutoCompleteTerms = new TextBoxAutoSuggestTerm.TextBoxAutoSuggestTermCollection();
		public TextBoxAutoSuggestTerm.TextBoxAutoSuggestTermCollection AutoSuggestTerms { get { return mvarAutoCompleteTerms; } }

		private void ACUpdateAutoCompleteList()
		{
			wndAC.lst.Items.Clear();

			bool selected = false;
			
			List<TextBoxAutoSuggestTerm> terms = mvarAutoCompleteTerms.ToList<TextBoxAutoSuggestTerm>();
			terms.Sort();


			TextBoxAutoSuggestTermItem previtem = null;
			foreach (TextBoxAutoSuggestTerm term in terms)
			{
				if (term is TextBoxAutoSuggestTermItem)
				{
					TextBoxAutoSuggestTermItem item = (term as TextBoxAutoSuggestTermItem);

					bool test = false;
					if (mvarCaseSensitive)
					{
						test = (item.Value == PreviousWord);
					}
					else
					{
						test = (item.Value.ToLower() == PreviousWord.ToLower());
					}
					if (test)
					{
						previtem = item;
						break;
					}
				}
			}

			if (previtem == null)
			{
				foreach (TextBoxAutoSuggestTerm term in terms)
				{
					ACUpdateAutoCompleteListRecursive(term, ref selected);
				}
			}
			else
			{
				// BEGIN: fix bug where completely typed word does not appear in AC list
				ACUpdateAutoCompleteListRecursive(previtem, ref selected);
				// END: fix bug where completely typed word does not appear in AC list

				foreach (TextBoxAutoSuggestTerm term in previtem.AutoSuggestTerms)
				{
					ACUpdateAutoCompleteListRecursive(term, ref selected);
				}
				ACDisplayAutoCompleteList();
			}

			if (wndAC.lst.Items.Count == 0) wndAC.Hide();
		}
		private void ACDisplayAutoCompleteList()
		{
			if (wndAC.lst.Items.Count > 0)
			{
				if (!wndAC.Visible)
				{
					Point pt = GetCaretRectangle().Location;
					int charIndex = GetCharIndexFromCharPosition(pt);
					charIndex -= CurrentWord.Length;
					pt = GetPhysicalPositionFromCharIndex(charIndex);

					pt.Y += Font.Height;

					wndAC.parent = this;
					wndAC.Location = PointToScreen(pt);
					wndAC.Show(this);
				}
			}
		}

		private void ACUpdateAutoCompleteListRecursive(TextBoxAutoSuggestTerm term, ref bool selected)
		{
			if (term is TextBoxAutoSuggestTermItem)
			{
				TextBoxAutoSuggestTermItem item = (term as TextBoxAutoSuggestTermItem);
				if (!mvarAutoSuggestFilter || item.Value.ToLower().StartsWith(PreviousWord.ToLower()))
				{
					wndAC.lst.Items.Add(item);
				}

				bool test = false;
				if (mvarCaseSensitive)
				{
					test = item.Value.StartsWith(PreviousWord);
				}
				else
				{
					test = item.Value.ToLower().StartsWith(PreviousWord.ToLower());
				}

				if (!selected && !String.IsNullOrEmpty(PreviousWord) && test)
				{
					wndAC.lst.SelectedItem = item;
					selected = true;
				}
			}
			else if (term is TextBoxAutoSuggestTermGroup)
			{
				TextBoxAutoSuggestTermGroup group = (term as TextBoxAutoSuggestTermGroup);
				if (group.Prefixes.Count > 0)
				{
					if (Selections.Count > 0)
					{
						foreach (string prefix in group.Prefixes)
						{
							int selstart = GetCharIndexFromSelection(Selections[0]);
							string text = Text.Substring(0, selstart);
							if (text.Contains(" ")) text = text.Substring(0, text.LastIndexOf(" "));

							if (prefix == String.Empty || text.EndsWith(prefix))
							{
								foreach (TextBoxAutoSuggestTerm term1 in group.AutoSuggestTerms)
								{
									ACUpdateAutoCompleteListRecursive(term1, ref selected);
								}
								break;
							}
						}
					}
				}
				else
				{
					foreach (TextBoxAutoSuggestTerm term1 in group.AutoSuggestTerms)
					{
						ACUpdateAutoCompleteListRecursive(term1, ref selected);
					}
				}
			}
		}

		internal bool ACAcceptAutoCompleteList()
		{
			if (wndAC.Visible)
			{
				TextBoxAutoSuggestTermItem term = (wndAC.lst.SelectedItem as TextBoxAutoSuggestTermItem);
				if (term != null)
				{
					TextBoxSelection sel = mvarSelections[0];
					if (sel != null)
					{
						int cursel = GetCharIndexFromSelection(sel);
						int selstart = cursel - PreviousWord.Length, sellength = PreviousWord.Length;

						InsertText(term.Value, selstart, sellength);
					}
				}
				wndAC.Visible = false;
				return true;
			}
			return false;
		}
		#endregion
		#region Public API methods
		#region Position conversion

		public Point GetPhysicalPositionFromCharPosition(Point pt)
		{
			return GetPhysicalPositionFromCharPosition(pt.Y, pt.X);
		}
		public Point GetPhysicalPositionFromCharPosition(int row, int column)
		{
			int _x = 0, _y = 0;
			int offsetX = 2, offsetY = 2;
			int cx = offsetX, cy = offsetY;
			int lastHeight = 0;

			for (int i = 0; i < Text.Length; i++)
			{
				if ((row == 0 && column == 0) || ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString)))
				{
					// new line
					_x = 0;
					cx = offsetX;

					// TODO: get largest font height for this line
					_y++;

					// use the largest font height for this line
					cy += lastHeight;
					lastHeight = 0;
					i += (mvarLineSeparatorString.Length - 1);
				}
				else
				{
					Font font = base.Font;
					Size sz = TextRenderer.MeasureText(Text[i].ToString(), font);
					if (lastHeight < sz.Height) lastHeight = sz.Height;

					_x++;
					cx += sz.Width + widthModifier;
				}
				if (_x == column && _y == row)
				{
					return new Point(cx, cy);
				}
			}
			return new Point(cx, cy);
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

			int c = 0;
			for (int i = 0; i < charIndex; i++)
			{
				if (i + mvarLineSeparatorString.Length <= Text.Length)
				{
					if (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString)
					{
						currentLineIndex++;
						c = 0;
						if (currentLineIndex >= lines.Length)
						{
							break;
						}

						x = 2;

						y += greatestHeight;
						greatestHeight = Font.Height;
						i += mvarLineSeparatorString.Length - 1;
						continue;
					}
				}
				int lastHeight = TextRenderer.MeasureText(Text[i].ToString(), font).Height;
				if (c < lines[currentLineIndex].Length)
				{
					if (lastHeight > greatestHeight)
					{
						greatestHeight = lastHeight;
					}
				}

				char chr = Text[i];
				x += TextRenderer.MeasureText(chr.ToString(), font).Width + widthModifier;
				c++;
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
			int dwx = 0;

			for (int i = 0; i < Text.Length; i++)
			{
				Size sz = TextRenderer.MeasureText(Text[i].ToString(), font);
				if (x >= 0 && x <= 2 && y >= _y && y <= (_y + sz.Height))
				{
					return 0;
				}
				else
				{
					if (i + mvarLineSeparatorString.Length <= Text.Length)
					{
						if (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString)
						{
							currentLineIndex++;
							if (currentLineIndex >= lines.Length)
							{
								break;
							}
							if (y >= _y && y <= (_y + sz.Height))
							{
								dwx = i;
							}

							_x = 2;
							_y += greatestHeight;
							greatestHeight = Font.Height;
							i += mvarLineSeparatorString.Length - 1;
							continue;
						}
					}


					int lastHeight = sz.Height;
					if (lastHeight > greatestHeight) greatestHeight = lastHeight;

					if (x >= _x && x <= (_x + sz.Width) && y >= _y && y <= (_y + sz.Height))
					{
						return i + 1;
					}
					else if (y >= _y && y <= (_y + sz.Height))
					{
						dwx = i + 1;
					}
				}
				_x += (sz.Width + widthModifier);
			}
			return Text.Length;
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
			
			string value = lines[row];


			return new System.Drawing.Point(x, y);
		}

		public int GetCharIndexFromSelection(TextBoxSelection sel)
		{
			if (sel is TextBoxLinearSelection)
			{
				return (sel as TextBoxLinearSelection).Start;
			}
			else if (sel is TextBoxRectangularSelection)
			{
				return GetCharIndexFromCharPosition((sel as TextBoxRectangularSelection).Start);
			}
			return -1;
		}

		public Point GetPhysicalPositionFromSelection(TextBoxSelection sel)
		{
			return GetPhysicalPositionFromCharIndex(GetCharIndexFromSelection(sel));
		}

		public int GetCharIndexFromCharPosition(Point pt)
		{
			return GetCharIndexFromCharPosition(pt.Y, pt.X);
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
			return Text.Length;
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
			return new Point(column, row);
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
		public void InsertText(string Text, bool updateSelection = true)
		{
			List<TextBoxSelection> sels = new List<TextBoxSelection>();
			foreach (TextBoxSelection sel in mvarSelections)
			{
				sels.Add(sel);
			}
			foreach (TextBoxSelection sel in sels)
			{
				int start = 0;
				int end = 0;
				
				if (sel is TextBoxLinearSelection)
				{
					start = (sel as TextBoxLinearSelection).Start;
					end = (sel as TextBoxLinearSelection).End;
				}
				else if (sel is TextBoxRectangularSelection)
				{
					start = GetCharIndexFromCharPosition((sel as TextBoxRectangularSelection).Start);
					end = GetCharIndexFromCharPosition((sel as TextBoxRectangularSelection).End);
				}

				int length = end - start;
				if (end == 0) length = 0;
				
				if (String.IsNullOrEmpty(base.Text))
				{
					start = 0;
					length = 0;
				}
				InsertText(Text, start, length, updateSelection);
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
		/// <param name="UpdateSelection">If true, updates the <see cref="TextBoxControl" /> selection start and length.</param>
		public void InsertText(string Text, int SelectionStart, int SelectionLength, bool UpdateSelection)
		{
			string textToInsert = Text;
			int textToInsertOffset = 0;

			foreach (TextBoxTextReplacement replacement in mvarTextReplacements)
			{
				if (replacement.SearchQuery == Text)
				{
					textToInsertOffset = replacement.CursorOffset;
					textToInsert = replacement.ReplacementValue;
					break;
				}
			}

			int selStart = SelectionStart;
			if (selStart > base.Text.Length || selStart < 0) selStart = 0;

			int selLen = SelectionLength;
			if (mvarEnableOverwrite && selLen == 0) selLen = 1;
			if (selStart + selLen > base.Text.Length) selLen = 0;

			string Before = base.Text.Substring(0, selStart);
			string After = base.Text.Substring(selStart + selLen);

			string text = Before + textToInsert + After;

			if (UpdateSelection)
			{
				foreach (TextBoxSelection sel in mvarSelections)
				{
					if (sel is TextBoxLinearSelection)
					{
						TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
						int x = lsel.Start;
						if (x < 0) x = 0;

						lsel.Start = SelectionStart + textToInsert.Length + textToInsertOffset;
						lsel.Length = 0;
					}
					else if (sel is TextBoxRectangularSelection)
					{
						TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
						int x = rsel.Start.X;
						int y = rsel.Start.Y;
						if (x < 0) x = 0;
						if (y < 0) y = 0;


						if (textToInsert == mvarLineSeparatorString)
						{
							rsel.Start = new Point(x, y + 1);
						}
						else
						{
							rsel.Start = new Point(x + textToInsert.Length + textToInsertOffset, y);
						}
						rsel.Length = new Size(0, 0);
					}
				}
			}
			this.Text = text;

			Invalidate();
			mvarCurrentWord = null;
			mvarNextWord = null;
			mvarPreviousWord = null;
		}

		/// <summary>
		/// Deletes one character to the right of the current cursor position. Does nothing if there is no text to the right of the
		/// current cursor position.
		/// </summary>
		public void DeleteText()
		{
			foreach (TextBoxSelection sel in mvarSelections)
			{
				if (sel is TextBoxLinearSelection)
				{
					TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
					int start = lsel.Start;
					int end = lsel.End;
					int length = end - start;
					if (end == 0) length = 0;

					if (start < 0) return;

					if (length == 0)
					{
						length++;
					}

					DeleteText(start, length);
					lsel.Start = lsel.Start;
					lsel.Length = 0;

					Invalidate();
				}
				else if (sel is TextBoxRectangularSelection)
				{
					TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
					int start = GetCharIndexFromCharPosition(rsel.Start);
					int end = GetCharIndexFromCharPosition(rsel.End);
					int length = end - start;
					if (end == 0) length = 0;

					if (start < 0) return;
					DeleteText(start + 1, length);
					rsel.Start = new Point(rsel.Start.X, rsel.Start.Y);
					rsel.Length = new Size(0, 0);

					Invalidate();
				}
			}
		}
		public void DeleteText(int SelectionStart, int SelectionLength)
		{
			int end = SelectionStart;
			if (end < 0) end = 0;
			string before = Text.Substring(0, end);
			if (SelectionStart >= Text.Length) SelectionStart = Text.Length - 1;

			string after = String.Empty;
			if (SelectionLength <= 0)
			{
				// this is what's bugging up -----------------------------------------v (-1 here?)
				if (this.SelectionStart == Text.Length)
				{
					after = Text.Substring(SelectionStart, Text.Length - SelectionStart - 1);
				}
				else
				{
					after = Text.Substring(SelectionStart, Text.Length - SelectionStart);
				}
			}
			else
			{
				after = Text.Substring(SelectionStart + SelectionLength);
			}
			Text = before + after;
			mvarCurrentWord = null;
			mvarNextWord = null;
		}
		#endregion
		#endregion
		#region Properties

		public int SelectionStart
		{
			get
			{
				if (mvarSelections.Count < 1) return -1;
				return GetCharIndexFromSelection(mvarSelections[0]);
			}
			set
			{
				if (mvarSelections.Count > 0)
				{
					if (mvarSelections[0] is TextBoxRectangularSelection)
					{
						(mvarSelections[0] as TextBoxRectangularSelection).Start = GetCharPositionFromCharIndex(value);
					}
					else if (mvarSelections[0] is TextBoxLinearSelection)
					{
						(mvarSelections[0] as TextBoxLinearSelection).Start = value;
					}
				}
				else
				{
					mvarSelections.Add(value, 0);
				}
			}
		}

		#region Character/Word Spacing
		private int mvarCharacterSpacing = 0;
		public int CharacterSpacing { get { return mvarCharacterSpacing; } set { mvarCharacterSpacing = value; } }
		private int mvarWordSpacing = 0;
		public int WordSpacing { get { return mvarWordSpacing; } set { mvarWordSpacing = value; } }
		#endregion

		private string mvarPlaceholderText = String.Empty;
		public string PlaceholderText { get { return mvarPlaceholderText; } set { mvarPlaceholderText = value; } }

		private TextBoxTextReplacement.TextBoxTextReplacementCollection mvarTextReplacements = new TextBoxTextReplacement.TextBoxTextReplacementCollection();
		public TextBoxTextReplacement.TextBoxTextReplacementCollection TextReplacements { get { return mvarTextReplacements; } }

		private string mvarPreviousWord = null;
		/// <summary>
		/// Gets the word in the text box before the current word.
		/// </summary>
		public string PreviousWord
		{
			get
			{
				if (mvarPreviousWord == null)
				{
					int ci = SelectionStart - 1;
					if (ci < 0) ci = 0;
					int idx = Text.LastIndexOf(' ', ci);
					string text = Text;
					if (idx > -1) text = Text.Substring(idx);
					text = text.Trim();

					idx = text.IndexOfAny(new char[] { ' ', '\r', '\n' });
					if (idx > -1)
					{
						text = text.Substring(0, idx).Trim();
					}

					mvarPreviousWord = text;
				}
				return mvarPreviousWord;
			}
		}

		private string mvarCurrentWord = null;
		/// <summary>
		/// Gets the last word typed in the text box.
		/// </summary>
		public string CurrentWord
		{
			get
			{
				if (mvarCurrentWord == null)
				{
					TextBoxSelection sel = null;
					if (mvarSelections.Count > 0) sel = mvarSelections[0];

					int selstart = 0;
					if (sel != null) selstart = GetCharIndexFromSelection(sel);

					if (selstart < 0) selstart = 0;
					if (selstart >= Text.Length) selstart = Text.Length - 1;

					if (String.IsNullOrEmpty(Text)) return String.Empty;

					bool found = false;
					foreach (string wbs in mvarWordBreakingSequences)
					{
						int i = Text.LastIndexOf(wbs, selstart);
						if (i < 0)
						{
							continue;
						}
						i++;
						selstart++;

						string word = null;
						if (selstart - i > 0)
						{
							word = Text.Substring(i);
						}
						else
						{
							word = String.Empty;
							// word = Text.Substring(i, Text.Length - i);
						}
						mvarCurrentWord = word;
						found = true;
						break;
					}

					if (!found)
					{
						mvarCurrentWord = Text;
					}
				}
				return mvarCurrentWord;
			}
		}

		private string mvarNextWord = null;
		/// <summary>
		/// Gets the word to the right of the caret.
		/// </summary>
		public string NextWord
		{
			get
			{
				if (mvarNextWord == null)
				{
					TextBoxSelection sel = null;
					if (mvarSelections.Count > 0) sel = mvarSelections[0];

					int selstart = 0;
					if (sel != null) selstart = GetCharIndexFromSelection(sel);

					int m = selstart - 1;
					if (m < 0) m = 0;
					if (m >= Text.Length) m = Text.Length - 1;

					bool found = false;
					foreach (string wbs in mvarWordBreakingSequences)
					{
						int i = Text.IndexOf(wbs, m);
						if (i < 0)
						{
							continue;
						}
						else
						{
							i++;
						}

						string word = null;
						if (selstart + i < Text.Length)
						{
							int l = selstart + i + 1;
							if (l >= Text.Length)
							{
								word = Text.Substring(selstart);
							}
							else
							{
								word = Text.Substring(selstart, i);
							}
						}
						else
						{
							word = String.Empty;
							// word = Text.Substring(i, Text.Length - i);
						}
						mvarNextWord = word;
						found = true;
						break;
					}

					if (!found)
					{
						mvarNextWord = Text.Substring(0, selstart);
					}
				}
				return mvarNextWord;
			}
		}

		private TextBoxSelection.TextBoxSelectionCollection mvarSelections = null;
		public TextBoxSelection.TextBoxSelectionCollection Selections { get { return mvarSelections; } }

		private bool mvarAcceptReturn = true;
		public bool AcceptReturn { get { return mvarAcceptReturn; } set { mvarAcceptReturn = value; } }

		private bool mvarEnableSelection = true;
		public bool EnableSelection { get { return mvarEnableSelection; } set { mvarEnableSelection = value; } }

		private bool mvarEnableMultiSelection = true;
		/// <summary>
		/// Determines whether the user can select multiple segments of text at .
		/// </summary>
		public bool EnableMultiSelection { get { return mvarEnableMultiSelection; } set { mvarEnableMultiSelection = value; } }

		private bool mvarCaseSensitive = true;
		/// <summary>
		/// Determines if case sensitivity should be taken into account for features like syntax
		/// highlighting and auto suggestion.
		/// </summary>
		public bool CaseSensitive { get { return mvarCaseSensitive; } set { mvarCaseSensitive = value; } }

		private bool mvarMultiline = false;
		public bool Multiline { get { return mvarMultiline; } set { mvarMultiline = value; } }

		private System.Windows.Forms.RichTextBoxScrollBars mvarScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
		public System.Windows.Forms.RichTextBoxScrollBars ScrollBars { get { return mvarScrollBars; } set { mvarScrollBars = value; } }

		private bool mvarHideSelection = true;
		public bool HideSelection { get { return mvarHideSelection; } set { mvarHideSelection = value; } }

		private TextBoxFormatting.TextBoxFormattingCollection mvarFormatting = new TextBoxFormatting.TextBoxFormattingCollection();
		public TextBoxFormatting.TextBoxFormattingCollection Formatting { get { return mvarFormatting; } }
		#endregion
		#region Outlining

		private bool mvarEnableOutlining = false;
		public bool EnableOutlining { get { return mvarEnableOutlining; } set { mvarEnableOutlining = value; } }

		#endregion
		#region Syntax Highlighting
		private bool mvarEnableSyntaxHighlight = false;
		public bool EnableSyntaxHighlight { get { return mvarEnableSyntaxHighlight; } set { mvarEnableSyntaxHighlight = value; } }

		private TextBoxSyntaxHighlightObject.TextBoxSyntaxHighlightObjectCollection mvarSyntaxHighlightObjects = new TextBoxSyntaxHighlightObject.TextBoxSyntaxHighlightObjectCollection();
		public TextBoxSyntaxHighlightObject.TextBoxSyntaxHighlightObjectCollection SyntaxHighlightObjects { get { return mvarSyntaxHighlightObjects; } }
		#endregion
		#region Caret
		private bool mvarEnableCaret = true;
		public bool EnableCaret { get { return mvarEnableCaret; } set { mvarEnableCaret = value; RefreshCaret(); } }

		private int mvarCaretSize = 1;
		public int CaretSize { get { return mvarCaretSize; } set { mvarCaretSize = value; RefreshCaret(); } }

		private System.Windows.Forms.Orientation mvarCaretOrientation = System.Windows.Forms.Orientation.Vertical;
		public System.Windows.Forms.Orientation CaretOrientation { get { return mvarCaretOrientation; } set { mvarCaretOrientation = value; RefreshCaret(); } }

		private bool m_DisplayCaret = false;

		private bool mvarEnableCaretBlink = true;
		public bool EnableCaretBlink { get { return mvarEnableCaretBlink; } set { mvarEnableCaretBlink = value; RefreshCaret(); } }

		private int mvarCaretBlinkInterval = System.Windows.Forms.SystemInformation.CaretBlinkTime;

		public int CaretBlinkInterval { get { return mvarCaretBlinkInterval; } set { mvarCaretBlinkInterval = value; tmrCaret.Interval = value; } }

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
		public Rectangle GetCaretRectangle(TextBoxSelection sel = null)
		{
			Point position = new Point(2, 2);
			if (sel == null && mvarSelections.Count > 0)
			{
				sel = mvarSelections[mvarSelections.Count - 1];
			}
			if (sel != null)
			{
				position = GetPhysicalPositionFromSelection(sel);
			}
			Size size = Size.Empty;
			switch (mvarCaretOrientation)
			{
				case System.Windows.Forms.Orientation.Horizontal:
				{
					size = new Size(mvarCaretSize, base.Font.Height);
					break;
				}
				case System.Windows.Forms.Orientation.Vertical:
				{
					size = new Size(mvarCaretSize, base.Font.Height);
					break;
				}
			}
			position.X += size.Width;
			return new Rectangle(position, size);
		}

		public void RefreshCaret()
		{
			foreach (TextBoxSelection sel in mvarSelections)
			{
				Invalidate(GetCaretRectangle(sel));
			}
		}
		#endregion
		#region Event Handlers
		#region Mouse Events
		protected virtual void OnMouseDoubleDown(System.Windows.Forms.MouseEventArgs e)
		{
			// get the location
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				if (mvarSelections.Count > 0)
				{
					TextBoxLinearSelection sel = (mvarSelections[0] as TextBoxLinearSelection);
					if (sel == null) return;

					int start = sel.Start;
					
					string preText = Text.Substring(0, start);
					string postText = Text.Substring(start);

					int offset = preText.LastIndexOfAny(new char[] { ' ', mvarLineSeparatorString[0] });
					int endoffset = postText.IndexOfAny(new char[] { ' ', mvarLineSeparatorString[0] });
					if (offset > -1)
					{
						offset = preText.Length - offset;
					}
					else
					{
						offset = start + 1;
					}

					start -= offset;

					if (endoffset > -1)
					{
						endoffset += preText.Length;
					}

					int end = endoffset;
					if (endoffset < 0) end = Text.Length;

					sel.Start = start + 1;
					sel.End = end;

					m_selectStart = sel.Start;
					m_Selecting = TextBoxSelectionMode.Word;

					Refresh();
				}
			}
		}

		private Point ptSelect = new Point(0, 0);
		private int m_selectStart = 0;

		private DateTime dtLastClickTime = DateTime.Now;
		private Point ptLastClickPoint = Point.Empty;

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (DateTime.Now.Subtract(dtLastClickTime).TotalMilliseconds <= SystemInformation.DoubleClickTime)
			{
				Point pt1 = e.Location;
				Point pt2 = ptLastClickPoint;

				if ((Math.Abs(pt1.X - pt2.X) < SystemInformation.DoubleClickSize.Width) && (Math.Abs(pt1.Y - pt2.Y) < SystemInformation.DoubleClickSize.Height))
				{
					OnMouseDoubleDown(e);
					return;
				}
			}
			else
			{
				dtLastClickTime = DateTime.Now;
				ptLastClickPoint = e.Location;
			}

			bool clearSelection = true;
			if (mvarEnableMultiSelection && ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) != System.Windows.Forms.Keys.Control))
			{
				// Clear the selections if (we are not holding the CTRL key and multi-select is
				// enabled) and we are not holding the right mouse button

				if (mvarSelections.Count > 0)
				{
					if (mvarSelections[0] is TextBoxLinearSelection)
					{
						int ci = GetCharIndexFromPhysicalPosition(e.Location);
						if (ci > -1)
						{
							if ((mvarSelections[0] as TextBoxLinearSelection).Contains(ci))
							{
								clearSelection = false;
							}
						}
					}
				}
				if (clearSelection)
				{
					mvarSelections.Clear();
				}
			}

			if (clearSelection)
			{
				if ((ModifierKeys & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt)
				{
					Point ptStartNew = GetCharPositionFromPhysicalPosition(e.Location);
					ptSelect = ptStartNew;
					mvarSelections.Add(ptStartNew, Size.Empty);
				}
				else
				{
					m_selectStart = GetCharIndexFromPhysicalPosition(e.Location);
					mvarSelections.Add(m_selectStart, 0);
				}
			}

			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				m_Selecting = TextBoxSelectionMode.Character;
			}
			m_DisplayCaret = true;

			Invalidate();
			Focus();
		}
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (mvarEnableSelection && (m_Selecting != TextBoxSelectionMode.None))
			{
				TextBoxSelection sel = mvarSelections[mvarSelections.Count - 1];
				if (sel is TextBoxRectangularSelection)
				{
					if ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt)
					{
						TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
				
						Point pt = e.Location;
						if (pt.X < Left) pt.X = 0;
						if (pt.Y < Top) pt.Y = 0;
						Point ptEndNew = GetCharPositionFromPhysicalPosition(pt);

						if (m_Selecting == TextBoxSelectionMode.Word)
						{
							int a = GetCharIndexFromCharPosition(ptEndNew);
							string b4 = Text.Substring(0, a);

							foreach (string w in mvarWordBreakingSequences)
							{
								if (!(b4.EndsWith(w) || a <= 0 || a >= Text.Length)) return;
							}
						}

						if (ptSelect.X > ptEndNew.X || ptSelect.Y > ptEndNew.Y)
						{
							rsel.Start = ptEndNew;
							rsel.End = ptSelect;
						}
						else
						{
							rsel.Start = ptSelect;
							rsel.End = ptEndNew;
						}
					}
				}
				else if (sel is TextBoxLinearSelection)
				{
					TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
					
					Point pt = e.Location;
					if (pt.X < Left) pt.X = 0;
					if (pt.Y < Top) pt.Y = 0;

					int end = GetCharIndexFromPhysicalPosition(pt);

					if (m_Selecting == TextBoxSelectionMode.Word)
					{
						int a = end;
						string b4 = Text.Substring(0, a);

						foreach (string w in mvarWordBreakingSequences)
						{
							if (!(b4.EndsWith(w) || a <= 0 || a >= Text.Length)) return;
						}
					}

					if (m_selectStart > end)
					{
						lsel.Start = end;
						lsel.End = m_selectStart;
					}
					else
					{
						lsel.Start = m_selectStart;
						lsel.End = end;
					}
				}
				Refresh();
			}
		}
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			base.OnMouseUp(e);
			/*
			if (m_Selecting != TextBoxSelectionMode.None)
			{
				foreach (TextBoxSelection sel in mvarSelections)
				{
					if (sel is TextBoxLinearSelection)
					{
						int charIndex = GetCharIndexFromPhysicalPosition(e.Location);
						TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
						if (lsel.Contains(charIndex))
						{
							lsel.Length = 0;
						}
					}
				}
			}
			*/
			

			bool clearSelection = false;
			if (mvarEnableMultiSelection && ((System.Windows.Forms.Control.ModifierKeys & System.Windows.Forms.Keys.Control) != System.Windows.Forms.Keys.Control))
			{
				// Clear the selections if (we are not holding the CTRL key and multi-select is
				// enabled) and we are not holding the right mouse button
				if (mvarSelections.Count > 0)
				{
					if (mvarSelections[0] is TextBoxLinearSelection)
					{
						int ci = GetCharIndexFromPhysicalPosition(e.Location);
						if (ci > -1)
						{
							if ((mvarSelections[0] as TextBoxLinearSelection).Contains(ci))
							{
								//clearSelection = true;
							}
						}
					}
				}
			}
			if (clearSelection)
			{
				mvarSelections.Clear();
				if ((ModifierKeys & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt)
				{
					Point ptStartNew = GetCharPositionFromPhysicalPosition(e.Location);
					ptSelect = ptStartNew;
					mvarSelections.Add(ptStartNew, Size.Empty);
				}
				else
				{
					m_selectStart = GetCharIndexFromPhysicalPosition(e.Location);
					mvarSelections.Add(m_selectStart, 0);
				}
			}

			m_Selecting = TextBoxSelectionMode.None;

			if (e.Button == System.Windows.Forms.MouseButtons.Right && base.ContextMenuStrip == null && base.ContextMenu == null)
			{
				mnuContext.Show(this, e.Location);
			}
		}
		#endregion
		#region Keyboard Events
		private string nextAltCode = String.Empty;
		private bool extendedAltCode = false;

		private bool mvarAutoIndentEnabled = true;
		/// <summary>
		/// Determines whether auto-indent is enabled. When enabled, a new line will
		/// automatically indent to the same amount of space as the previous line.
		/// </summary>
		public bool AutoIndentEnabled { get { return mvarAutoIndentEnabled; } set { mvarAutoIndentEnabled = value; } }

		protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Handled) return;

			if (mvarAutoIndentEnabled)
			{
				if (e.KeyCode == System.Windows.Forms.Keys.Enter)
				{
					// handle auto-indent
					if (mvarReplaceTabsWithSpaces)
					{
						int aiStart = 0;
						string aiCurrentLine = Lines[CurrentLineIndex];
						for (aiStart = 0; aiStart < Lines[CurrentLineIndex].Length; aiStart++)
						{
							if (aiCurrentLine[aiStart] != ' ')
							{
								break;
							}
						}
						if (aiStart > 0)
						{
							InsertText(mvarLineSeparatorString + new string(' ', aiStart));
							return;
						}
					}
					else
					{
						int aiStart = 0;
						string aiCurrentLine = Lines[CurrentLineIndex];
						for (aiStart = 0; aiStart < Lines[CurrentLineIndex].Length; aiStart++)
						{
							if (aiCurrentLine[aiStart] != '\t')
							{
								break;
							}
						}
						if (aiStart > 0)
						{
							InsertText(mvarLineSeparatorString + new string('\t', aiStart));
							return;
						}
					}
				}
			}

			if (extendedAltCode)
			{
				e.Handled = true;
			}
			if (!e.Alt)
			{
				e.SuppressKeyPress = true;
			}

			if (mvarAutoSuggestAcceptKeys.Count > 0)
			{
				foreach (System.Windows.Forms.Keys keys in mvarAutoSuggestAcceptKeys)
				{
					if (keys == e.KeyData)
					{
						if (ACAcceptAutoCompleteList())
						{
							if (mvarAutoSuggestInhibitKeys.Contains(keys)) return;
						}
						else
						{
							break;
						}
					}
				}
			}

			switch (e.KeyCode)
			{
				case System.Windows.Forms.Keys.Escape:
				{
					wndAC.Visible = false;
					break;
				}
				#region Function Keys
				case System.Windows.Forms.Keys.F1:
				case System.Windows.Forms.Keys.F2:
				case System.Windows.Forms.Keys.F3:
				case System.Windows.Forms.Keys.F4:
				case System.Windows.Forms.Keys.F5:
				case System.Windows.Forms.Keys.F6:
				case System.Windows.Forms.Keys.F7:
				case System.Windows.Forms.Keys.F8:
				case System.Windows.Forms.Keys.F9:
				case System.Windows.Forms.Keys.F10:
				case System.Windows.Forms.Keys.F11:
				case System.Windows.Forms.Keys.F12:
				{
					break;
				}
				#endregion
				#region Modifier Keys, these should be ignored
				case System.Windows.Forms.Keys.CapsLock:
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
				#endregion
				case System.Windows.Forms.Keys.Enter:
				{
					if (mvarAcceptReturn)
					{
						e.SuppressKeyPress = true;
						e.Handled = true;
						InsertText(mvarLineSeparatorString);
					}
					break;
				}
				#region Delete/Backspace
				case System.Windows.Forms.Keys.Back:
				{
					if (Text.Length <= 0)
					{
						// System.Media.SystemSounds.Beep.Play();
						return;
					}

					foreach (TextBoxSelection sel in mvarSelections)
					{
						if (sel is TextBoxLinearSelection)
						{
							TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
							int start = lsel.Start;
							int end = lsel.End;
							int length = lsel.Length;
							if (end == 0) length = 0;

							if (start < 0) return;

							if (e.Control)
							{
								List<int> indices = new List<int>();
								foreach (string wbs in mvarWordBreakingSequences)
								{
									int len = (start - wbs.Length - 1);
									if (len >= 0 && len < Text.Length)
									{
										int i = Text.LastIndexOf(wbs, len);
										if (i <= -1) continue;
										i++;
										indices.Add(i);
									}
								}

								if (indices.Count > 0)
								{
									indices.Sort();

									length += (start - indices[indices.Count - 1]);
									start = indices[indices.Count - 1];
								}
								else
								{
									length = start; // Text.Length;
									start = 0;
								}
							}

							bool isTab = false;
							if (mvarReplaceTabsWithSpaces)
							{
								string tabString = new string(' ', mvarTabSize);
								if (Text.Length - start >= mvarTabSize && start - mvarTabSize >= 0)
								{
									if (Text.Substring(start - mvarTabSize, mvarTabSize) == tabString)
									{
										length = mvarTabSize;
										start -= mvarTabSize;
										isTab = true;
									}
								}
							}

							if (!isTab)
							{
								if (length == 0)
								{
									start--;
									length = 1;
								}

								if (start >= Text.Length) start = Text.Length - 1;

								if (Text.Contains(mvarLineSeparatorString))
								{
									if (start + 1 >= mvarLineSeparatorString.Length)
									{
										if (Text.Substring(start - mvarLineSeparatorString.Length + 1, mvarLineSeparatorString.Length) == mvarLineSeparatorString)
										{
											// handle the special case when the cursor is immediately after a line separator...
											// TODO: need to handle cursor immediately BEFORE line separators for "delete" key too!!
											length = mvarLineSeparatorString.Length;
											start -= (mvarLineSeparatorString.Length - 1);
										}
									}
								}
							}

							DeleteText(start, length);
							lsel.Start = start;
							lsel.Length = 0;
						}
						else if (sel is TextBoxRectangularSelection)
						{
							TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
							int start = GetCharIndexFromCharPosition(rsel.Start);
							int end = GetCharIndexFromCharPosition(rsel.End);
							int length = end - start;
							if (end == 0) length = 0;

							if (start < 0) return;

							if (e.Control)
							{
								int i = Text.LastIndexOfAny(new char[] { ' ', '\r', '\n' }, start - 2) + 1;
								length += (start - i - 1);
								start = i + 1;
							}

							DeleteText(start, length);
							rsel.Start = new Point(rsel.Start.X - (length + 1), rsel.Start.Y);
							rsel.Length = new Size(0, 0);
						}
					}
					Invalidate();

					ACUpdateAutoCompleteList();
					ACDisplayAutoCompleteList();
					break;
				}
				case System.Windows.Forms.Keys.Delete:
				{
					if (Text.Length <= 0)
					{
						// System.Media.SystemSounds.Beep.Play();
						return;
					}

					if (e.Control)
					{
						// If CTRL is pressed, delete the entire word (separated by <see cref="WordBreakingSequences" />) to the right
						// of the current cursor position.
						int start = GetCharIndexFromSelection(mvarSelections[0]);
						int length = -1;
						foreach (string wbs in mvarWordBreakingSequences)
						{
							length = Text.IndexOf(wbs, start) - (start - 1);
							if (length == -1) continue;
							break;
						}
						// here are a few words
						if (length == -1) length = Text.Length - start;

						DeleteText(start, length);
					}
					else
					{
						DeleteText();
					}
					break;
				}
				#endregion
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
				case System.Windows.Forms.Keys.Home:
				{
					if (e.Control || !Text.Contains(mvarLineSeparatorString))
					{
						mvarSelections.Clear();
						mvarSelections.Add(0, 0);
						/*
						foreach (TextBoxSelection sel in mvarSelections)
						{
							int oldstart = sel.Start.X;
							sel.Start = new Point(0, sel.Start.Y);

							if (e.Shift)
							{
								sel.Length = new Size(oldstart, 0);
							}
							else
							{
								sel.Length = new Size(0, 0);
							}
						}
						*/
					}
					else
					{
						// only home to the start of the line.
						mvarSelections.Clear();

						int idx = Text.LastIndexOf(Environment.NewLine);
						if (idx == -1)
						{
							mvarSelections.Add(0, 0);
						}
						else
						{
							mvarSelections.Add(idx + Environment.NewLine.Length, 0);
						}
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
					Invalidate();
					break;
				}
				case System.Windows.Forms.Keys.End:
				{
					if (e.Control || !Text.Contains(mvarLineSeparatorString))
					{
						if (mvarSelections.Count > 0)
						{
							TextBoxSelection sel = (mvarSelections[mvarSelections.Count - 1]);
							mvarSelections.Clear();

							if (sel is TextBoxRectangularSelection)
							{
								TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
								rsel.Start = new Point(Text.Length, rsel.Start.Y);
								rsel.Length = new Size(0, 0);
							}
							else if (sel is TextBoxLinearSelection)
							{
								TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
								lsel.Start = Text.Length;
								lsel.Length = 0;
							}
							mvarSelections.Add(sel);
						}
					}
					else
					{
						// only end to the end of the line.
						TextBoxLinearSelection sel = (mvarSelections[mvarSelections.Count - 1] as TextBoxLinearSelection);
						if (sel != null)
						{
							int curSel = sel.Start + sel.End;
							mvarSelections.Clear();
							mvarSelections.Add(Text.IndexOf(Environment.NewLine, sel.End), 0);
						}
					}

					if (e.Shift)
					{
					}
					else
					{
						// mvarSelectionLength = 0;
					}

					m_DisplayCaret = true;
					Invalidate();
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
					if (e.Alt)
					{
						nextAltCode += "0";
						break;
					}
					InsertText("0");
					break;
				}
				case System.Windows.Forms.Keys.NumPad1:
				{
					if (e.Alt)
					{
						nextAltCode += "1";
						break;
					}
					InsertText("1");
					break;
				}
				case System.Windows.Forms.Keys.NumPad2:
				{
					if (e.Alt)
					{
						nextAltCode += "2";
						break;
					}
					InsertText("2");
					break;
				}
				case System.Windows.Forms.Keys.NumPad3:
				{
					if (e.Alt)
					{
						nextAltCode += "3";
						break;
					}
					InsertText("3");
					break;
				}
				case System.Windows.Forms.Keys.NumPad4:
				{
					if (e.Alt)
					{
						nextAltCode += "4";
						break;
					}
					InsertText("4");
					break;
				}
				case System.Windows.Forms.Keys.NumPad5:
				{
					if (e.Alt)
					{
						nextAltCode += "5";
						break;
					}
					InsertText("5");
					break;
				}
				case System.Windows.Forms.Keys.NumPad6:
				{
					if (e.Alt)
					{
						nextAltCode += "6";
						break;
					}
					InsertText("6");
					break;
				}
				case System.Windows.Forms.Keys.NumPad7:
				{
					if (e.Alt)
					{
						nextAltCode += "7";
						break;
					}
					InsertText("7");
					break;
				}
				case System.Windows.Forms.Keys.NumPad8:
				{
					if (e.Alt)
					{
						nextAltCode += "8";
						break;
					}
					InsertText("8");
					break;
				}
				case System.Windows.Forms.Keys.NumPad9:
				{
					if (e.Alt)
					{
						nextAltCode += "9";
						break;
					}
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
					if (e.Alt)
					{
						extendedAltCode = true;
						e.Handled = true;
						e.SuppressKeyPress = true;
						return;
					}
					InsertText("+");
					break;
				}
				case System.Windows.Forms.Keys.Decimal:
				{
					InsertText(".");
					break;
				}
				#endregion
				case System.Windows.Forms.Keys.Space:
				{
					InsertText(" ");
					ACUpdateAutoCompleteList();
					break;
				}
				case (System.Windows.Forms.Keys)229:
				{
					e.Handled = true;
					imeReceivedKeyboardEvent = String.Empty;
					break;
				}
				default:
				{
					if (e.Alt)
					{
						if (extendedAltCode && (e.KeyValue >= (int)'A' && e.KeyValue <= (int)'F'))
						{
							nextAltCode += (char)e.KeyValue;
						}
						break;
					}

					if (e.Shift || System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock))
					{
						InsertText(((char)e.KeyValue).ToString().ToUpper());
					}
					else
					{
						InsertText(((char)e.KeyValue).ToString().ToLower());
					}

					if (Selections.Count > 0)
					{
						int selstart = GetCharIndexFromSelection(Selections[0]);
						foreach (string prefix in mvarAutoSuggestInhibitPrefixes)
						{
							int m = 0; // GetFirstCharIndexOfCurrentLine()
							if (Text.Substring(m, Math.Min(Text.Length, prefix.Length)) == prefix)
							{
								return;
							}
						}
					}

					if (mvarAutoSuggestMode == TextBoxAutoSuggestMode.Popup)
					{
						ACUpdateAutoCompleteList();
						ACDisplayAutoCompleteList();

					}
					break;
				}
			}
		}

		private string imeReceivedKeyboardEvent = String.Empty;
		protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
		{
			// base.OnKeyPress(e);
			imeReceivedKeyboardEvent += e.KeyChar.ToString();
		}
		protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.KeyCode == System.Windows.Forms.Keys.Menu)
			{
				// parse the alt code - damn, this was hard work!
				if (nextAltCode == String.Empty) return;

				int iAltCode = 0;
				if (extendedAltCode)
				{
					iAltCode = Int32.Parse(nextAltCode, System.Globalization.NumberStyles.HexNumber);
				}
				else
				{
					iAltCode = Int32.Parse(nextAltCode);
				}
				Encoding encoding = null;
				if (!extendedAltCode)
				{
					if (nextAltCode.StartsWith("0"))
					{
						// we need to use the Windows-1252 code page in order to get a good match
						encoding = System.Text.Encoding.GetEncoding("Windows-1252");
					}
					else
					{
						// for compatibility reasons, if the number doesn't begin with 0, use code page 437
						encoding = System.Text.Encoding.GetEncoding(437);
					}
				}

				// if we cannot find this encoding (for example, if we're on a non-Windows OS)
				// we'll just use the default encoding
				if (encoding == null) encoding = System.Text.Encoding.Unicode;

				string sAltCode = encoding.GetString(BitConverter.GetBytes(iAltCode));
				if (sAltCode.Contains("\0")) sAltCode = sAltCode.Substring(0, sAltCode.IndexOf("\0"));
				sAltCode = sAltCode[0].ToString();

				InsertText(sAltCode);

				nextAltCode = String.Empty;
				extendedAltCode = false;
			}
			else if (e.KeyCode == (System.Windows.Forms.Keys.LButton | System.Windows.Forms.Keys.MButton | System.Windows.Forms.Keys.Back))
			{
				// IME keyboard event has finished. cut the stored IME string in half (because WinForms
				// is stupid and processes IME keyboard events twice)...
				imeReceivedKeyboardEvent = imeReceivedKeyboardEvent.Substring(0, imeReceivedKeyboardEvent.Length / 2);
				// insert the text into the TextBox
				InsertText(imeReceivedKeyboardEvent);
				// and reset the stored IME string for the next time someone uses it.
				imeReceivedKeyboardEvent = String.Empty;
			}
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
					if (wndAC != null && wndAC.Visible)
					{
						if (wndAC.lst.SelectedIndex - 1 >= 0)
						{
							wndAC.lst.SelectedIndex--;
						}
						else
						{
							wndAC.lst.SelectedIndex = wndAC.lst.Items.Count - 1;
						}
					}
					else
					{
						if (CurrentLineIndex - 1 >= 0)
						{
							CurrentLineIndex--;
						}
					}
					return true;
				}
				case System.Windows.Forms.Keys.Down:
				{
					if (wndAC != null && wndAC.Visible)
					{
						if (wndAC.lst.SelectedIndex + 1 < wndAC.lst.Items.Count)
						{
							wndAC.lst.SelectedIndex++;
						}
						else
						{
							wndAC.lst.SelectedIndex = 0;
						}
					}
					else
					{
						if (CurrentLineIndex + 1 < Lines.Length)
						{
							CurrentLineIndex++;
						}
					}
					return true;
				}
				case System.Windows.Forms.Keys.Left:
				{
					TextBoxSelection sel = mvarSelections[mvarSelections.Count - 1];
					mvarSelections.Clear();

					if (sel is TextBoxLinearSelection)
					{
						TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);

						if (ctrl)
						{
							// If CTRL is pressed, skip the entire word (separated by <see cref="WordBreakingSequences" />) to the left
							// of the current cursor position.
							int start = GetCharIndexFromSelection(lsel);
							int length = -1;
							foreach (string wbs in mvarWordBreakingSequences)
							{
								string lookIn = Text.Substring(0, start);
								int i = lookIn.LastIndexOf(wbs);
								if (i == -1) continue;

								length = (start - i - 1);
								if (length == -1) continue;
								break;
							}

							if (length == -1) length = Text.Length - start;
							if (length == 0) length = 1;
							lsel.Start -= length;

							if (!shift)
							{
								lsel.Length = 0;
							}
						}
						else
						{
							if (lsel.Start - 1 >= 0)
							{
								lsel.Start -= 1;

								// Reset the length if we are not holding down the shift key
								if (!shift) lsel.Length = 0;
							}
						}
						mvarSelections.Add(lsel);
					}
					else if (sel is TextBoxRectangularSelection)
					{
						if (shift)
						{
						}
						else
						{

						}
					}

					m_DisplayCaret = true;
					Invalidate();
					return true;
				}
				case System.Windows.Forms.Keys.Right:
				{
					TextBoxSelection sel = mvarSelections[mvarSelections.Count - 1];
					mvarSelections.Clear();

					if (sel is TextBoxLinearSelection)
					{
						TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
						if (lsel.Start < Text.Length)
						{
							if (shift)
							{
								lsel.End += 1;
							}
							else
							{
								lsel.Start += 1;

								// Reset the length if we are not holding down the shift key
								lsel.Length = 0;
							}
						}
					}
					else if (sel is TextBoxRectangularSelection)
					{
						TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
						if (shift)
						{
							if (rsel.Start.X - 1 >= 0)
							{
								Point ptStart = rsel.Start;
								// sel.Start = new Point(sel.Start.X + 1, sel.Start.Y);
								rsel.End = new Point(rsel.End.X + 1, rsel.Start.Y);
								/*
								if (sel.End.X == 0)
								{
									sel.End = new Point(sel.Start.X, sel.Start.Y);
								}
								else
								{
									sel.End = new Point(sel.End.X, sel.End.Y);
								}
								*/
							}
						}
						else
						{
							if (rsel.Start.X - 1 >= 0)
							{
								rsel.Start = new Point(rsel.Start.X + 1, rsel.Start.Y);
								rsel.End = new Point(0, 0);
							}
						}
					}
					mvarSelections.Add(sel);

					m_DisplayCaret = true;
					Invalidate();
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}
		#endregion
		#region Focus
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
		#endregion
		
		public string[] Lines
		{
			get
			{
				return base.Text.Split(new string[] { mvarLineSeparatorString }, StringSplitOptions.None);
			}
		}
		public string CurrentLine
		{
			get
			{
				return Lines[CurrentLineIndex];
			}
		}
		
		protected override void OnTextChanged(EventArgs e)
		{
			Refresh();
			base.OnTextChanged(e);
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
			Rectangle rect = new Rectangle(2, 2, base.Width, base.Height);

			int pX = 0, pY = 0;
			int largestHeight = Font.Height;

			TextBoxSyntaxHighlightObject sho = null;

			for (int i = 0; i < Text.Length; i++)
			{
				Font font = Font;
				Color foreColor = ForeColor;
				Color backColor = Color.Empty;

				if (ForeColor == Color.Empty)
				{
					foreColor = Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedForeground;
				}
				else
				{
					foreColor = ForeColor;
				}

				if ((i + mvarLineSeparatorString.Length < Text.Length) && (Text.Substring(i, mvarLineSeparatorString.Length) == mvarLineSeparatorString))
				{
					// new line
					pX = 0;
					pY++;

					rect.X = 0;
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

				bool inSelection = false;
				foreach (TextBoxSelection sel in mvarSelections)
				{
					if (sel is TextBoxRectangularSelection)
					{
						TextBoxRectangularSelection rsel = (sel as TextBoxRectangularSelection);
						if (rsel.Length.Width > 0 || rsel.Length.Height > 0)
						{
							if (rsel.Contains(pX, pY))
							{
								e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedBackground), rect);
								foreColor = Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedForeground;
								inSelection = true;
								break;
							}
						}
					}
					else if (sel is TextBoxLinearSelection)
					{
						TextBoxLinearSelection lsel = (sel as TextBoxLinearSelection);
						if (lsel.Length > 0)
						{
							if (lsel.Contains(i))
							{
								e.Graphics.FillRectangle(new SolidBrush(Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedBackground), rect);
								foreColor = Theming.Theme.CurrentTheme.ColorTable.FocusedHighlightedForeground;
								inSelection = true;
								break;
							}
						}
					}
				}
				rect.Width -= widthModifier;
				rect.X -= 2;

				#region Syntax Highlighting
				#region Syntax Highlight Object Selection
				if (!inSelection)
				{
					if (sho == null)
					{
						foreach (TextBoxSyntaxHighlightObject sho1 in mvarSyntaxHighlightObjects)
						{
							if (sho1 is TextBoxSyntaxHighlightTerm)
							{
								TextBoxSyntaxHighlightTerm term = (sho1 as TextBoxSyntaxHighlightTerm);
								if (i + term.Value.Length <= Text.Length)
								{
									string cmp = Text.Substring(i, term.Value.Length);
									string cmp2 = term.Value;

									if (!mvarCaseSensitive)
									{
										cmp = cmp.ToLower();
										cmp2 = cmp2.ToLower();
									}
									if (cmp2 == cmp)
									{
										bool found = false;
										// determine if we encounter a reason to stop syntax highlighting for this
										// otherwise valid term
										foreach (string wrd in mvarWordBreakingSequences)
										{
											// if (i == 0) break;
											if (i + cmp.Length + wrd.Length >= Text.Length)
											{
												found = true;
												break;
											}

											if (Text.Substring(i + cmp.Length, wrd.Length) == wrd)
											{
												found = true;
												break;
											}
										}
										if (!found) break;

										sho = sho1;
										break;
									}
								}
							}
						}
					}
				}
				#endregion
				#region Syntax Highlight Style Application
				if (sho != null)
				{
					if (sho is TextBoxSyntaxHighlightTerm)
					{
						TextBoxSyntaxHighlightTerm term = (sho as TextBoxSyntaxHighlightTerm);
						string cmp = Text.Substring(0, i);
						string cmp2 = term.Value;
						if (!mvarCaseSensitive)
						{
							cmp = cmp.ToLower();
							cmp2 = cmp2.ToLower();
						}
						if (cmp.EndsWith(cmp2))
						{
							sho = null;
						}
					}
				}

				if (!inSelection)
				{
					if (sho != null && sho.Group != null)
					{
						foreColor = sho.Group.ForeColor;
						backColor = sho.Group.BackColor;
					}
				}
				#endregion
				#endregion

				foreach (TextBoxFormatting formatting in mvarFormatting)
				{
					if (i >= formatting.Start && i <= formatting.End)
					{
						// apply the formatting
						if ((formatting.Attributes & TextBoxFormattingAttributes.BackColor) == TextBoxFormattingAttributes.BackColor) backColor = formatting.BackColor;
						if ((formatting.Attributes & TextBoxFormattingAttributes.ForeColor) == TextBoxFormattingAttributes.BackColor) foreColor = formatting.ForeColor;
						if ((formatting.Attributes & TextBoxFormattingAttributes.Font) == TextBoxFormattingAttributes.Font) font = formatting.Font;
						// if ((formatting.Attributes & TextBoxFormattingAttributes.Case) == TextBoxFormattingAttributes.Case) font = formatting.Font;
					}
				}

				TextRenderer.DrawText(e.Graphics, Text[i].ToString(), font, rect, foreColor, backColor, System.Windows.Forms.TextFormatFlags.Left | System.Windows.Forms.TextFormatFlags.Top);

				size.Width += widthModifier;

				rect.X += size.Width + mvarCharacterSpacing;
			}

			foreach (TextBoxFormatting formatting in mvarFormatting)
			{
				if ((formatting.Attributes & TextBoxFormattingAttributes.Underline) == TextBoxFormattingAttributes.Underline)
				{
					Point ptStart = GetPhysicalPositionFromCharIndex(formatting.Start);
					Point ptEnd = GetPhysicalPositionFromCharIndex(formatting.End);

					ptStart.Y += largestHeight;
					ptEnd.Y += largestHeight;

					for (int i = 0; i < formatting.UnderlineStyle.Count; i++)
					{
						// get the first point under which to draw the line

						if (formatting.UnderlineStyle.Type == TextBoxFormattingLineType.Wave)
						{
							e.Graphics.DrawWavyLine(new Pen(formatting.UnderlineStyle.Color, formatting.UnderlineStyle.Size), ptStart.X, ptStart.Y, ptEnd.X, ptStart.Y);
						}
						else
						{
							Pen pen = new Pen(formatting.UnderlineStyle.Color);
							switch (formatting.UnderlineStyle.Type)
							{
								case TextBoxFormattingLineType.Solid:
								{
									pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
									break;
								}
								case TextBoxFormattingLineType.Dash:
								{
									pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
									break;
								}
								case TextBoxFormattingLineType.Dot:
								{
									pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
									break;
								}
								case TextBoxFormattingLineType.DotDash:
								{
									pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
									break;
								}
								case TextBoxFormattingLineType.DotDotDash:
								{
									pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
									break;
								}
							}
							e.Graphics.DrawLine(pen, ptStart, ptEnd);
						}
					}
				}
			}
		}
		#endregion


		private void mnuContext_Opening(object sender, CancelEventArgs e)
		{
			mnuContextSep3.Visible = mvarEnableOutlining;
			mnuContextOutlining.Visible = mvarEnableOutlining;
		}

		public int GetLineIndexFromCharIndex(int charIndex)
		{
			int ci = 0;
			for (int i = 0; i < Lines.Length; i++)
			{
				int start = ci;
				ci += Lines[i].Length + mvarLineSeparatorString.Length;
				int end = ci;

				if (charIndex >= start && charIndex <= end) return i;
			}
			return -1;
		}

		private System.Collections.Specialized.StringCollection mvarAutoSuggestInhibitPrefixes = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection AutoSuggestInhibitPrefixes { get { return mvarAutoSuggestInhibitPrefixes; } }

		private System.Collections.Specialized.StringCollection mvarWordBreakingSequences = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection WordBreakingSequences { get { return mvarWordBreakingSequences; } }

		public int CurrentLineIndex
		{
			get
			{
				TextBoxLinearSelection lsel = (mvarSelections[0] as TextBoxLinearSelection);
				if (lsel != null)
				{
					return GetLineIndexFromCharIndex(lsel.Start);
				}
				return -1;
			}
			set
			{
				TextBoxLinearSelection lsel = (mvarSelections[0] as TextBoxLinearSelection);
				if (lsel != null)
				{
					if (value < 0 || value >= Lines.Length) return;

					Point pt = GetCharPositionFromCharIndex(lsel.Start);
					if (pt.X > Lines[value].Length) pt.X = 0;
					pt.Y = value;

					lsel.Start = GetCharIndexFromCharPosition(pt) - 1;
					lsel.End = 0;
					Refresh();
				}
			}
		}

		public void InsertLine()
		{
			string indentString = String.Empty;
			if (mvarAutoIndentEnabled)
			{
				if (mvarReplaceTabsWithSpaces)
				{
					indentString = new string(' ', mvarTabSize);
				}
				else
				{
					indentString = "\t";
				}
			}
			InsertText(mvarLineSeparatorString + indentString);
		}
	}
}
