// Вставьте сюда финальное содержимое файла StatisticsTask.cs

using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            return visits
                .GroupBy(record => record.UserId)
                .Select(visitRecords => GetUserTimes(visitRecords, slideType))
                .SelectMany(result => result) 
                .ToList()  
                .DefaultIfEmpty(0) 
                .Median(); 
        }

        private static IEnumerable<double> GetUserTimes(IGrouping<int, VisitRecord> userRecords, SlideType slideType)
        {
            return userRecords
                .OrderBy(visitRecord => visitRecord.DateTime)
                .Bigrams()
                .Where(tuple => tuple.Item1.SlideType == slideType)
                .Select(tuple => (tuple.Item2.DateTime - tuple.Item1.DateTime).TotalMinutes)
                .Where(d => d >= 1 && d <= 120);
        }
    }
}