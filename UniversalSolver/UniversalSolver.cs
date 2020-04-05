using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalSolver
{
    public class UniversalSolver
    {
        private ISolverStrategy _solverStrategy;

        public int[,] Solve<T>(int?[,] problem, T solverStrategy) where T : ISolverStrategy
        {
            var tries = new Stack<IOptionSpace>();
            _solverStrategy = solverStrategy;
            _solverStrategy.Validate(problem);

            problem = problem.Transpose();

            var currentTry = _solverStrategy.GetOptions(problem);

            while (true)
            {
                var ordered = currentTry.CellValues.IterateAll()
                    .Where(o => o.value.Options.Length != 1)
                    .OrderBy(o => o.value.Options.Length).ToArray();

                if (!ordered.Any()) break;

                var (x, y, value) = ordered.First();
                var min = value.Options.Length;
                if (min == 0)
                {
                    if (tries.Count == 0)
                        throw new Exception("Unable to solve the problem");
                    currentTry = tries.Pop();
                    continue;
                }

                var (first, rest) = _solverStrategy.SplitFirst(currentTry, x, y);
                tries.Push(rest);
                currentTry = first;
            }

            var result = new int[problem.GetUpperBound(0) + 1, problem.GetUpperBound(1) + 1];
            foreach (var cell in currentTry.CellValues.IterateAll())
            {
                var (x, y, value) = cell;
                result[x, y] = value.Options[0];
            }

            result = result.Transpose();

            return result;
        }
    }
}
