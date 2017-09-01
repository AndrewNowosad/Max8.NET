using System;

namespace Max8.NET.Models
{
    struct Cell
    {
        public static readonly int Range = 8;

        static Random random = new Random();

        public static Cell CreateRandomCell()
        {
            int v = random.Next(-Range, Range);
            if (v >= 0) v++;
            return new Cell(v);
        }

        public int Value { get; }
        public bool IsActive { get; set; }

        Cell(int value)
        {
            Value = value;
            IsActive = true;
        }
    }
}