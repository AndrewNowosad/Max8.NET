﻿using System;

namespace Max8.NET.Models
{
    class Max8Ai
    {
        int depth;
        public int Depth
        {
            get => depth;
            set => depth = Math.Max(1, value);
        }

        public int FindBestMove(Field field, Direction direction, int curPos)
            => FindBestMove(field.CreateCopy(), direction, curPos, Depth).Coordinate.Value;

        struct Result
        {
            public int? Coordinate { get; }
            public int Score { get; }
            public Result(int? coordinate, int score)
            {
                Coordinate = coordinate;
                Score = score;
            }
        }

        Result FindBestMove(Field field, Direction direction, int curPos, int depth)
        {
            if (depth < 1) return new Result(null, 0);
            if (direction == Direction.Horizontal)
                field.Transpose();
            int? max = null;
            int? index = null;
            for (int y = 0; y < Field.Width; y++)
                if (field[curPos, y].IsActive)
                {
                    var res = field[curPos, y].Value;
                    if (depth > 1)
                    {
                        var fNext = field.CreateCopy();
                        fNext.DeactivateCell(curPos, y);
                        res -= FindBestMove(fNext, Direction.Horizontal, y, depth - 1).Score;
                    }
                    if (!max.HasValue || res > max)
                    {
                        max = res;
                        index = y;
                    }
                }
            return new Result(index, max ?? 0);
        }
    }
}