// Вставьте сюда финальное содержимое файла ExperimentsTask.cs

//Не моя

using System.Collections.Generic;

namespace StructBenchmarking
{
    public static class Experiments
    {
        public static ChartData BuildChartDataForArrayCreation(IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var i in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(i,
                    benchmark.MeasureDurationInMs(new ClassArrayCreationTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i,
                    benchmark.MeasureDurationInMs(new StructArrayCreationTask(i), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Create array",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }

        public static ChartData BuildChartDataForMethodCall(IBenchmark benchmark, int repetitionsCount)
        {
            var classesTimes = new List<ExperimentResult>();
            var structuresTimes = new List<ExperimentResult>();

            foreach (var i in Constants.FieldCounts)
            {
                classesTimes.Add(new ExperimentResult(i,
                    benchmark.MeasureDurationInMs(new MethodCallWithClassArgumentTask(i), repetitionsCount)));
                structuresTimes.Add(new ExperimentResult(i,
                    benchmark.MeasureDurationInMs(new MethodCallWithStructArgumentTask(i), repetitionsCount)));
            }

            return new ChartData
            {
                Title = "Call method with argument",
                ClassPoints = classesTimes,
                StructPoints = structuresTimes,
            };
        }
    }
}