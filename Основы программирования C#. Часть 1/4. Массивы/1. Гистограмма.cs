// Вставьте сюда финальное содержимое файла HistogramTask.cs

using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var labels = new string[31];
            for (var i = 0; i < labels.Length; i++)
            {
                labels[i] = (i + 1).ToString();
            }
            var birthCount = new double[31];
            foreach (var day in names)
            {
                if (day.Name == name && day.BirthDate.Day != 1)
                    birthCount[day.BirthDate.Day - 1]++;
            }
            return new HistogramData($"Рождаемость с именем {name}", labels, birthCount);
        }
    }
}