// Вставьте сюда финальное содержимое файла HeatmapTask.cs

using System;

namespace Names
{
    internal static class HeatmapTask
    {
        private static string[] GetLabels(int startNumber, int elementsNumber)
        {
            var array = new string[elementsNumber];
            for (var i = 0; i < array.Length; i++)
                array[i] = (i + startNumber).ToString();
            return array;
        }

        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var mounth = GetLabels(1, 12);
            var day = GetLabels(2, 30);
            var mounthDay = new double[30, 12];
            foreach (var name in names)
                if (name.BirthDate.Day != 1)
                    mounthDay[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            return new HeatmapData("Карта интенсивности", mounthDay, day, mounth);
        }
    }
}