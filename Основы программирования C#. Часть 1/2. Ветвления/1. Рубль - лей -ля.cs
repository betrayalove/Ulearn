// Вставьте сюда финальное содержимое файла PluralizeTask.cs

namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            if ((count % 100 != 11) && (count % 10 == 1))
                return "рубль";
            else if ((count % 100 < 12 || count % 100 > 14) && (count % 10 > 1) && (count % 10 < 5))
                return "рубля";
            return "рублей";
        }
    }
}