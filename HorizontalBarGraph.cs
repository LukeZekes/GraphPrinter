using System;
using System.Collections.Generic;
using System.Threading;

namespace GraphPrinter
{
    class HorizontalBarGraph
    {
        public enum SortMode
        {
            Increasing = 0,
            Decreasing = 1
        }
        readonly List<Item> items; //List of all items included in the graph
        readonly List<Marker> markers; //List of all markers in the graph (allows the graph to be colored per range of values)
        //The scale of the horizontal axis e.g. a scale of 1 means each character is equal to 1 unit, a scale of 10 means each character represents 10 units
        public double scale = 1;
        public ConsoleColor baseColor = ConsoleColor.White;
        public string label = "";
        public SortMode sortMode = SortMode.Increasing;

        #region Constructors
        public HorizontalBarGraph()
        {
            items = new List<Item>();
            markers = new List<Marker>();
        }
        public HorizontalBarGraph(params Item[] _items)
        {
            items = new List<Item>(_items);
            markers = new List<Marker>();
        }
        public HorizontalBarGraph(IEnumerable<Item> _items)
        {
            items = new List<Item>(_items);
            markers = new List<Marker>();
        }
        #endregion
        #region Item Methods
        //Removes all items with the same name as item from the graph, then adds item to the graph. Returns the added item
        public Item AddItem(Item item)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.Equals(item.Name))
                {
                    items.RemoveAt(i);
                }
            }
            items.Add(item);
            return item;
        }
        //Removes all items with the same name as _name from the graph, then adds a new item to the graph. Returns the added item
        public Item AddItem(string _name, double _value)
        {
            Item item = new Item(_name, _value);
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.Equals(item.Name))
                {
                    items.RemoveAt(i);
                }
            }
            items.Add(item);
            return item;
        }
        //Removes the specified item from the graph. Returns false if the item was not present in the graph
        public bool RemoveItem(Item _item)
        {
            return items.Remove(_item);
        }
        //Removes an item with the specified name from the graph. Returns false if no item with that name was found
        public bool RemoveItem(string _name)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].Name.Equals(_name))
                {
                    items.RemoveAt(i);
                    return true;
                }

            }
            return false;
        }
        public string OutputItems()
        {
            string str = "";
            foreach (Item item in items)
            {
                str += item + "\n";
            }
            return str;
        }

        #endregion
        #region Marker Methods
        public Marker AddMarker(Marker _marker)
        {
            //See if a marker already exists at this same value, and remove it if it does
            for(int i = 0; i < markers.Count; i++) {
                Marker m = markers[i];
                if(m.MarkerValue == _marker.MarkerValue)
                {
                    markers.RemoveAt(i);
                }
            }
            markers.Add(_marker);
            return _marker;
        }
        public Marker AddMarker(double _markerValue, ConsoleColor _markerColor, string _markerTag = "")
        {
            Marker _marker = new Marker(_markerValue, _markerColor, _markerTag);
            for (int i = 0; i < markers.Count; i++)
            {
                Marker m = markers[i];
                if (m.MarkerValue == _marker.MarkerValue)
                {
                    markers.RemoveAt(i);
                }
            }
            markers.Add(_marker);
            return _marker;
        }
        //Removes the specified marker from the graph. Returns false if the marker was not present in the graph
        public bool RemoveMarker(Marker _marker)
        {
            return markers.Remove(_marker);
        }
        //Removes an marker with the specified value from the graph. Returns false if no marker with that value was found
        public bool RemoveMarker(double _markerValue)
        {
            for (int i = 0; i < markers.Count; i++)
            {
                if (markers[i].MarkerValue == _markerValue)
                {
                    markers.RemoveAt(i);
                    return true;
                }

            }
            return false;
        }
        //Removes all markers with the tag _markerTag
        public bool RemoveMarkers(string _markerTag)
        {
            int removed = 0;
            for (int i = 0; i < markers.Count; i++)
            {
                if (markers[i].MarkerTag.Equals(_markerTag))
                {
                    markers.RemoveAt(i);
                    removed++;
                }

            }
            return removed > 0;
        }
        public string OutputMarkers()
        {
            string str = "";
            foreach (Marker marker in markers)
            {
                str += marker + "\n";
            }
            return str;
        }

        #endregion
        public double Mean()
        {
            double sum = 0;
            foreach (Item item in items)
                sum += item.Value;
            return sum / items.Count;

        }
        public double Median()
        {
            items.Sort((x, y) => x.Value.CompareTo(y.Value));
            if (items.Count % 2 == 0)
            {
                //Find middle 2 items
                //0, 1, 2, 3, 4, 5
                double item1 = items[(items.Count / 2) - 1].Value;
                double item2 = items[items.Count / 2].Value;
                //Get average of them
                return (item1 + item2) / 2;
            }
            else
            {
                //Find item in the middle of the list
                //0, 1, 2, 3, 4
                return items[items.Count / 2].Value;
            }
        }
        public Item Min()
        {
            items.Sort((x, y) => x.Value.CompareTo(y.Value));
            return items[0];
        }
        public Item Max()
        {
            items.Sort((x, y) => x.Value.CompareTo(y.Value));
            return items[^1];
        }
        //Clears graph label, markers and items
        public void ClearGraph()
        {
            items.Clear();
            markers.Clear();
            label = "";
        }
        //Clears graph and resets baseColor, sortMode and scale (needs to be updated if more properties are added)
        public void ResetGraph()
        {
            ClearGraph();
            scale = 1; ;
            baseColor = ConsoleColor.White;
            sortMode = SortMode.Increasing;
        }
        //If x is a multiple of 10, then x is returned. Otherwise, the next highest multiple of 10 is returned
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
            if (items.Count == 0)
            {
                Console.WriteLine("No items in this graph");
                return;
            }
            Console.WriteLine(); //To avoid any weird issues that arise when this is not called on a new line
            
            markers.Sort((x, y) => x.MarkerValue.CompareTo(y.MarkerValue));

            List<Item> sortedItems = new List<Item>(items); //This (hopefully) preserves the original ordering of the items List
            sortedItems.Sort((x, y) => x.Name.Length.CompareTo(y.Name.Length)); //Sorts items in order of increasing name length
            string longestName = sortedItems[^1].Name; //Gets longest name 
            int colWidth = longestName.Length + 2;

            sortedItems.Sort((x, y) => x.Value.CompareTo(y.Value)); //Sorts items in increasing order of value
            int width = (int)(GetInterval(sortedItems[^1].Value) / scale) + colWidth; //Width of the graph, including left margin
            if (sortMode == SortMode.Decreasing)
            {
                sortedItems.Reverse(); //Sorts items in decreasing order instead
                width = (int)(GetInterval(sortedItems[0].Value) / scale) + colWidth;
            }
            Console.ForegroundColor = baseColor;
            //Print out label
            
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
            for (int i = 0; i <= width - colWidth; i++)
            {
                if (i % 10 == 0)
                {
                    //Works backwards through markers to find out which marker i falls into and colors foreground appropriately
                    for(int j = markers.Count - 1; j >= 0; j--)
                    {
                        Marker m = markers[j];
                        if(i * scale >= m.MarkerValue)
                        {
                            Console.ForegroundColor = m.MarkerColor;
                            break;
                        }
                    }
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
            for (int i = 0; i <= width; i++)
            {
                if ((i - colWidth) % 10 == 0 && i >= colWidth)
                {
                    /*for (int j = markers.Count - 1; j >= 0; j--)
                    {
                        Marker m = markers[j];
                        if ((i-colWidth)* scale >= m.MarkerValue)
                        {
                            Console.ForegroundColor = m.MarkerColor;
                            break;
                        }
                    }*/
                    Console.Write("|");
                }
                else
                    Console.Write("-");
            }
            Console.ForegroundColor = baseColor;

            //Console.Write("|");
            Console.WriteLine();
            //Print out y-axis labels and bars

        foreach (Item item in sortedItems)
            {
                int markerIndex = -1; //markers[markerIndex] is the largest marker that applies to item
                int numSpaces = colWidth - item.Name.Length;
                //Coloring item names based on value
                for (int j = markers.Count - 1; j >= 0; j--)
                {
                    Marker m = markers[j];
                    if(item.Value >= m.MarkerValue)
                    {
                        Console.ForegroundColor = m.MarkerColor;
                        markerIndex = j;
                        break;
                    }
                }
                Console.Write(item.Name);

                Console.ForegroundColor = baseColor;
                for (int i = 0; i < numSpaces; i++)
                    Console.Write(" ");

                Console.Write("|");
                //Writing bars
                for (double i = scale; i <= item.Value; i+=scale)
                {
                    for (int j = markerIndex; j >= 0; j--)
                    {
                        Marker m = markers[j];
                        if (i >= m.MarkerValue)
                        {
                            Console.ForegroundColor = m.MarkerColor;
                            break;
                        }
                    }
                    Console.Write("#"); //TODO: Add ability to change character used for graphing
                    //Thread.Sleep(50);

                }
                Console.Write("  " + item.Value);
                if (markerIndex != -1 && !markers[markerIndex].MarkerTag.Equals(""))
                    Console.Write($" ({markers[markerIndex].MarkerTag})");
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
