using System;


namespace GraphPrinter
{
    class Marker
    {
        double MarkerValue;
        ConsoleColor MarkerColor;

        public Marker(double _MarkerValue, ConsoleColor _MarkerColor)
        {
            MarkerValue = _MarkerValue;
            MarkerColor = _MarkerColor;
        }

        public override string ToString()
        {
            return $"MinValue: {MarkerValue}, Color: {MarkerColor}";
        }
    }
}
