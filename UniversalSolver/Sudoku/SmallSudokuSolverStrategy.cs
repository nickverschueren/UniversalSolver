﻿namespace UniversalSolver.Sudoku
{

    public class SmallSudokuSolverStrategy : SudokuSolverStrategyBase
    {
        protected readonly int _groupSize;

        public SmallSudokuSolverStrategy() : base(6)
        {
        }

        protected override (int x, int y)[,] GetGroups()
        {
            var result = new (int x, int y)[6, 6]
            {
                { (0,0),(1,0),(2,0),(0,1),(1,1),(2,1) },
                { (3,0),(4,0),(5,0),(3,1),(4,1),(5,1) },
                { (0,2),(1,2),(2,2),(0,3),(1,3),(2,3) },
                { (3,2),(4,2),(5,2),(3,3),(4,3),(5,3) },
                { (0,4),(1,4),(2,4),(0,5),(1,5),(2,5) },
                { (3,4),(4,4),(5,4),(3,5),(4,5),(5,5) }
            };
            return result;
        }
    }
}
