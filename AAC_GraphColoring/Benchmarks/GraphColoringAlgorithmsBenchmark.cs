using System.Collections.Generic;
using AAC_Graph.Модели;
using AAC_Graph.Сервисы.Randomizer;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AAC_Graph.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class GraphColoringAlgorithmsBenchmark
    {
        private static readonly IGraphColor BacktrackingAlgorithm = new BacktrackingAlgorithm();
        private static readonly IGraphColor GreedyTrivialAlgorithm = new GreedyTrivialAlgorithm();
        private static readonly IGraphColor GreedySortedByRankAlgorithm = new GreedySortedByRankAlgorithm();

        #region Benchmarks

        /*[Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(BacktrackingAlgoArgumentsProvider))]
        public void BenchmarkBacktrackingAlgorithm(int dim, ref byte[,] matrix)
        {
            BacktrackingAlgorithm.ColorGraph(dim, ref matrix);
        }*/

        [Benchmark]
        [ArgumentsSource(nameof(GreedyTrivialAlgoArgumentsProvider))]
        public void BenchmarkGreedyTrivialAlgorithm(int dim, ref byte[,] matrix)
        {
            GreedyTrivialAlgorithm.ColorGraph(dim, ref matrix);
        }

        [Benchmark]
        [ArgumentsSource(nameof(GreedySortedByRankAlgoArgumentsProvider))]
        public void BenchmarkGreedySortedByRankAlgorithm(int dim, ref byte[,] matrix)
        {
            GreedySortedByRankAlgorithm.ColorGraph(dim, ref matrix);
        }

        #endregion

        #region Data Providers

        public IEnumerable<object[]> BacktrackingAlgoArgumentsProvider()
        {
            const int dim = 5;
            var testData = Randomizer.GetRandomAdjacencyMatrix(dim);
            yield return new object[] {dim, testData};
        }


        public IEnumerable<object[]> GreedyTrivialAlgoArgumentsProvider()
        {
            const int dim = 100;
            var testData = Randomizer.GetRandomAdjacencyMatrix(dim);
            yield return new object[] {dim, testData};
        }

        public IEnumerable<object[]> GreedySortedByRankAlgoArgumentsProvider()
        {
            const int dim = 100;
            var testData = Randomizer.GetRandomAdjacencyMatrix(dim);
            yield return new object[] {dim, testData};
        }

        #endregion
    }
}