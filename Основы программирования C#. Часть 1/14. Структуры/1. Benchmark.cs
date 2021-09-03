// Вставьте сюда финальное содержимое файла BenchmarkTask.cs

//Не моя

using System;
using System.Diagnostics;
using System.Text;
using NUnit.Framework;
 
namespace StructBenchmarking
{
    public class Benchmark : IBenchmark
    {
        public double MeasureDurationInMs(ITask task, int repetitionCount)
        {
            task.Run();
            GC.Collect();                   
            GC.WaitForPendingFinalizers(); 
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (var i = 0; i < repetitionCount; i++)
                task.Run();
            stopwatch.Stop();
            return (double)stopwatch.ElapsedMilliseconds / repetitionCount;
        }
    }
 
    public class CreateStringBuilderString : ITask
    {
        public void Run()
        {
            var stringBuilderString = new StringBuilder();
            for (var i = 0; i < 10000; i++)
                stringBuilderString.Append('a');
            stringBuilderString.ToString();
        }
    }
 
    public class CreateStringConstructorString : ITask
    {
        public void Run()
        {
            new string('a', 10000);
        }
    }
 
    [TestFixture]
    public class RealBenchmarkUsageSample
    {
        [Test]
        public void StringConstructorFasterThanStringBuilder()
        {
            var repetitionCount = 10000;
            var stringBuilderString = new CreateStringBuilderString();
            var stringConstructorString = new CreateStringConstructorString();
            var benchmark = new Benchmark();
            var stringBuilderTime = benchmark.MeasureDurationInMs(stringBuilderString, repetitionCount);
            var stringConstructorTime = benchmark.MeasureDurationInMs(stringConstructorString, repetitionCount);
            Assert.Less(stringConstructorTime, stringBuilderTime);
        }
    }
}