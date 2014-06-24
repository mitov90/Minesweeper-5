namespace Minesweeper
{
    using System;

    public class Board
    {
        private int rows;
        private int columns;
        private int minesCount;
        private Field[,] fields;
        private Random random;

        public Board(int rows, int columns, int minesCount)
        {
            this.random = new Random();
            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;
            this.fields = new Field[rows, columns];

            for (int row = 0; row < this.fields.GetLength(0); row++)
            {
                for (int col = 0; col < this.fields.GetLength(1); col++)
                {
                    this.fields[row, col] = new Field();
                }
            }

            this.SetMines();
        }

        public void PrintGameBoard()
        {
            Console.Write("    ");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (int i = 0; i < this.rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (int j = 0; j < this.columns; j++)
                {
                    Field currentField = this.fields[i, j];
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
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public BoardStatus OpenField(int row, int column)
        {
            Field field = this.fields[row, column];
            BoardStatus status;

            if (field.Status == FieldStatus.IsAMine)
            {
                status = BoardStatus.SteppedOnAMine;
            }
            else if (field.Status == FieldStatus.Opened)
            {
                status = BoardStatus.AlreadyOpened;
            }
            else
            {
                field.Value = this.ScanSurroundingFields(row, column);
                field.Status = FieldStatus.Opened;
                if (this.CheckIfWin())
                {
                    status = BoardStatus.AllFieldsAreOpened;
                }
                else
                {
                    status = BoardStatus.SuccessfullyOpened;
                }
            }

            return status;
        }

        public void PrintAllFields()
        {
            Console.Write("    ");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write(i + " ");
            }

            Console.WriteLine();

            Console.Write("   _");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();

            for (int i = 0; i < this.rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (int j = 0; j < this.columns; j++)
                {
                    Field currentField = this.fields[i, j];
                    if (currentField.Status == FieldStatus.Opened)
                    {
                        Console.Write(this.fields[i, j].Value + " ");
                    }
                    else if (currentField.Status == FieldStatus.IsAMine)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        currentField.Value = this.ScanSurroundingFields(i, j);
                        Console.Write(this.fields[i, j].Value + " ");
                    }
                }

                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (int i = 0; i < this.columns; i++)
            {
                Console.Write("__");
            }

            Console.WriteLine();
        }

        public int CountOpenedFields()
        {
            int count = 0;
            for (int i = 0; i < this.fields.GetLength(0); i++)
            {
                for (int j = 0; j < this.fields.GetLength(1); j++)
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
            int number = this.random.Next(minValue, maxValue);
            return number;
        }

        private bool IsMineInPosition(int row, int column)
        {
            if ((0 <= row) && (row < this.rows)
                && (0 <= column) && (column < this.columns)
                && (this.fields[row, column].Status == FieldStatus.IsAMine))
            {
                return true;
            }

            return false;
        }

        private int ScanSurroundingFields(int row, int column)
        {
            int mines = 0;
            int previousRow = row - 1;
            int nextRow = row + 1;
            int previousColumn = column - 1;
            int nextColumn = column + 1;

            for (int i = previousRow; i <= nextRow; i++)
            {
                for (int j = previousColumn; j <= nextColumn; j++)
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
            for (int i = 0; i < this.minesCount; i++)
            {
                int row = this.GenerateRandomNumber(0, this.rows);
                int column = this.GenerateRandomNumber(0, this.columns);

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
