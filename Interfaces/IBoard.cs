namespace Minesweeper.Interfaces
{
    public interface IBoard
    {
        int Rows { get; }

        int Columns { get; }

        int MinesCount { get; }

        Field[,] FieldsMatrix { get; }
    }
}
