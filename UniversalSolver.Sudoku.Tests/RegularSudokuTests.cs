using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace UniversalSolver.Sudoku.Tests
{
    public class RegularSudokuTests
    {
        [Theory]
        [ClassData(typeof(SudokuData))]
        public void Should_Solve_A_Regular_Sudoku_Problem(int size, int?[,] given, int[,] expected)
        {
            var target = new UniversalSolver();
            var sudokuStrategy = new RegularSudokuSolverStrategy(size);
            var actual = target.Solve(given, sudokuStrategy);
            Assert.Equal(expected, actual);
        }

        private class SudokuData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, new int?[,] { { null } }, new int[,] { { 1 } } };
                yield return new object[] { 1, new int?[,] { { 1 } }, new int[,] { { 1 } } };
                yield return new object[] { 4, SudokuProblem4, SudokuSolution4 };
                yield return new object[] { 9, SudokuProblem9, SudokuSolution9 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            static readonly int?[,] SudokuProblem4 = new int?[4, 4]
                {
                    { null,    3,    4, null },
                    {    4, null, null,    2 },
                    {    1, null, null,    3 },
                    { null,    2,    1, null }
                };

            static readonly int[,] SudokuSolution4 = new int[4, 4]
                {
                    { 2, 3, 4, 1 },
                    { 4, 1, 3, 2 },
                    { 1, 4, 2, 3 },
                    { 3, 2, 1, 4 }
                };

            static readonly int?[,] SudokuProblem9 = new int?[9, 9]
                {
                { null,    7, null,    4,    2, null, null,    1, null },
                {    6, null,    4, null, null,    3,    9,    2, null },
                { null,    3, null,    9, null, null, null,    6, null },
                { null, null, null, null, null,    2,    3, null,    9 },
                {    2, null, null, null,    6, null, null, null,    7 },
                { null, null,    1, null, null,    9, null,    4, null },
                {    9, null, null, null, null,    5, null,    3, null },
                { null,    4,    5, null, null, null,    8, null,    2 },
                { null,    2, null, null, null,    8, null,    7, null }
                };

            static readonly int[,] SudokuSolution9 = new int[9, 9]
                {
                    { 8, 7, 9, 4, 2, 6, 5, 1, 3 },
                    { 6, 1, 4, 7, 5, 3, 9, 2, 8 },
                    { 5, 3, 2, 9, 8, 1, 7, 6, 4 },
                    { 4, 6, 8, 1, 7, 2, 3, 5, 9 },
                    { 2, 9, 3, 5, 6, 4, 1, 8, 7 },
                    { 7, 5, 1, 8, 3, 9, 2, 4, 6 },
                    { 9, 8, 7, 2, 4, 5, 6, 3, 1 },
                    { 3, 4, 5, 6, 1, 7, 8, 9, 2 },
                    { 1, 2, 6, 3, 9, 8, 4, 7, 5 }
                };
        }
    }
}
