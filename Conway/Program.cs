using System;
using System.Collections.Generic;
using System.Linq;

namespace Conway
{
    class Program
    {
        static void Main(string[] args)
        {
            static void GlyphWrite(Cell cell) =>
                Console.Write(cell.CellCondition == Condition.Alive ? '#' : ' ');

            var cells = new List<Cell>();

            foreach (var x in Enumerable.Range(0, 8))
            {
                foreach (var y in Enumerable.Range(0, 8))
                {
                    cells.Add(new Cell(x, y, Condition.Dead));
                }
            }

            cells[15].CellCondition = Condition.Alive;
            cells[17].CellCondition = Condition.Alive;
            cells[23].CellCondition = Condition.Alive;
            cells[25].CellCondition = Condition.Alive;
            
            Console.Clear();
            foreach (var cell in cells)
            {
                Console.SetCursorPosition(cell.X, cell.Y);
                GlyphWrite(cell);
            }

            var gen = 1;
            while (true)
            {
                //Loop
                Console.SetCursorPosition(12, 8);
                Console.Write("Generation " + gen);
                System.Threading.Thread.Sleep(2000);

                foreach (var cell in cells)
                {
                    cell.ChangeState(cells);
                }

                foreach (var cell in cells)
                {
                    Console.SetCursorPosition(cell.X, cell.Y);
                    GlyphWrite(cell);
                }

                gen++;
            }
        }
    }
}

/*
Conway's GameOfLife

It is a cellular automaton devised by the British mathematician John Horton Conway in 1970. 
It is a zero-player game, meaning that its evolution is determined by its initial state, requiring no further input. 
You just have to observe how it evolves. How it works : Every cell, which are placed on a grid, interacts with its eight neighbours, 
which are the cells that are horizontally, vertically, or diagonally adjacent. At each step in time, the following transitions occur: 
- Any live cell with fewer than two live neighbours dies, as if by underpopulation. 
- Any live cell with two or three live neighbours lives on to the next generation. - Any live cell with more than three live neighbours dies,
 as if by overpopulation. - Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
*/