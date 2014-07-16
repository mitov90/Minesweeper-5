namespace Minesweeper
{
    using System;
    using Interfaces;

    using Minesweeper.Data;
    using Minesweeper.Enums;

    /// <summary>
    /// Sets all mines at random position
    /// </summary>
    public class MineSetterVisitor : IVisitor
    {
        public void Visit(Board board)
        {
            for (var i = 0; i < board.MinesCount; i++)
            {
                var row = RandomGenerator.GenerateRandomNumber(0, board.Rows);
                var column = RandomGenerator.GenerateRandomNumber(0, board.Columns);

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
    }
}
