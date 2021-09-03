// Вставьте сюда финальное содержимое файла TextGeneratorTask.cs

// Решение не прошло проверку антиплагиата((((

using System.Collections.Generic;

namespace TextAnalysis
{
    internal static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            for (var i = 0; i < wordsCount; i++)
            {
                string addPhrase;
                if (nextWords.ContainsKey(phraseBeginning))
                    addPhrase = nextWords[phraseBeginning];
                else if (phraseBeginning.Split().Length > 1)
                {
                    if (nextWords.ContainsKey(Split(phraseBeginning, 2) + " " + Split(phraseBeginning, 1)))
                        addPhrase = nextWords[Split(phraseBeginning, 2) + " " + Split(phraseBeginning, 1)];
                    else if (nextWords.ContainsKey(Split(phraseBeginning, 1)))
                        addPhrase = nextWords[Split(phraseBeginning, 1)];
                    else break;
                }
                else break;
                phraseBeginning += (" " + addPhrase);
            }
            return phraseBeginning;
        }

        private static string Split(string text, int i)
        {
            return text.Split()[text.Split().Length - i];
        }
    }
}