namespace Minesweeper.Interfaces
{
    using System.Collections.Generic;

    public interface IRenderer
    {
        void Write(string str);

        void PrintGameBoard();

        void PrintAllFields();

        void PrintTopPlayers(List<IPlayer> players);
    }
}
