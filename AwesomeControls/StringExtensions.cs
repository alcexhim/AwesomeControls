using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeControls
{
	internal static class StringExtensions
	{
		public static string Capitalize(this string value)
		{
			if (String.IsNullOrEmpty(value)) return value;
			if (value.Length == 1) return value.ToUpper();
			return value[0].ToString().ToUpper() + value.Substring(1);
		}
	}
}
