using System;

namespace UniversalSolver
{
    public class CellValue
    {
        public CellValue(int fixedValue)
        {
            Options = new[] { fixedValue };
            IsFixed = true;
        }

        public CellValue(int[] options)
        {
            Options = options;
            IsFixed = false;
        }

        public readonly int[] Options;
        public int FixedValue => IsFixed ? Options[0] : throw new InvalidOperationException();
        public readonly bool IsFixed;

        public CellValue Clone()
        {
            if (IsFixed) return new CellValue(FixedValue);
            var copy = new int[Options.Length];
            Options.CopyTo(copy, 0);
            return new CellValue(copy);
        }

        public override string ToString()
        {
            return IsFixed ? Options[0].ToString() : $"[{string.Join(",", Options)}]";
        }
    }
}
