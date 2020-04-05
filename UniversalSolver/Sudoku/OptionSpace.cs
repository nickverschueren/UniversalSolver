using System.Collections.Generic;

namespace UniversalSolver.Sudoku
{
    public class OptionSpace : IOptionSpace
    {
        public CellValue[,] CellValues { get; set; }
        internal (int x, int y)[,] Groups { get; set; }
        internal Dictionary<(int, int), int> Cell2Group { get; set; }

        public OptionSpace Clone()
        {
            var result = new OptionSpace
            {
                Groups = Groups,
                Cell2Group = Cell2Group,
                CellValues = new CellValue[CellValues.GetUpperBound(0) + 1, CellValues.GetUpperBound(1) + 1]
            };

            foreach (var (x, y, value) in CellValues.IterateAll())
            {
                result.CellValues[x, y] = value.Clone();
            }
            return result;
        }
    }

}
