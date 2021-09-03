[TestCase("text", new[] {"text"})]
[TestCase("hello world", new[] {"hello", "world"})]
// Вставляйте сюда свои тесты
public static void RunTests(string input, string[] expectedOutput)
{
    // Тело метода изменять не нужно
    Test(input, expectedOutput);
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