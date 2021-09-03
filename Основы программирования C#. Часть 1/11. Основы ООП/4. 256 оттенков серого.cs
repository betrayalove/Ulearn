// Вставьте сюда финальное содержимое файла SegmentExtensions.cs

using System.Collections.Generic;
using System.Drawing;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> Dictionary = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color)
        {
            if (Dictionary.ContainsKey(segment))
                Dictionary[segment] = color;
            else 
                Dictionary.Add(segment, color);
        }

        public static Color GetColor(this Segment segment)
        {
            if (Dictionary.ContainsKey(segment))
                return Dictionary[segment];
            return Color.Black;
        }
    }
}