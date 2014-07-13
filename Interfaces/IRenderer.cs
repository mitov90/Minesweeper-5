namespace Minesweeper.Interfaces
{
    public interface IRenderer
    {
        void Write(string str);

        void PrintGameBoard();

        void PrintAllFields();
    }
}
