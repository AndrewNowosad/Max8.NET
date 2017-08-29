namespace Max8.NET.ViewModels
{
    class CellVm : Vm
    {
        public int Value { get; }

        bool isActive;
        public bool IsActive
        {
            get => isActive;
            set => Set(ref isActive, value);
        }

        public CellVm(int value)
        {
            Value = value;
            IsActive = true;
        }
    }
}