namespace Minesweeper.Interfaces
{
    using System.Collections.Generic;

    public interface IHighscore
    {
        List<IPlayer> TopPlayers { get; }

        bool IsHighScore(int currentPlayerScore);

        void AddTopPlayer(Player currentPlayer);
    }
}
