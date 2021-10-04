using System;

namespace GraphPrinter
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demo program
            HorizontalBarGraph barGraph = new HorizontalBarGraph();
            /*barGraph.AddMarker(0, ConsoleColor.Green);
            barGraph.AddMarker(10, ConsoleColor.Yellow);
            barGraph.AddMarker(20, ConsoleColor.Red);
            
            barGraph.AddItem("Item 1", 20);
            barGraph.AddItem("Item 2", 2);
            barGraph.AddItem("Item 3", 15);
            barGraph.AddItem("Item 4", 41);
            barGraph.scale = 1;
            barGraph.baseColor = ConsoleColor.White;
            barGraph.label = "Test Graph";
            */
            barGraph.PrintGraph();
            
        }
    }
}
