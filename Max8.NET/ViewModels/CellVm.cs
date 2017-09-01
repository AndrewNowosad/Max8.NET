namespace Max8.NET.ViewModels
{
    class CellVm : Vm
    {
        public int Value { get; }

        bool isActive = true;
        public bool IsActive
        {
            get => isActive;
            set => Set(ref isActive, value);
        }

        bool isAvailable = false;
        public bool IsAvailable
        {
            get => isAvailable;
            set => Set(ref isAvailable, value);
        }

        public CellVm(int value)
        {
            Value = value;
        }
    }
}