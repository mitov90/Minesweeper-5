namespace Minesweeper
{
    using System;
    using Interfaces;
    using System.Collections.Generic;

    public class Renderer : IRenderer
    {
        private const string bombSymbol = "*";
        private const string uncoveredFieldSymbol = "?";
        private const string space = " ";
        private const string noPlayersRecordedMessage = "There is still no TOP players!---";
        private const string scoreBoardTitle = "Scoreboard";

        public void Write(string input)
        {
            Console.WriteLine(input);
        }

        public static void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the game “Minesweeper”!\nTry to reveal all cells without mines.\nPlease press:\n\n" +
                            "'" + ConsoleKey.T.ToString() + "' to view the scoreboard\n" +
                            "'" + ConsoleKey.N.ToString() + "' to start a new game\n" +
                            "'" + ConsoleKey.Q.ToString() + "' to quit the game!\n\n");
            Console.WriteLine();
        }

        public void PrintAllFields(IBoard board, IBoardScanner boardScanner)
        {
            Console.Write("    ");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < board.Rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < board.Columns; j++)
                {
                    var currentField = board.FieldsMatrix[i, j];
                    switch (currentField.Status)
                    {
                        case FieldStatus.Opened:
                            Console.Write(board.FieldsMatrix[i, j].Value + " ");
                            break;
                        case FieldStatus.IsAMine:
                            Console.Write(bombSymbol + space);
                            break;
                        default:
                            currentField.Value = boardScanner.ScanSurroundingFields(i, j);
                            Console.Write(board.FieldsMatrix[i, j].Value + " ");
                            break;
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public void PrintGameBoard(IBoard board)
        {
            Console.Write("    ");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < board.Rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < board.Columns; j++)
                {
                    var currentField = board.FieldsMatrix[i, j];
                    if (currentField.Status == FieldStatus.Opened)
                    {
                        Console.Write(board.FieldsMatrix[i, j].Value);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(uncoveredFieldSymbol + space);
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public void PrintTopPlayers(List<IPlayer> players)
        {
            if (players.Count > 0)
            {
                Console.WriteLine(scoreBoardTitle);
                for (var i = 0; i < players.Count; i++)
                {
                    var playerRank = i + 1;
                    Console.WriteLine(playerRank + ". " + players[i]);
                }
            }
            else
            {
                Console.WriteLine(noPlayersRecordedMessage);
            }
        }
    }
}
