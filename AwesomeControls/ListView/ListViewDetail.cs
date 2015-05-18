using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AwesomeControls.ListView
{
	public abstract class ListViewDetail : IComparable
	{
		public class ListViewDetailCollection
			: System.Collections.ObjectModel.Collection<ListViewDetail>
		{
			public ListViewDetail Add(string Text)
			{
				ListViewDetailLabel item = new ListViewDetailLabel();
				item.Text = Text;
				Add(item);
				return item;
			}

			public new ListViewDetail this[int index]
			{
				get
				{
					ListViewDetail item = base[index];
					if (item == null)
					{
						ListViewDetailEmpty item1 = new ListViewDetailEmpty();
						Add(item1);
						item = item1;
					}
					return item;
				}
				set
				{
					if (index < Count)
					{
						base[index] = value;
					}
					else
					{
						for (int i = Count - 1; i < index; i++)
						{
							base.Add(new ListViewDetailEmpty());
						}
						base.Add(value);
					}
				}
			}
		}
		private Color mvarBackColor = Color.Empty;
		public Color BackColor { get { return mvarBackColor; } set { mvarBackColor = value; } }

		private Color mvarForeColor = Color.Empty;
		public Color ForeColor { get { return mvarForeColor; } set { mvarForeColor = value; } }

		public abstract int CompareTo(object other);
	}

	public class ListViewDetailEmpty : ListViewDetail
	{
		public override int CompareTo(object other)
		{
			// always equal, since they're both empty
			return 0;
		}
	}
	public class ListViewDetailLabel : ListViewDetail
	{
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		public ListViewDetailLabel()
			: this(String.Empty)
		{
		}
		public ListViewDetailLabel(string text)
		{
			mvarText = text;
		}

		public override int CompareTo(object other)
		{
			if (other is ListViewDetailLabel) return mvarText.CompareTo((other as ListViewDetailLabel).Text);
			return 0;
		}
	}

	public class ListViewDetailChoice : ListViewDetail
	{
		private bool mvarRequireChoice = false;
		public bool RequireChoice { get { return mvarRequireChoice; } set { mvarRequireChoice = value; } }

		private ListViewDetailChoiceOption.ListViewDetailChoiceOptionCollection mvarOptions = new ListViewDetailChoiceOption.ListViewDetailChoiceOptionCollection();
		public ListViewDetailChoiceOption.ListViewDetailChoiceOptionCollection Options { get { return mvarOptions; } }

		public ListViewDetailChoiceOption SelectedOption
		{
			get
			{
				foreach (ListViewDetailChoiceOption option in mvarOptions)
				{
					if (option.Selected) return option;
				}
				return null;
			}
			set
			{
				foreach (ListViewDetailChoiceOption option in mvarOptions)
				{
					if (option == value)
					{
						option.Selected = true;
						break;
					}
				}
			}
		}

		public override int CompareTo(object other)
		{
			return 0;
		}
	}

	public class ListViewDetailChoiceOption
	{
		public class ListViewDetailChoiceOptionCollection
			: System.Collections.ObjectModel.Collection<ListViewDetailChoiceOption>
		{
			public ListViewDetailChoiceOption Add(string text)
			{
				return Add(text, text);
			}
			public ListViewDetailChoiceOption Add(string text, object value)
			{
				ListViewDetailChoiceOption option = new ListViewDetailChoiceOption(text, value);
				Add(option);
				return option;
			}
		}

		public ListViewDetailChoiceOption(string text)
		{
			mvarText = text;
			mvarValue = text;
		}
		public ListViewDetailChoiceOption(string text, object value)
		{
			mvarText = text;
			mvarValue = value;
		}

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

		private bool mvarSelected = false;
		public bool Selected { get { return mvarSelected; } set { mvarSelected = value; } }

		public override string ToString()
		{
			return mvarText;
		}
	}
}
