using System;
using System.Collections.Generic;
using System.Linq;

namespace Conway
{
    class Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Condition CellCondition { get; set; }

        public Cell(
            int x
            , int y
            , Condition cellCondition)
        {
            X = x;
            Y = y;
            CellCondition = cellCondition;
        }
        
        //Functions, don't change state.
        private static readonly Func<Cell, bool> IsAliveP =
            cell => cell.CellCondition == Condition.Alive;

        private static readonly Func<Cell, int, int, bool> TopRightP =
            (cell, x, y) => cell.X == x + 1 && cell.Y == y - 1 && IsAliveP(cell);

        private static readonly Func<Cell, int, int, bool> TopLeftP =
            (cell, x, y) => cell.X == x - 1 && cell.Y == y - 1 && IsAliveP(cell);

        private static  readonly Func<Cell, int, int, bool> BottomLeftP =
            (cell, x, y) => cell.X == x - 1 && cell.Y == y + 1 && IsAliveP(cell);

        private static readonly Func<Cell, int, int, bool> BottomRightP =
            (cell, x, y) => cell.X == x + 1 && cell.Y == y + 1 && IsAliveP(cell);
        
        //Methods, don't change state.
        private int CheckHorizontal(IEnumerable<Cell> cells) =>
            cells.Count(cell => cell.Y == Y - 1 && IsAliveP(cell) || cell.Y == Y + 1 && IsAliveP(cell));

        private int CheckVertical(IEnumerable<Cell> cells) =>
            cells.Count(cell => cell.X == X - 1 && IsAliveP(cell) || cell.X == X + 1 && IsAliveP(cell));

        private int CheckDiagonal(List<Cell> cells) =>
            cells.Count(cell => TopLeftP(cell, X, Y)
                                && TopRightP(cell, X, Y)
                                && BottomLeftP(cell, X, Y)
                                && BottomRightP(cell, X, Y));

        private int CountUp(List<Cell> cells) =>
            CheckHorizontal(cells) + CheckVertical(cells) + CheckDiagonal(cells);
    
        //Changes state.
        public void ChangeState(List<Cell> cells) =>
            CellCondition = CountUp(cells) > 2 && CountUp(cells) < 5 ? Condition.Alive: Condition.Dead;
    }
}