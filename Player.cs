﻿namespace Minesweeper
{
    using System;
    using System.Text;

    public class Player : IComparable
    {
        private string name;
        private int score;

        public Player(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        public string Name
        {
            get 
            { 
                return this.name;
            }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Invalid name of the player.");
                }

                this.name = value;
            }
        }

        public int Score
        {
            get 
            { 
                return this.score;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid score of the player. Score must be positive number.");
                }

                this.score = value;
            }
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
