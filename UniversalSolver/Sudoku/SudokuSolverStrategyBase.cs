using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalSolver.Sudoku
{
    public abstract class SudokuSolverStrategyBase : ISolverStrategy
    {
        protected SudokuSolverStrategyBase(int size)
        {
            Size = size;
        }

        protected int Size { get; }

        public void Validate(int?[,] problem)
        {
            if (Size < 1)
                throw new ArgumentException("The minimum size is 1", nameof(problem));
            if (problem.GetUpperBound(0) != Size - 1)
                throw new ArgumentException($"The width should be {Size}", nameof(problem));
            if (problem.GetUpperBound(1) != Size - 1)
                throw new ArgumentException($"The height should be {Size}", nameof(problem));
            if (problem.Cast<int?>().Min(i => i ?? 1) < 1)
                throw new ArgumentException("No number in the problem should be less than 1", nameof(problem));
            if (problem.Cast<int?>().Max(i => i ?? 1) > Size)
                throw new ArgumentException($"No number in the problem should be greater than {Size}", nameof(problem));
        }

        public IOptionSpace GetOptions(int?[,] problem)
        {
            var groups = GetGroups();
            var cell2Group =
                groups.IterateAll()
                    .ToDictionary(g => g.value, g => g.x);

            var optionSpace = new OptionSpace
            {
                Groups = groups,
                Cell2Group = cell2Group
            };

            var options = new CellValue[Size, Size];
            foreach (var cell in options.IterateAll())
            {
                var (x, y, _) = cell;
                options[x, y] = GetOptionsForCell(x, y, problem, optionSpace);
            }

            optionSpace.CellValues = options;
            return optionSpace;
        }

        private CellValue GetOptionsForCell(int x, int y, int?[,] problem, OptionSpace optionSpace)
        {
            var problemValue = problem[x, y];
            if (problemValue.HasValue)
                return new CellValue(problemValue.Value);
            else
                return new CellValue(GetOptionsFromProblem(x, y, problem, optionSpace));
        }

        public (IOptionSpace first, IOptionSpace rest) SplitFirst(IOptionSpace current, int x, int y)
        {
            if (current is OptionSpace optionSpace)
            {
                var first = optionSpace.Clone();
                var firstValue = current.CellValues[x, y].Options.First();

                first.CellValues[x, y] = new CellValue(firstValue);
                Resolve(first, x, y, firstValue);

                var rest = optionSpace.Clone();
                var restOptions = current.CellValues[x, y].Options.Skip(1).ToArray();

                if (restOptions.Length == 1)
                {
                    rest.CellValues[x, y] = new CellValue(restOptions[0]);
                    Resolve(rest, x, y, restOptions[0]);
                }
                else
                    rest.CellValues[x, y] = new CellValue(restOptions);

                return (first, rest);
            }
            throw new ArgumentException($"Not of type {typeof(OptionSpace).FullName}", nameof(current));
        }

        private void Resolve(OptionSpace optionSpace, int x, int y, int value)
        {
            var updated = (x, y);
            var singleValues = new List<(int x, int y, int value)>();
            foreach (var cell in optionSpace.CellValues.IterateX(y).Where(c => c.x != x))
            {
                var toUpdate = (cell.x, y);
                var (newCellValue, update) = ResolveCell(toUpdate, updated, value, optionSpace, singleValues);
                if (update)
                    optionSpace.CellValues[cell.x, y] = newCellValue;
            }
            foreach (var cell in optionSpace.CellValues.IterateY(x).Where(c => c.y != y))
            {
                var toUpdate = (x, cell.y);
                var (newCellValue, update) = ResolveCell(toUpdate, updated, value, optionSpace, singleValues);
                if (update)
                    optionSpace.CellValues[x, cell.y] = newCellValue;
            }
            foreach (var (_, toUpdate) in optionSpace.Groups.IterateY(optionSpace.Cell2Group[(x, y)]))
            {
                var (newCellValue, update) = ResolveCell(toUpdate, updated, value, optionSpace, singleValues);
                if (update)
                    optionSpace.CellValues[toUpdate.x, toUpdate.y] = newCellValue;
            }

            foreach (var cell in singleValues.Distinct())
            {
                Resolve(optionSpace, cell.x, cell.y, cell.value);
            }
        }

        private (CellValue newCellValue, bool update) ResolveCell((int, int) toUpdate,
            (int, int) updated, int updatedValue, OptionSpace optionSpace,
            List<(int, int, int)> singleValues)
        {
            var (cx, cy) = toUpdate;
            var (x, y) = updated;
            if (cx == x && cy == y) return (null, false);
            var cell = optionSpace.CellValues[cx, cy];
            if (cell.IsFixed) return (null, false);
            var cellValue = new CellValue(cell.Options.Where(o => o != updatedValue).ToArray());
            if (cellValue.Options.Length == 1 &&
                optionSpace.CellValues[cx, cy].Options.Length != 1)
                singleValues.Add((cx, cy, cellValue.Options[0]));
            return (cellValue, true);
        }

        private int[] GetOptionsFromProblem(int x, int y, int?[,] problem, OptionSpace optionSpace)
        {
            var result = Enumerable.Range(1, Size);
            var except = problem.IterateX(y).Where(c => c.value.HasValue).Select(c => c.value.Value);
            except = except.Concat(problem.IterateY(x).Where(c => c.value.HasValue).Select(c => c.value.Value));
            except = except.Concat(optionSpace.Groups.IterateY(optionSpace.Cell2Group[(x, y)])
               .Select(c => problem[c.value.x, c.value.y]).Where(c => c.HasValue).Select(c => c.Value));
            except = except.Distinct();
            result = result.Except(except);
            return result.ToArray();
        }

        protected abstract (int x, int y)[,] GetGroups();
    }
}
