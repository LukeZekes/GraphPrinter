using System;

namespace GraphPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            
            HorizontalBarGraph barGraph = new HorizontalBarGraph();
            barGraph.AddMarker(0, ConsoleColor.Red);
            barGraph.AddMarker(1, ConsoleColor.White);
            barGraph.AddMarker(2, ConsoleColor.Blue);
            Console.WriteLine(barGraph.OutputMarkers());
            /*
            barGraph.AddItem("Item 1", 20);
            barGraph.AddItem("Item 2", 2);
            barGraph.AddItem("Item 3", 15);
            barGraph.AddItem("Item 4", 41);
            barGraph.scale = 1;
            barGraph.baseColor = ConsoleColor.White;
            barGraph.label = "Test Graph";
            Console.WriteLine(barGraph.OutputItems());
            barGraph.PrintGraph();
            */
        }
    }
}
