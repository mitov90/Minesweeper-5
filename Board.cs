namespace Minesweeper
{
    using System;

    public class Board
    {
        private readonly int rows;
        private readonly int columns;
        private readonly int minesCount;
        private readonly Field[,] fields;
        private readonly Random random;

        public Board(int rows, int columns, int minesCount)
        {
            this.random = new Random();
            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;
            this.fields = new Field[rows, columns];

            this.fields = PrepareMatrix(this.rows, this.columns);

            this.SetMines();
        }

        public static Field[,] PrepareMatrix(int rows, int cols)
        {
            var fieldMatrix = new Field[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    fieldMatrix[row, col] = new Field();
                }
            }

            return fieldMatrix;
        }

        public void PrintGameBoard()
        {
            Console.Write("    ");
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < this.rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < this.columns; j++)
                {
                    var currentField = this.fields[i, j];
                    if (currentField.Status == FieldStatus.Opened)
                    {
                        Console.Write(this.fields[i, j].Value);
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
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public BoardStatus OpenField(int row, int column)
        {
            var field = this.fields[row, column];
            BoardStatus status;

            switch (field.Status)
            {
                case FieldStatus.IsAMine:
                    status = BoardStatus.SteppedOnAMine;
                    break;
                case FieldStatus.Opened:
                    status = BoardStatus.AlreadyOpened;
                    break;
                default:
                    field.Value = this.ScanSurroundingFields(row, column);
                    field.Status = FieldStatus.Opened;
                    status = this.CheckIfWin() ? BoardStatus.AllFieldsAreOpened : BoardStatus.SuccessfullyOpened;
                    break;
            }

            return status;
        }

        public void PrintAllFields()
        {
            Console.Write("    ");
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (var i = 0; i < this.rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < this.columns; j++)
                {
                    var currentField = this.fields[i, j];
                    switch (currentField.Status)
                    {
                        case FieldStatus.Opened:
                            Console.Write(this.fields[i, j].Value + " ");
                            break;
                        case FieldStatus.IsAMine:
                            Console.Write("* ");
                            break;
                        default:
                            currentField.Value = this.ScanSurroundingFields(i, j);
                            Console.Write(this.fields[i, j].Value + " ");
                            break;
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public int CountOpenedFields()
        {
            var count = 0;
            for (var i = 0; i < this.fields.GetLength(0); i++)
            {
                for (var j = 0; j < this.fields.GetLength(1); j++)
                {
                    if (this.fields[i, j].Status == FieldStatus.Opened)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            var number = this.random.Next(minValue, maxValue);
            return number;
        }

        private bool IsMineInPosition(int row, int column)
        {
            return (0 <= row) && (row < this.rows)
                   && (0 <= column) && (column < this.columns)
                   && (this.fields[row, column].Status == FieldStatus.IsAMine);
        }

        private int ScanSurroundingFields(int row, int column)
        {
            var mines = 0;
            var previousRow = row - 1;
            var nextRow = row + 1;
            var previousColumn = column - 1;
            var nextColumn = column + 1;

            for (var i = previousRow; i <= nextRow; i++)
            {
                for (var j = previousColumn; j <= nextColumn; j++)
                {
                    if (!(i == row && j == column) && this.IsMineInPosition(i, j))
                    {
                        mines++;
                    }
                }
            }

            return mines;
        }

        private void SetMines()
        {
            for (var i = 0; i < this.minesCount; i++)
            {
                var row = this.GenerateRandomNumber(0, this.rows);
                var column = this.GenerateRandomNumber(0, this.columns);

                if (this.fields[row, column].Status == FieldStatus.IsAMine)
                {
                    i--;
                }
                else
                {
                    this.fields[row, column].Status = FieldStatus.IsAMine;
                }
            }
        }

        private bool CheckIfWin()
        {
            int openedFields = this.CountOpenedFields();
            if ((openedFields + this.minesCount) == (this.rows * this.columns))
            {
                return true;
            }

            return false;
        }
    }
}
