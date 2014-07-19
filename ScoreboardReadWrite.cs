namespace Minesweeper
{
    using System.Collections.Generic;

    using Minesweeper.Data;
    using Minesweeper.ReadWrite;

    public class ScoreboardReadWrite
    {
        internal readonly GameData gameData;

        public void SaveScoreboard(List<Player> players)
        {
            FileReadWrite.Serialize(players, @"..\..\Scoreboard.bin");
        }

        public void ReadScoreboard()
        {
            var players = new List<Player>();

            if (null != (List<Player>)FileReadWrite.Deserialize(@"..\..\Scoreboard.bin"))
            {
                players = (List<Player>)FileReadWrite.Deserialize(@"..\..\Scoreboard.bin");

                for (int i = 0; i < players.Count; i++)
                {
                    this.gameData.Highscore.AddTopPlayer(players[i]);
                }
            }
        }
    }
}