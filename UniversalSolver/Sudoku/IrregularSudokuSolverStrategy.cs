using System;
using System.Linq;

namespace UniversalSolver.Sudoku
{
    public class IrregularSudokuSolverStrategy : SudokuSolverStrategyBase
    {
        private readonly int[,] _groupLayout;

        public IrregularSudokuSolverStrategy(int size, int[,] groupLayout) : base(size)
        {
            var sqrt = Math.Sqrt(Size);
            if (sqrt != Convert.ToInt32(Math.Truncate(sqrt)))
                throw new ArgumentException("Must always be a square of a whole number", nameof(size));
            var groups = groupLayout.Cast<int>().GroupBy(i => i);
            if (groups.Count() != size)
                throw new ArgumentException($"Must have exactly {size} groups", nameof(groupLayout));
            var counts = groups.Select(g => g.Count());
            if (counts.Min() != size || counts.Max() != size)
                throw new ArgumentException($"All groups must contain {size} cells", nameof(groupLayout));
            _groupLayout = groupLayout;
        }

        protected override (int x, int y)[,] GetGroups()
        {
            var result = new (int x, int y)[Size, Size];
            var i = 0;
            foreach (var g in _groupLayout.Transpose().IterateAll()
                .GroupBy(c => c.value))
            {
                var j = 0;
                foreach (var c in g)
                {
                    result[i, j] = (c.x, c.y);
                    j++;
                }
                i++;
            }

            return result;
        }
    }
}
