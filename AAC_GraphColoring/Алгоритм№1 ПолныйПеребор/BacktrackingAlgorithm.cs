using System.Collections.Generic;
using System.Linq;
using AAC_Graph.Модели;

namespace AAC_Graph
{
    public class BacktrackingAlgorithm : IGraphColor //так как обработка вершин происходит с помощью рекурсии (каждый раз рекурсия вызывается для еще неокрашенной вершины), то сложность O(n!)
    {
        private int _dim; //Размерность матрицы
        private byte[,] _adjacencyMatrix; //Матрица смежности
        public (int colorsAmount, int[] coloredNodes) ColorGraph(int dim, ref byte[,] adjacencyMatrix)
        {
            _dim = dim;
            _adjacencyMatrix = adjacencyMatrix;
            var localMax = int.MaxValue;
            var coloredNodesResult = new Dictionary<int, int>(0); //Набор цветов
            for (int i = 0; i < _dim; i++) 
            {
                var result = RecursiveColorGraph(new Dictionary<int, int>(),i);
                var max = result.Values.Max();
                if (localMax > max)
                {
                    localMax = max;
                    coloredNodesResult = result;
                }
            }

            var commonFormat = GetResult(_dim, ref coloredNodesResult);
            var colorsAmount = commonFormat.Max();

            return (colorsAmount + 1, commonFormat);
        }
     
        private Dictionary<int,int> RecursiveColorGraph(Dictionary<int,int> currentColoredNodes,int currentNode) //Рекурсивный обход всех вершин //O(n!)
        {
            PaintCurrentNode(currentColoredNodes, currentNode);

            var minNodeColorsList = new Dictionary<int, int>(0);
            for (int i = 0; i < _dim; i++)
            {
                //если вершина уже раскрашена, то берем следующую
                if (currentColoredNodes.ContainsKey(i)) continue;
                
                var tempNewDict = new Dictionary<int, int>(currentColoredNodes);
                var nodeColorsList = RecursiveColorGraph(tempNewDict,i);
               //если хроматическое число текущего минимума больше , чем у списка из рекурсивного возврата,то обновим набор вершин с минимальным хромо числом
                var isNodeColorsListHasLessChromaticNumber =
                    CheckIsChromaticNumberIsLessOnComparingList(ref minNodeColorsList, ref nodeColorsList);
                if (isNodeColorsListHasLessChromaticNumber)
                {
                    minNodeColorsList = nodeColorsList;
                }
            }

            if (currentColoredNodes.Count == _dim)
            {
                return currentColoredNodes;
            }
            return minNodeColorsList;
        }

       
        private void PaintCurrentNode(Dictionary<int, int> currentColoredNodes, int currentNode) //Раскраска вершины в свободный цвет
        {
            //цикл по всем цветам
            for (int color = 0; color < 16777216; color++) 
            {
                var isColorFound = true;
                //цикл по всем раскрашенным вершинам
                foreach (var nodeNumber in currentColoredNodes.Keys)
                    //если вершина смежна с нашей
                    if (_adjacencyMatrix[currentNode, nodeNumber] == 1)
                        //если она уже использует наш цвет, то его нельзя использовать
                        if (currentColoredNodes[nodeNumber] == color)
                        {
                            isColorFound = false;
                            break;
                        }

                if (!isColorFound) continue;

                currentColoredNodes.Add(currentNode, color);
                break;
            }
        }
    
        private bool CheckIsChromaticNumberIsLessOnComparingList(ref Dictionary<int,int> minNodeColors,ref Dictionary<int,int> comparingNodeColors) //Проверяет, меньше ли хроматическое число у сравниваемого элемента относительно минимального
        {
            var minNodeColorsAmount = GetChromaticNumber(minNodeColors);
            var comparingNodeColorsAmount = GetChromaticNumber(comparingNodeColors);

            return comparingNodeColorsAmount < minNodeColorsAmount ;
        }

        
        public int GetChromaticNumber(Dictionary<int,int> numberColors) // Получает хроматическое число из списка
        {
            if (numberColors.Count == 0)  return int.MaxValue;

            return numberColors.Values.Max();
        }
     
        private int[] GetResult(int dim,ref Dictionary<int,int> sortedNodesByRank) //Конвертация структуры в массив, где индекс - номер узла, а значение - цвет 
        {
            var nodeIndexColor = new int[dim];
            for (int i = 0; i < dim; i++)
            {
                nodeIndexColor[i] = sortedNodesByRank[i] ;
            }

            return nodeIndexColor;
        }
    }
}