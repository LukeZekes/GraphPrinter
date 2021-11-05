using System;

namespace GraphPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo program
            HorizontalBarGraph barGraph = new HorizontalBarGraph();
            barGraph.AddMarker(0, ConsoleColor.Green);
            barGraph.AddMarker(0, ConsoleColor.Green, "Green");
            barGraph.AddMarker(10, ConsoleColor.Yellow, "Yellow");
            barGraph.AddMarker(20, ConsoleColor.Red, "Red");

            barGraph.AddItem("Item 1", 6);
            barGraph.AddItem("Item 2", 6);
            barGraph.AddItem("Item 3", 3);
            barGraph.AddItem("Item 4", 4);
            barGraph.AddItem("Item 5", 5);
            
            barGraph.scale = 2;
            barGraph.baseColor = ConsoleColor.Magenta;
            barGraph.label = "Test Graph";

            barGraph.PrintGraph();
            barGraph.ResetGraph();
            barGraph.AddItem("Item 1", 6);

            barGraph.PrintGraph();

        }
    }
}
