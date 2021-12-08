namespace AAC_Graph.Модели
{
    public struct NodeNumberRank
    {
        public int nodeNumber; //номер вершины
        public int nodeRank; //номер цвета 

        public NodeNumberRank(int nodeNumber, int nodeRank)
        {
            this.nodeNumber = nodeNumber;
            this.nodeRank = nodeRank;
        }
    }
}