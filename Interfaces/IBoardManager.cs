namespace Minesweeper.Interfaces
{
    public interface IBoardManager
    {
        BoardStatus OpenField(int row, int column);

        int CountOpenedFields();
    }
}
