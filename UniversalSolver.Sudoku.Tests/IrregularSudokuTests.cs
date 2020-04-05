using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace UniversalSolver.Sudoku.Tests
{
    public class IrregularSudokuTests
    {
        [Theory]
        [ClassData(typeof(SudokuData))]
        public void Should_Solve_An_Irregular_Sudoku_Problem(int size, int?[,] given, int[,] layout, int[,] expected)
        {
            var target = new UniversalSolver();
            var sudokuStrategy = new IrregularSudokuSolverStrategy(size, layout);
            var actual = target.Solve(given, sudokuStrategy);
            Assert.Equal(expected, actual);
        }

        private class SudokuData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, new int?[,] { { null } }, new int[,] { { 1 } }, new int[,] { { 1 } } };
                yield return new object[] { 1, new int?[,] { { 1 } }, new int[,] { { 1 } }, new int[,] { { 1 } } };
                yield return new object[] { 9, SudokuProblem9, SudokuLayout9, SudokuSolution9 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            static readonly int?[,] SudokuProblem9 = new int?[9, 9]
                {
                { null, null,    9,    1, null, null, null, null, null },
                { null,    7,    8, null, null, null, null,    2, null },
                {    6, null, null, null,    9, null, null, null, null },
                { null, null, null, null,    1, null, null, null, null },
                { null, null,    3, null, null,    5, null, null, null },
                { null, null,    1,    7,    2, null, null,    9,    8 },
                { null, null, null,    2,    4, null, null, null, null },
                { null,    8,    4, null, null, null, null, null, null },
                { null, null, null, null, null, null, null,    7, null }
                };

            static readonly int[,] SudokuLayout9 = new int[9, 9]
                {
                { 1, 1, 2, 2, 2, 2, 2, 2, 2 },
                { 1, 1, 2, 2, 3, 3, 3, 4, 4 },
                { 1, 1, 5, 5, 5, 5, 3, 4, 4 },
                { 1, 1, 1, 5, 3, 3, 3, 4, 6 },
                { 7, 7, 5, 5, 3, 8, 3, 4, 6 },
                { 7, 7, 7, 5, 8, 8, 4, 4, 6 },
                { 7, 9, 9, 5, 8, 8, 4, 6, 6 },
                { 7, 7, 9, 9, 9, 8, 6, 6, 6 },
                { 7, 9, 9, 9, 9, 8, 8, 8, 6 }
                };

            static readonly int[,] SudokuSolution9 = new int[9, 9]
                {
                { 8, 3, 9, 1, 7, 2, 6, 4, 5 },
                { 1, 7, 8, 3, 5, 4, 9, 2, 6 },
                { 6, 4, 5, 8, 9, 1, 2, 3, 7 },
                { 9, 5, 2, 6, 1, 7, 3, 8, 4 },
                { 7, 9, 3, 4, 6, 5, 8, 1, 2 },
                { 5, 6, 1, 7, 2, 3, 4, 9, 8 },
                { 3, 1, 7, 2, 4, 8, 5, 6, 9 },
                { 2, 8, 4, 9, 3, 6, 7, 5, 1 },
                { 4, 2, 6, 5, 8, 9, 1, 7, 3 }
                };
        }
    }
}
