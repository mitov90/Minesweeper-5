using System;

namespace Minesweeper
{
    internal class Player : IComparable
    {
        private readonly string _name;
        private readonly int _score;

        public Player(string name, int score)
        {
            _name = name;
            _score = score;
        }

        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get { return _score; }
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Player))
            {
                throw new ArgumentException(
                    "A Player object is required for comparison.");
            }
            return -1*_score.CompareTo(((Player) obj)._score);
        }

        public override string ToString()
        {
            return _name + " --> " + _score;
        }
    }
}