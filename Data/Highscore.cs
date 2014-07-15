namespace Minesweeper.Data
{
    using System;
    using System.Collections.Generic;

    using Minesweeper.Interfaces;

    public class Highscore : IHighscore
    {
        private const int MAX_TOP_PLAYERS = 5;

        private List<IPlayer> topPlayers;

        public Highscore()
        {
            this.topPlayers = new List<IPlayer> { Capacity = MAX_TOP_PLAYERS };
        }

        public List<IPlayer> TopPlayers
        {
            get
            {
                return new List<IPlayer>(this.topPlayers);
            }

            private set
            {
                this.topPlayers = value;
            }
        }

        public bool IsHighScore(int currentPlayerScore)
        {
            if (this.topPlayers.Capacity > this.topPlayers.Count)
            {
                return true;
            }

            foreach (var player in this.topPlayers)
            {
                var topPlayer = (Player)player;
                if (topPlayer.Score < currentPlayerScore)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddTopPlayer(Player currentPlayer)
        {
            if (currentPlayer == null)
            {
                throw new ArgumentNullException("currentPlayer");
            }

            if (this.topPlayers.Capacity > this.topPlayers.Count)
            {
                this.topPlayers.Add(currentPlayer);
                this.topPlayers.Sort();
            }
            else
            {
                var lastTopPlayerIndex = this.topPlayers.Capacity - 1;

                this.topPlayers.RemoveAt(lastTopPlayerIndex);
                this.topPlayers.Add(currentPlayer);
                this.topPlayers.Sort();
            }
        }
    }
}
