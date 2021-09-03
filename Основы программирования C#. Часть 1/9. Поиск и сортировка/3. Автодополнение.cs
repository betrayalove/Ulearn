// Вставьте сюда финальное содержимое файла AutocompleteTask.cs

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var countByPrefix = Math.Min(count, GetCountByPrefix(phrases, prefix));
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var topByPrefix = new string[countByPrefix];
            var i = 0;
            while (i < countByPrefix)
            {
                topByPrefix[i] = phrases[left + i + 1];
                i++;
            }
            return topByPrefix;
        }

        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var countOfPhrases = phrases.Count;
            var left = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, countOfPhrases);
            var right = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, countOfPhrases);
            return right - left - 1;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyResult()
        {
            var phrases = new string[] { "aa", "ab", "bc", "bd", "be", "ca", "cb" };
            var prefix = "aaa";
            var expectedCount = 0;
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenPrefixEmpty()
        {
            var phrases = new string[] { "aa", "ab", "bc", "bd", "be", "ca", "cb" };
            var prefix = "";
            var expectedCount = phrases.Length;
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenSingleResult()
        {
            var phrases = new string[] { "aa", "ab", "bc", "bd", "be", "ca", "cb" };
            var prefix = "aa";
            var expectedCount = 1;
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenMultipleResult()
        {
            var phrases = new string[] { "aa", "ab", "bc", "bd", "be", "ca", "cb" };
            var prefix = "b";
            var expectedCount = 3;
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}