// Вставьте сюда финальное содержимое файла TicketsTask.cs

using System.Numerics;

namespace Tickets
{
    public static class TicketsTask
    {
        public static BigInteger Solve(int totalLen, int totalSum)
        {
            var halfSum = totalSum / 2;
            var bigIntegers = new BigInteger[totalLen + 1, halfSum + 1];
            if (totalSum % 2 != 0) return 0;
            for (var i = 1; i <= totalLen; i++)
                bigIntegers[i, 0] = 1;
            for (var i = 0; i <= halfSum; i++)
                bigIntegers[0, i] = 0;
            for (var i = 1; i <= totalLen; i++)
                for (var j = 1; j <= halfSum; j++)
                    if (j > i * 9) bigIntegers[i, j] = 0;
                    else
                    {
                        bigIntegers[i, j] = bigIntegers[i - 1, j] + bigIntegers[i, j - 1];
                        if (j > 9) bigIntegers[i, j] -= bigIntegers[i - 1, j - 10];
                    }
            return bigIntegers[totalLen, halfSum] * bigIntegers[totalLen, halfSum];
        }
    }
}