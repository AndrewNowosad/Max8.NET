using Max8.NET.Models;

namespace Max8.NET.ViewModels
{
    class PlayerVm : Vm
    {
        public string Name { get; }
        public bool IsAi { get; }
        public Max8Ai Ai { get; }

        int score = 0;
        public int Score
        {
            get => score;
            set => Set(ref score, value);
        }

        public PlayerVm(string name, bool isAi, Max8Ai ai)
        {
            Name = name;
            IsAi = isAi;
            Ai = ai;
        }
    }
}