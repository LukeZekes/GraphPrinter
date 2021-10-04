using System;

namespace GraphPrinter
{
    class Item
    {
        public string Name {get; set;}
        public double Value { get; set; }

        public Item(string _name, double _value)
        {
            Name = _name;
            Value = _value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}
