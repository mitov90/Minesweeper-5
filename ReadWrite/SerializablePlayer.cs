namespace Minesweeper.ReadWrite
{
    using System;

    [Serializable]
    public class SerializablePlayer
    {
        public SerializablePlayer(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        public string Name { get; set; }

        public int Score { get; set; }
    }
}
