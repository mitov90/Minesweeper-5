namespace Minesweeper
{
    using System;

    public class RandomGenerator
    {
        private static Random random = new Random();

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            var number = random.Next(minValue, maxValue);
            return number;
        }
    }
}
