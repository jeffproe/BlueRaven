using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BlueRaven.Data.Extensions
{
	public static class StringUrlExtensions
	{
		public static string RemoveDiacritics(this string text)
		{
			var normalizedString = text.Normalize(NormalizationForm.FormD);
			var stringBuilder = new StringBuilder();

			foreach (var c in normalizedString)
			{
				var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
				if (unicodeCategory != UnicodeCategory.NonSpacingMark)
				{
					stringBuilder.Append(c);
				}
			}

			return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
		}

		public static string RemoveReservedUrlCharacters(this string text)
		{
			var reservedCharacters = new List<string>() { "!", "#", "$", "&", "'", "(", ")", "*", ",", "/", ":", ";", "=", "?", "@", "[", "]", "\"", "%", ".", "<", ">", "\\", "^", "_", "'", "{", "}", "|", "~", "`", "+" };

			foreach (var chr in reservedCharacters)
			{
				text = text.Replace(chr, "");
			}

			return text;
		}
	}
}