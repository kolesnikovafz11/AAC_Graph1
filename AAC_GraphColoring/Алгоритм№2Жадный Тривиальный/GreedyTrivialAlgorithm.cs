using System.Linq;
using AAC_Graph.Модели;

namespace AAC_Graph
{
    public sealed class GreedyTrivialAlgorithm : IGraphColor
    {
        public (int colorsAmount, int[] coloredNodes) ColorGraph(int dim, ref byte[,] adjacencyMatrix)
        {
            var coloredNodes = new int[dim];
            InitAllNodesAsUncolored(ref coloredNodes);

            //цикл по всем вершинам
            for (int i = 0; i < dim; i++)
            {
                //цикл по всем цветам
                for (int j = 0; j < 16777216; j++) //256*256*256 = rgb
                {
                    if (!CanNodeBeColored(i, j, dim, ref adjacencyMatrix, ref coloredNodes)) continue;

                    coloredNodes[i] = j;
                    break;
                }
            }
            
            return (coloredNodes.Max() + 1, coloredNodes);
        }
        
        private  void InitAllNodesAsUncolored(ref int[] coloredNodes) //Инициализация вершины как непокрашенной
        {
            // -1 - не раскрашеннная вершина
            for (int i = 0; i < coloredNodes.Length; i++)
            {
                coloredNodes[i] = -1;
            }
        }

        private bool CanNodeBeColored(int node, int color, int dim, ref byte[,] adjacencyMatrix, ref int[] coloredList) //Проверка, можно ли раскрасить вершину в текущий цвет
        {
            //цикл по всем вершинам
            for (int i = 0; i < dim; i++)
            {
                //если вершина смежна с нашей
                if (adjacencyMatrix[node, i] == 1)
                {
                    //если вершина раскрашена
                    if (coloredList[i] != -1)
                    {
                        //если она уже использует наш цвет, то его нельзя использовать
                        if (coloredList[i] == color)
                        {
                            return false;
                        }
                    }
                }
            }

            //ни одна вершина смежная с нами не использует наш цвет, значит его можно взять
            return true;
        }
    }
}