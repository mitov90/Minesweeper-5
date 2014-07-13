namespace Minesweeper
{
    using System;
    using Interfaces;

    /// <summary>
    /// Sets all mines at random position
    /// </summary>
    public class MineSetterVisitor : IVisitor
    {
        private Random rand = new Random();

        public void Visit(Board board)
        {
            for (var i = 0; i < board.MinesCount; i++)
            {
                var row = this.GenerateRandomNumber(0, board.Rows);
                var column = this.GenerateRandomNumber(0, board.Columns);

                if (board[row, column].Status == FieldStatus.IsAMine)
                {
                    i--;
                }
                else
                {
                    board[row, column].Status = FieldStatus.IsAMine;
                }
            }
        }

        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            var number = this.rand.Next(minValue, maxValue);
            return number;
        }
    }
}
