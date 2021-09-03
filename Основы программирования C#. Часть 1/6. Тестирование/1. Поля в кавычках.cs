// Вставьте сюда финальное содержимое файла QuotedFieldTask.cs

using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var stringBuilder = new StringBuilder();
            var length = 1;
            for (var i = startIndex + 1; i < line.Length; i++)
            {
                length++;
                if (line[startIndex] == line[i] && line[i - 1] != '\\')
                    break;
                if (line[i] != '\\')
                    stringBuilder.Append(line[i]);
            }
            return new Token(stringBuilder.ToString(), startIndex, length);
        }
    }
}