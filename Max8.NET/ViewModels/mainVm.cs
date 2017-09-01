using Max8.NET.Helpers;
using Max8.NET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Max8.NET.ViewModels
{
    class MainVm : Vm
    {
        public IReadOnlyList<PlayerVm> AllPlayers1 { get; }
        public IReadOnlyList<PlayerVm> AllPlayers2 { get; }

        public MainVm()
        {
            AllPlayers1 = new List<PlayerVm>
            {
                new PlayerVm("Человек 1", false, null),
                new PlayerVm("ИИ 1 ур.", true, new Max8Ai { Depth = 1 }),
                new PlayerVm("ИИ 2 ур.", true, new Max8Ai { Depth = 2 }),
                new PlayerVm("ИИ 3 ур.", true, new Max8Ai { Depth = 3 }),
                new PlayerVm("ИИ 4 ур.", true, new Max8Ai { Depth = 4 }),
                new PlayerVm("ИИ 5 ур.", true, new Max8Ai { Depth = 5 })
            };
            AllPlayers2 = new List<PlayerVm>
            {
                new PlayerVm("Человек 2", false, null),
                new PlayerVm("ИИ 1 ур.", true, new Max8Ai { Depth = 1 }),
                new PlayerVm("ИИ 2 ур.", true, new Max8Ai { Depth = 2 }),
                new PlayerVm("ИИ 3 ур.", true, new Max8Ai { Depth = 3 }),
                new PlayerVm("ИИ 4 ур.", true, new Max8Ai { Depth = 4 }),
                new PlayerVm("ИИ 5 ур.", true, new Max8Ai { Depth = 5 })
            };
            NewGameCommand = new DelegateCommand(_ => NewGame());
            StopGameCommand = new DelegateCommand(_ => StopGame());
            PeopleMoveCommand = new DelegateCommand(o => PeopleMove((CellVm)o));
        }

        PlayerVm player1, player2, currentPlayer;
        public PlayerVm Player1
        {
            get => player1;
            set { if (Set(ref player1, value)) IsGameOver = false; }
        }
        public PlayerVm Player2
        {
            get => player2;
            set { if (Set(ref player2, value)) IsGameOver = false; }
        }
        public PlayerVm CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        bool isInGame = false, isGameOver = false;
        public bool IsInGame
        {
            get => isInGame;
            set => Set(ref isInGame, value);
        }
        public bool IsGameOver
        {
            get => isGameOver;
            set => Set(ref isGameOver, value);
        }

        Field field;
        FieldVm fieldVm;
        public FieldVm FieldVm
        {
            get => fieldVm;
            set => Set(ref fieldVm, value);
        }

        int curX, curY;
        public int CurX
        {
            get => curX;
            set => Set(ref curX, value);
        }
        public int CurY
        {
            get => curY;
            set => Set(ref curY, value);
        }

        public ICommand NewGameCommand { get; }
        public ICommand StopGameCommand { get; }
        public ICommand PeopleMoveCommand { get; }

        void StopGame()
        {
            IsInGame = false;
        }

        void NewGame()
        {
            if (Player1 == null ||
                Player2 == null) return;
            field = Field.CreateRandom();
            FieldVm = new FieldVm(field);
            Player1.Score = Player2.Score = 0;
            CurrentPlayer = null;
            CurX = CurY = 1;
            IsInGame = true;
            IsGameOver = false;
            MoveEnded();
        }

        void PeopleMove(CellVm cellVm)
        {
            if (CurrentPlayer.IsAi) return;
            int index = FieldVm.Cells.IndexOf(cellVm);
            CurX = index % FieldVm.FieldSize;
            CurY = index / FieldVm.FieldSize;
            CkeckLastCell();
        }

        async void AiMove()
        {
            var d = CurrentPlayer == Player1 ? Direction.Horizontal : Direction.Vertical;
            var delay = TplHelpers.Delay<int>(500);
            if (d == Direction.Horizontal)
            {
                CurX = (await Task.WhenAll(Task.Run(() => CurrentPlayer.Ai.FindBestMove(field, d, CurY)), delay))[0];
            }
            else
            {
                CurY = (await Task.WhenAll(Task.Run(() => CurrentPlayer.Ai.FindBestMove(field, d, CurX)), delay))[0];
            }
            if (!IsInGame) return;
            CkeckLastCell();
        }

        void CkeckLastCell()
        {
            CurrentPlayer.Score += field[CurX, CurY].Value;
            field.DeactivateCell(CurX, CurY);
            FieldVm.Cells[CurX + CurY * FieldVm.FieldSize].IsActive = false;
            MoveEnded();
        }

        void MoveEnded()
        {
            if (CurrentPlayer == Player1)
            {
                CurrentPlayer = Player2;
                IsInGame = FieldVm.ActivateVertical(CurX);
            }
            else
            {
                CurrentPlayer = Player1;
                IsInGame = FieldVm.ActivateHorizontal(CurY);
            }
            if (!IsInGame) IsGameOver = true;
            if (IsInGame && CurrentPlayer.IsAi) AiMove();
        }
    }
}