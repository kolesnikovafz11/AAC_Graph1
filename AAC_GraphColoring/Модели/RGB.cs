namespace AAC_Graph.Модели
{
    public struct RGB
    {
        private readonly byte _r;
        private readonly byte _g;
        private readonly byte _b;

        public RGB(byte r, byte g, byte b)
        {
            _r = r;
            _g = g;
            _b = b;
        }
        public override string ToString()
        {
            var rgbFormatString = $"(r={_r},g={_g},b={_b})";
            var colorString = "\x1b[48;2;" + _r + ";" + _g + ";" + _b + $"m\"COLOR\"";
            return rgbFormatString + colorString;
        }
    }
}