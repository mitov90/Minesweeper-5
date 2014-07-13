namespace Minesweeper
{
    using System;
    using Interfaces;

    public class Renderer : IRenderer
    {
        private IBoard board;
        private IBoardScanner boardScanner;

        public Renderer(IBoard board, IBoardScanner boardScanner)
        {
            this.board = board;
            this.boardScanner = boardScanner;
        }

        public static void Write(string input)
        {
            Console.WriteLine(input);
        }

        public static void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Welcome to the game “Minesweeper”!\nTry to reveal all cells without mines.\nPlease press:\n\n" +
                            "'" + ConsoleKey.T.ToString() + "' to view the scoreboard\n" +
                            "'" + ConsoleKey.N.ToString() + "' to start a new game\n" +
                            "'" + ConsoleKey.Q.ToString() + "' to quit the game!\n\n");
            Console.WriteLine();
        }

        public void PrintAllFields()
        {
            Console.Clear();
            Console.Write("    ");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < this.board.Rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < this.board.Columns; j++)
                {
                    var currentField = this.board.FieldsMatrix[i, j];
                    switch (currentField.Status)
                    {
                        case FieldStatus.Opened:
                            Console.Write(this.board.FieldsMatrix[i, j].Value + " ");
                            break;
                        case FieldStatus.IsAMine:
                            Console.Write("* ");
                            break;
                        default:
                            currentField.Value = this.boardScanner.ScanSurroundingFields(i, j);
                            Console.Write(this.board.FieldsMatrix[i, j].Value + " ");
                            break;
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public void PrintGameBoard()
        {
            Console.Clear();
            Console.Write("    ");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < this.board.Rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < this.board.Columns; j++)
                {
                    var currentField = this.board.FieldsMatrix[i, j];
                    if (currentField.Status == FieldStatus.Opened)
                    {
                        Console.Write(this.board.FieldsMatrix[i, j].Value);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write("? ");
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < this.board.Columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }
    }
}
