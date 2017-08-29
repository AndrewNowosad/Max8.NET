namespace Max8.NET.Models
{
    class Field
    {
        public static readonly int Width = 8;

        public static Field CreateRandom()
        {
            var f = new Field();
            f.cells = new Cell[Width, Width];
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Width; ++y)
                    f.cells[x, y] = Cell.CreateRandomCell();
            return f;
        }

        Cell[,] cells;
        bool isTransp = false;

        Field() { }

        public Cell this[int x, int y]
            => isTransp ? cells[y, x] : cells[x, y];

        public void Transpose()
            => isTransp = !isTransp;

        public Field CreateCopy()
        {
            return new Field
            {
                cells = (Cell[,])cells.Clone(),
                isTransp = isTransp
            };
        }
    }
}