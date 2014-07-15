﻿namespace Minesweeper.Logic
{
    using System;
    using System.Collections.Generic;

    using Minesweeper.Enums;
    using Minesweeper.Interfaces;

    /// <summary>
    /// Represent console renderer
    /// </summary>
    public class Renderer : IRenderer
    {
        private const string BOMB_SYMBOL = "*";
        private const string UNCOVERED_FIELD_SYMBOL = "?";
        private const string SPACE = " ";
        private const string NO_PLAYERS_MESSAGE = "There is still no TOP players!---";
        private const string SCORE_BOARD_TITLE = "Scoreboard";

        public void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Welcome to the game “Minesweeper”!\nTry to reveal all cells without mines.\nPlease press:\n\n" +
                            "'" + ConsoleKey.T.ToString() + "' to view the scoreboard\n" +
                            "'" + ConsoleKey.N.ToString() + "' to start a new game\n" +
                            "'" + ConsoleKey.Q.ToString() + "' to quit the game!\n\n");
            Console.WriteLine();
        }

        public void Write(string input)
        {
            Console.WriteLine(input);
        }

        public void PrintAllFields(IBoard board, IBoardScanner boardScanner)
        {
            Console.Clear();
            this.PrintUpperBorder(board);

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
                            Console.Write(BOMB_SYMBOL + SPACE);
                            break;
                        default:
                            currentField.Value = boardScanner.ScanSurroundingFields(i, j);
                            Console.Write(board.FieldsMatrix[i, j].Value + " ");
                            break;
                    }
                }

                Console.WriteLine("|");
            }

            this.PrintBottomBorder(board);
        }      

        public void PrintGameBoard(IBoard board)
        {
            Console.Clear();
            this.PrintUpperBorder(board);

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
                        Console.Write(UNCOVERED_FIELD_SYMBOL + SPACE);
                    }
                }

                Console.WriteLine("|");
            }

            this.PrintBottomBorder(board);
        }

        public void PrintTopPlayers(List<IPlayer> players)
        {
            if (players.Count > 0)
            {
                Console.WriteLine(SCORE_BOARD_TITLE);
                for (var i = 0; i < players.Count; i++)
                {
                    var playerRank = i + 1;
                    Console.WriteLine(playerRank + ". " + players[i]);
                }
            }
            else
            {
                Console.WriteLine(NO_PLAYERS_MESSAGE);
            }
        }

        private void PrintUpperBorder(IBoard board)
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
        }

        private void PrintBottomBorder(IBoard board)
        {
            Console.Write("   _");
            for (var i = 0; i < board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }  
    }
}
