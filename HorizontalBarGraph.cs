using System;
using System.Collections.Generic;

namespace GraphPrinter
{
    class HorizontalBarGraph
    {
        readonly List<Item> items;
        //The scale of the horizontal axis e.g. a scale of 1 means each character is equal to 1 unit, a scale of 10 means each character represents 10 units
        public double scale = 1;
        public ConsoleColor baseColor = ConsoleColor.White;
        public string label = "";
        public HorizontalBarGraph()
        {
            items = new List<Item>();
        }
        public HorizontalBarGraph(params Item[] _items)
        {
            items = new List<Item>(_items);
        }
        public HorizontalBarGraph(IEnumerable<Item> _items)
        {
            items = new List<Item>(_items);
        }

        public Item AddItem(Item item)
        {
            items.Add(item);
            return item;
        }
        public Item AddItem(string _name, double _value)
        {
            Item item = new Item(_name, _value);
            items.Add(item);
            return item;
        }
        public string OutputItems()
        {
            string str = "";
            foreach(Item item in items)
            {
                str += item + "\n";
            }
            return str;
        }
        //Postcondition: If x is a multiple of 10, then x is returned. Otherwise, the next highest multiple of 10 is returned
        double GetInterval(double x)
        {
            if (x % 10 == 0)
                return x;
            else
            {
                while (x % (10 * scale) != 0)
                    x++;

                return x;
            }
        }
        public void PrintGraph()
        {
            Console.WriteLine(); //To avoid any weird issues that arise when this is not called on a new line

            List<Item> sortedItems = items; //This (hopefully) preserves the original ordering of the items List
            sortedItems.Sort((x, y) => x.Name.Length.CompareTo(y.Name.Length)); //Sorts items in order of increasing name length
            string longestName = items[^1].Name; //Gets longest name 
            int colWidth = longestName.Length + 2;

            sortedItems.Sort((x, y) => x.Value.CompareTo(y.Value)); //Sorts items in increasing order of risk score
        //  if (sortMode == SortMode.Decreasing)
            //  items.Reverse(); //Sorts items in decreasing order instead

            Console.ForegroundColor = baseColor;
            //Print out label
            int width = (int)(GetInterval(sortedItems[^1].Value) / scale) + colWidth;
            int margin = (width - label.Length) / 2;
            for (int i = 0; i < margin; i++)
                Console.Write(" ");
            
            Console.Write(label);

            for (int i = 0; i < margin; i++)
                Console.Write(" ");
            Console.WriteLine();
            
            //Print out x-axis
            for (int i = 0; i < colWidth; i++)
            {
                Console.Write("-");
            }
            for (int i = 0; i < (GetInterval(sortedItems[^1].Value) / scale) + 1; i++)
            {
                if (i % 10 == 0)
                {/*
                    switch (i)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 10:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case 20:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 30:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }
                    */
                    Console.Write(i * scale);
                    Console.ForegroundColor = baseColor;
                }
                else if ((i - 1) % 10 == 0 && i - 1 > 0)
                    Console.Write("");
                else
                {
                    Console.Write("-");
                }
            }

            Console.WriteLine();
            //Print out divider
            for (int i = 0; i < colWidth + (GetInterval(sortedItems[^1].Value) / scale) + 1; i++)
            {
                if ((i - colWidth) % 10 == 0 && i >= colWidth)
                {
                    /* Coloring needs to be redone for this project to allow for greater control
                    switch (i - colWidth)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 10:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case 20:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        case 30:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                    }*/
                    Console.Write("|");
                    Console.ForegroundColor = baseColor;

                }
                else
                    Console.Write("-");
            }

            //Console.Write("|");
            Console.WriteLine();
            //Print out y-axis labels and bars

        foreach (Item item in items)
            {
                int numSpaces = colWidth - item.Name.Length;
                /*  Coloring item names based on value - values for color thresholds not implemented
                if (item.value < 10)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (item.value < 20)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (item.value < 30)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (item.value < 40)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                */
                Console.Write(item.Name);

               // Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < numSpaces; i++)
                    Console.Write(" ");

                Console.Write("|");
                //Writing bars
                for (double i = scale; i <= item.Value; i+=scale)
                {
                    /* More coloring, needs to be redone for this project
                    if (i < 10)
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (i < 20)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (i < 30)
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (i < 40)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.DarkRed;

                    */
                    
                    Console.Write("#"); //TODO: Add ability to change character used for graphing
                }


                Console.Write("  " + item.Value);
                Console.WriteLine();
            }

            Console.ResetColor();

        }

    }
}
