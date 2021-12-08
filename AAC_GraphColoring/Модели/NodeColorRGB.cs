namespace AAC_Graph.Модели
{
    public struct NodeColorRGB
    {
        private int _nodeNumber;
        private RGB _color;

        public NodeColorRGB(int nodeNumber, RGB color)
        {
            _nodeNumber = nodeNumber;
            _color = color;
        }

        public override string ToString()
        {
            var nodeString = $"node number:{_nodeNumber}";
            var commonString = $"{nodeString} - {_color}";
            return commonString;
        }
    }
}