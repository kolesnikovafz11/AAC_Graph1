using System;
using AAC_Graph.Модели;
using AAC_Graph.Сервисы.FileIO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AAC_Graph.Benchmarks;
using AAC_Graph.Сервисы.Randomizer;
using BenchmarkDotNet.Running;

namespace AAC_Graph
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var adjacencyMatrix = FileReaderWriter.ReadAdjacencyMatrix(FileReaderWriter.InputFilePath);
            var dimension = adjacencyMatrix.GetLength(0);//размерность матрицы считанной       
            Console.WriteLine("Выберите алгоритм для раскраски графа из файла:");
            Console.WriteLine("1 Полный перебор ");
            Console.WriteLine("2 Тривиальный жадный ");
            Console.WriteLine("3 Жадный с оптимизацией ");
            int cnt = int.Parse(Console.ReadLine());
            switch (cnt)
            {
                case 1:
                        IGraphColor graphColor = new BacktrackingAlgorithm();
                        var result = graphColor.ColorGraph(dimension, ref adjacencyMatrix);
                        FileReaderWriter.WriteGraphColorResult(FileReaderWriter.OutputFilePath, result);
                        Console.WriteLine("Проверьте файл Output");
                    break;
                case 2: 
                        graphColor = new GreedyTrivialAlgorithm();
                        result = graphColor.ColorGraph(dimension, ref adjacencyMatrix);
                        FileReaderWriter.WriteGraphColorResult(FileReaderWriter.OutputFilePath, result);
                        Console.WriteLine("Проверьте файл Output");
                    break;
                case 3: 
                        graphColor = new GreedySortedByRankAlgorithm();
                        result = graphColor.ColorGraph(dimension, ref adjacencyMatrix);
                        FileReaderWriter.WriteGraphColorResult(FileReaderWriter.OutputFilePath, result);
                        Console.WriteLine("Проверьте файл Output");
                    break;

            }
            BenchmarkRunner.Run<GraphColoringAlgorithmsBenchmark>(); //автоматическое тестирование            
            Console.WriteLine("Введите любую клавишу для выхода ...");
            Console.ReadKey();
        }
    }
}