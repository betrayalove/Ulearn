// Вставьте сюда финальное содержимое файла FieldsParserTask.cs

using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("", new string[0])]
        [TestCase("\"\\\"text\\\"\"", new[] { "\"text\"" })]
        [TestCase("'\\\'text\\\''", new[] { "'text'" })]
        [TestCase("'\"text\"", new[] { "\"text\"" })]
        [TestCase("hello  world ", new[] { "hello", "world" })]
        [TestCase("\"'hello' world\"", new[] { "'hello' world" })]
        [TestCase("\"hello\"world", new[] { "hello", "world" })]
        [TestCase(@"""\\""", new[] { "\\" })]
        [TestCase("hello\"world\"", new[] { "hello", "world" })]
        [TestCase("' ", new[] { " " })]
        [TestCase("\'\'", new[] { "" })]

        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var list = new List<Token>();
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                    continue;
                var token = TakeToken(line, i);
                list.Add(token);
                i = token.GetIndexNextToToken() - 1;
            }
            return list;
        }

        public static Token TakeToken(string line, int i)
        {
            if (line[i] == '\'' || line[i] == '\"')
                return ReadQuotedField(line, i);
            return ReadField(line, i);
        }

        private static Token ReadField(string line, int startIndex)
        {
            var stringBuilder = new StringBuilder();
            for (var i = startIndex; i < line.Length; i++)
            {
                if (line[i] == '\'' || line[i] == '\"' || line[i] == ' ')
                    break;
                stringBuilder.Append(line[i]);
            }
            return new Token(stringBuilder.ToString(), startIndex, stringBuilder.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}