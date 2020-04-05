using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace UniversalSolver.Sudoku.Tests
{
    public class SmallSudokuTests
    {
        [Theory]
        [ClassData(typeof(SudokuData))]
        public void Should_Solve_A_Small_Sudoku_Problem(int?[,] given, int[,] expected)
        {
            var target = new UniversalSolver();
            var sudokuStrategy = new SmallSudokuSolverStrategy();
            var actual = target.Solve(given, sudokuStrategy);
            Assert.Equal(expected, actual);
        }

        private class SudokuData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SudokuProblem1, SudokuSolution1 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            static readonly int?[,] SudokuProblem1 = new int?[6, 6]
                {
                {    1,    2, null,    3, null,    5 },
                { null, null,    3, null,    4, null },
                {    6, null,    2, null, null,    1 },
                {    4,    1,    5,    2,    3, null },
                { null,    5,    1, null, null, null },
                { null, null, null, null, null, null }
                };

            static readonly int[,] SudokuSolution1 = new int[6, 6]
                {
                { 1, 2, 4, 3, 6, 5 },
                { 5, 6, 3, 1, 4, 2 },
                { 6, 3, 2, 4, 5, 1 },
                { 4, 1, 5, 2, 3, 6 },
                { 3, 5, 1, 6, 2, 4 },
                { 2, 4, 6, 5, 1, 3 }
                };
        }
    }
}
