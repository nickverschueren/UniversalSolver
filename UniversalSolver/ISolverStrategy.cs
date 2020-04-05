namespace UniversalSolver
{
    public interface ISolverStrategy
    {
        void Validate(int?[,] problem);
        IOptionSpace GetOptions(int?[,] problem);
        (IOptionSpace first, IOptionSpace rest) SplitFirst(IOptionSpace current, int x, int y);
    }
}
