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

        public void PrintAllFields()
        {
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
