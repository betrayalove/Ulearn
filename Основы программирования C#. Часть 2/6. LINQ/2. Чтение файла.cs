// Вставьте сюда финальное содержимое файла ParsingTask.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace linq_slideviews
{
    public class ParsingTask
    {
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return lines
                    .Skip(1)
                    .Select(line => line.Split(';'))
                    .Select(data =>
                    {
                        if (data.Length != 3 ||
                            !int.TryParse(data[0], out var id) ||
                            !Enum.TryParse(data[1], true, out SlideType type))
                            return null;
                        return new SlideRecord(id, type, data[2]);
                    })
                    .Where(slideRecord => slideRecord != null)
                    .ToDictionary(slideRecord => slideRecord.SlideId);
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1).Select(line => ParseVisitRecord(slides, line));
        }

        private static VisitRecord ParseVisitRecord(IDictionary<int, SlideRecord> slides, string line)
        {
            var data = line.Split(';');
            if (data.Length != 4 ||
                !int.TryParse(data[0], out var id) ||
                !int.TryParse(data[1], out var slideId) ||
                !DateTime.TryParse(data[2], out var date) ||
                !DateTime.TryParse(data[3], out var time) ||
                !slides.TryGetValue(slideId, out var slide))
                throw new FormatException($"Wrong line [{line}]");
            return new VisitRecord(id, slideId, date.Add(time.TimeOfDay), slide.SlideType);
        }
    }
}