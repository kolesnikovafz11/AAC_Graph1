namespace AAC_Graph.Модели
{
    public interface IGraphColor
    { 
        (int colorsAmount, int[] coloredNodes) ColorGraph(int dim, ref byte[,] adjacencyMatrix);
    }
}