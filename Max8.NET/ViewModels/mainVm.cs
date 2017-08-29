using Max8.NET.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Max8.NET.ViewModels
{
    class MainVm : Vm
    {
        IReadOnlyList<PlayerVm> AllPlayers { get; }

        public MainVm()
        {
            AllPlayers = new List<PlayerVm>
            {
                new PlayerVm("Человек", false, null),
                new PlayerVm("ИИ 1 ур.", true, new Max8Ai { Depth = 1 }),
                new PlayerVm("ИИ 2 ур.", true, new Max8Ai { Depth = 2 }),
                new PlayerVm("ИИ 3 ур.", true, new Max8Ai { Depth = 3 }),
                new PlayerVm("ИИ 4 ур.", true, new Max8Ai { Depth = 4 }),
                new PlayerVm("ИИ 5 ур.", true, new Max8Ai { Depth = 5 })
            };
            PeopleMoveCommand = new DelegateCommand(o => PeopleMove((CellVm)o));
        }

        PlayerVm player1, player2, currentPlayer;
        public PlayerVm Player1
        {
            get => player1;
            set => Set(ref player1, value);
        }
        public PlayerVm Player2
        {
            get => player2;
            set => Set(ref player2, value);
        }
        public PlayerVm CurrentPlayer
        {
            get => currentPlayer;
            set => Set(ref currentPlayer, value);
        }

        bool isInGame = false;
        public bool IsInGame
        {
            get => isInGame;
            set => Set(ref isInGame, value);
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

        public ICommand PeopleMoveCommand;

        void NewGame()
        {
            if (Player1 == null ||
                Player2 == null) return;
            field = Field.CreateRandom();
            FieldVm = new FieldVm(field);
            Player1.Score = Player2.Score = 0;
            CurrentPlayer = Player1;
            CurX = CurY = 1;
            IsInGame = true;
            MoveEnded();
        }

        void PeopleMove(CellVm cellVm)
        {
            if (CurrentPlayer.IsAi) return;
            var index = (FieldVm.Cells as List<CellVm>).IndexOf(cellVm);
            CurX = index % FieldVm.FieldSize;
            CurY = index / FieldVm.FieldSize;
            CkeckLastCell();
        }

        void AiMove()
        {
            var d = CurrentPlayer == Player1 ? Direction.Horizontal : Direction.Vertical;
            if (d == Direction.Horizontal)
            {
                CurX = CurrentPlayer.Ai.FindBestMove(field, d, CurY);
            }
            else
            {
                CurY = CurrentPlayer.Ai.FindBestMove(field, d, CurX);
            }
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
            if (CurrentPlayer == Player2)
            {
                CurrentPlayer = Player1;
                IsInGame = FieldVm.ActivateHorizontal(CurY);
            }
            else
            {
                CurrentPlayer = Player2;
                IsInGame = FieldVm.ActivateVertical(CurX);
            }
            if (isInGame && CurrentPlayer.IsAi) AiMove();
        }
    }
}