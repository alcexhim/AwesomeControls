using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AwesomeControls.ListView
{
	public class ListViewItemBoundsCollection
	{
		private List<ListViewItem> itemsByIndex = new List<ListViewItem>();
		private List<Rectangle> boundsByIndex = new List<Rectangle>();

		private Dictionary<ListViewItem, Rectangle> boundsByItem = new Dictionary<ListViewItem, Rectangle>();

		public void Add(ListViewItem item, Rectangle bounds)
		{
			boundsByItem.Add(item, bounds);

			itemsByIndex.Add(item);
			boundsByIndex.Add(bounds);
		}

		public Rectangle GetBounds(ListViewItem item)
		{
			if (!boundsByItem.ContainsKey(item))
			{
				return Rectangle.Empty;
			}
			return boundsByItem[item];
		}

		public ListViewItem[] GetIntersectedItems(Rectangle rect, bool wasControlKeyPressed, int start = 0)
		{
			if (boundsByIndex.Count == 0) return new ListViewItem[0];

			int counterF = start;
			int counterB = Count - 1;

			if (counterF < 0) counterF = 0;
			if (counterF > Count - 1) counterF = Count - 1;

			List<ListViewItem> lst = new List<ListViewItem>();

			Rectangle rectF, rectB;
			while (counterF != counterB)
			{
				rectF = boundsByIndex[counterF];
				rectB = boundsByIndex[counterB];

				if (rectF.IntersectsWith(rect)) lst.Add(itemsByIndex[counterF]);
				if (rectB.IntersectsWith(rect)) lst.Add(itemsByIndex[counterB]);

				// if (!wasControlKeyPressed) continue;

				counterB--;
				counterF++;

				if (counterF >= Count) return lst.ToArray();
				if (counterB < 0) return lst.ToArray();
			}

			rectF = boundsByIndex[counterF];
			rectB = boundsByIndex[counterB];

			if (rectF.IntersectsWith(rect)) lst.Add(itemsByIndex[counterF]);
			if (rectB.IntersectsWith(rect)) lst.Add(itemsByIndex[counterB]);

			// if (!wasControlKeyPressed) continue;

			return lst.ToArray();
		}

		public bool Contains(ListViewItem lvi)
		{
			return boundsByItem.ContainsKey(lvi);
		}

		public ListViewItem GetItemAtPoint(Point point, int start = 0)
		{
			int counterF = start;
			int counterB = Count - 1;

			if (counterF >= Count) return null;
			if (counterB < 0) return null;

			if (counterF < 0) counterF = 0;

			while (counterF != counterB)
			{
				Rectangle rectF = boundsByIndex[counterF];
				if (rectF.Contains(point)) return itemsByIndex[counterF];

				Rectangle rectB = boundsByIndex[counterB];
				if (rectB.Contains(point)) return itemsByIndex[counterB];

				counterF++;
				counterB--;

				if (counterF >= Count) break;
				if (counterB < 0) break;
			}

			if (counterF >= this.Count) return null;
			if (counterB < 0) return null;

			Rectangle rectC = boundsByIndex[counterB];
			if (rectC.Contains(point)) return itemsByIndex[counterB];

			return null;
		}

		public int Count { get { return itemsByIndex.Count; } }

		public void Clear()
		{
			itemsByIndex.Clear();
			boundsByIndex.Clear();
			boundsByItem.Clear();
		}
	}
}
