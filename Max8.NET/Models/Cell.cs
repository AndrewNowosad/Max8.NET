using System;

namespace Max8.NET.Models
{
    struct Cell
    {
        static Random random = new Random();
        static int GetRandomValue()
        {
            int v = random.Next(-Range, Range);
            if (v >= 0) v++;
            return v;
        }

        public static readonly int Range = 8;

        public static Cell CreateRandomCell()
            => new Cell(GetRandomValue());

        public int Value { get; }
        public bool IsActive { get; set; }

        Cell(int value)
        {
            Value = value;
            IsActive = true;
        }
    }
}