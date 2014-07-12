namespace Minesweeper.Interfaces
{
    interface IBoardManager
    {
        BoardStatus OpenField(int row, int column);
        int CountOpenedFields();
    }
}
