using System;


namespace GraphPrinter
{
    class Marker
    {
        public double MarkerValue;
        public ConsoleColor MarkerColor;
        public string MarkerTag;

        public Marker(double _MarkerValue, ConsoleColor _MarkerColor)
        {
            MarkerValue = _MarkerValue;
            MarkerColor = _MarkerColor;
            MarkerTag = "";
        }
        public Marker(double _MarkerValue, ConsoleColor _MarkerColor, string _MarkerTag)
        {
            MarkerValue = _MarkerValue;
            MarkerColor = _MarkerColor;
            MarkerTag = _MarkerTag;
        }

        public override string ToString()
        {
            return $"MinValue: {MarkerValue}, Color: {MarkerColor}";
        }
    }
}
