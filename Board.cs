namespace Minesweeper
{
    using System;
    using Interfaces;

    public class Board
    {
        private int rows;
        private int columns;
        private int minesCount;
        private Field[,] fields;

        public Board(int rows, int columns, int minesCount)
        {
            this.rows = rows;
            this.columns = columns;
            this.minesCount = minesCount;
            this.fields = new Field[rows, columns];

            this.fields = PrepareMatrix(this.rows, this.columns);
        }

        public int Rows
        {
            get
            {
                return this.rows;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Rows cannot be less than zero.");
                }

                this.rows = value;
            }
        }

        public int Columns
        {
            get
            {
                return this.columns;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Columns cannot be less than zero.");
                }

                this.columns = value;
            }
        }

        public int MinesCount
        {
            get
            {
                return this.minesCount;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("There should be positive number of mines in the game.");
                }

                this.minesCount = value;
            }
        }

        public Field this[int row, int col]
        {
            get
            {
                if (!this.IsPositionValid(row, col))
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                return this.fields[row, col];
            }

            set
            {
                if (!this.IsPositionValid(row, col))
                {
                    throw new IndexOutOfRangeException("Invalid index");
                }

                this.fields[row, col] = value;
            }
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

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
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
        
        private bool CheckIfWin()
        {
            int openedFields = this.CountOpenedFields();
            if ((openedFields + this.minesCount) == (this.rows * this.columns))
            {
                return true;
            }

            return false;
        }

        private bool IsPositionValid(int row, int col)
        {
            if ((row < 0 || row >= this.fields.GetLength(0)) || (col < 0 || col >= this.fields.GetLength(1)))
            {
                return false;
            }

            return true;
        }
    }
}
