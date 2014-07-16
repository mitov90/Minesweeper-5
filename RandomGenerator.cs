namespace Minesweeper
{
    using System;

    public class RandomGenerator
    {
        private Random random;
    
        public RandomGenerator()
        {
            this.random = new Random();
        }

        public int GenerateRandomNumber(int minValue, int maxValue)
        {
            var number = random.Next(minValue, maxValue);
            return number;
        }
    }
}
