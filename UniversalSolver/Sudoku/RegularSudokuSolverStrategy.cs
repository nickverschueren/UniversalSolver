using System;

namespace UniversalSolver.Sudoku
{

    public class RegularSudokuSolverStrategy : SudokuSolverStrategyBase
    {
        protected readonly int _groupSize;

        public RegularSudokuSolverStrategy(int size) : base(size)
        {
            var groupSize = Math.Sqrt(Size);
            _groupSize = Convert.ToInt32(Math.Truncate(groupSize));
            if (groupSize != _groupSize)
                throw new ArgumentException("Must always be a square of a whole number", nameof(size));
        }

        protected override (int x, int y)[,] GetGroups()
        {
            var result = new (int, int)[Size, Size];
            int g = 0;
            for (int y = 0; y < _groupSize; y++)
            {
                var yOffset = y * _groupSize;
                for (int x = 0; x < _groupSize; x++)
                {
                    var xOffset = x * _groupSize;
                    int c = 0;
                    for (int dx = 0; dx < _groupSize; dx++)
                    {
                        for (int dy = 0; dy < _groupSize; dy++)
                        {
                            result[g, c++] = (xOffset + dx, yOffset + dy);
                        }
                    }
                    g++;
                }
            }
            return result;
        }
    }
}
