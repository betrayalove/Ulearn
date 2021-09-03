// Вставьте сюда финальное содержимое файла LeftBorderTask.cs

using System;
using System.Collections.Generic;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right - left == 1)
                return left;
			var middle = (left + (right - left ) / 2);
            if (string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) <= 0)
                return GetLeftBorderIndex(phrases, prefix, left, middle);
            return GetLeftBorderIndex(phrases, prefix, middle, right);
        }
    }
}