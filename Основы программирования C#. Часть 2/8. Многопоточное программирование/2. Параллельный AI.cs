// Вставьте сюда финальное содержимое файла Bot_Parallel.cs

using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            var tasks = new Task<Tuple<Turn, double>>[threadsCount];

            for (var i = 0; i < threadsCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    var random1 = new Random();
                    return SearchBestMove(rocket, random1, iterationsCount / threadsCount);
                });
            }
            var result = Task.WhenAll(tasks);
            var max = result.Result.Max(tuple => tuple.Item2);

            foreach (var (item1, item2) in result.Result)
                if (item2 == max)
                    return rocket.Move(item1, level);
            return rocket.Move(Turn.None, level);
        }
    }
}