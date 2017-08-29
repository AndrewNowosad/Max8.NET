using Max8.NET.Models;
using System.Collections.Generic;

namespace Max8.NET.ViewModels
{
    class FieldVm : Vm
    {
        public int FieldSize => Field.Width;

        public IReadOnlyList<CellVm> Cells { get; }

        public FieldVm(Field field)
        {
            var cells = new List<CellVm>(FieldSize * FieldSize);
            for (int y = 0; y < FieldSize; ++y)
                for (int x = 0; x < FieldSize; ++x)
                    cells.Add(new CellVm(field[x, y].Value));
            Cells = cells;
        }

        public bool ActivateHorizontal(int h)
        {
            int k = 0;
            for (int y = 0; y < FieldSize; ++y)
                for (int x = 0; x < FieldSize; ++x)
                {
                    Cells[x + y * FieldSize].IsAvailable = y == h;
                    if (y == h && Cells[x + y * FieldSize].IsActive) ++k;
                }
            return k > 0;
        }

        public bool ActivateVertical(int v)
        {
            int k = 0;
            for (int y = 0; y < FieldSize; ++y)
                for (int x = 0; x < FieldSize; ++x)
                {
                    Cells[x + y * FieldSize].IsAvailable = x == v;
                    if (x == v && Cells[x + y * FieldSize].IsActive) ++k;
                }
            return k > 0;
        }
    }
}