namespace Minesweeper
{
    using System;
    using System.Text;

    public class Player : IComparable
    {
        private string name;
        private int score;

        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string Name
        {
            get { return this.name; }
        }

        public int Score
        {
            get { return this.score; }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Player))
            {
                throw new ArgumentException(
                   "A Player object is required for comparison.");
            }

            int comparison = this.score.CompareTo(((Player)obj).score);
            return -1 * comparison;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.name);
            result.Append(" --> ");
            result.Append(this.Score);
            return result.ToString();
        }
    }
}
