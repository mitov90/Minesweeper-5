using System;
using System.Linq;

namespace Minesweeper
{
    internal class Board
    {
        public enum Status
        {
            SteppedOnAMine,
            AlreadyOpened,
            SuccessfullyOpened,
            AllFieldsAreOpened
        }

        private readonly int _columns;
        private readonly Field[][] _fields;
        private readonly int _minesCount;
        private readonly Random _random;
        private readonly int _rows;

        public Board(int rows, int columns, int minesCount)
        {
            _random = new Random();
            _rows = rows;
            _columns = columns;
            _minesCount = minesCount;
            _fields = new Field[rows][];
            for (var i = 0; i < _fields.Length; i++)
            {
                _fields[i] = new Field[columns];
            }
            foreach (var t in _fields)
            {
                for (var j = 0; j < t.Length; j++)
                {
                    t[j] = new Field();
                }
            }
            SetMines();
        }

        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            int number = _random.Next(minValue, maxValue);
            return number;
        }

        private int ScanSurroundingFields(int row, int column)
        {
            var mines = 0;
            if ((row > 0) &&
                (column > 0) &&
                (_fields[row - 1][column - 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((row > 0) &&
                (_fields[row - 1][column].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((row > 0) &&
                (column < _columns - 1) &&
                (_fields[row - 1][column + 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((column > 0) &&
                (_fields[row][column - 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((column < _columns - 1) &&
                (_fields[row][column + 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((row < _rows - 1) &&
                (column > 0) &&
                (_fields[row + 1][column - 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((row < _rows - 1) &&
                (_fields[row + 1][column].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            if ((row < _rows - 1) &&
                (column < _columns - 1) &&
                (_fields[row + 1][column + 1].Status == Field.FieldStatus.IsAMine))
            {
                mines++;
            }
            return mines;
        }

        private void SetMines()
        {
            for (var i = 0; i < _minesCount; i++)
            {
                var row = GenerateRandomNumber(0, _rows);
                var column = GenerateRandomNumber(0, _columns);
                if (_fields[row][column].Status == Field.FieldStatus.IsAMine)
                {
                    i--;
                }
                else
                {
                    _fields[row][column].Status = Field.FieldStatus.IsAMine;
                }
            }
        }

        private bool CheckIfWin()
        {
            var openedFields = _fields.Sum(t => t.Count(t1 => t1.Status == Field.FieldStatus.Opened));
            return (openedFields + _minesCount) == (_rows*_columns);
        }

        public void PrintGameBoard()
        {
            Console.Write("    ");
            for (var i = 0; i < _columns; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < _columns; i++)
            {
                Console.Write("__");
            }
            Console.WriteLine();

            for (var i = 0; i < _rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < _columns; j++)
                {
                    var currentField = _fields[i][j];
                    if (currentField.Status == Field.FieldStatus.Opened)
                    {
                        Console.Write(_fields[i][j].Value);
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
            for (var i = 0; i < _columns; i++)
            {
                Console.Write("__");
            }
            Console.WriteLine();
        }

        public Status OpenField(int row, int column)
        {
            var field = _fields[row][column];
            Status status;

            switch (field.Status)
            {
                case Field.FieldStatus.IsAMine:
                    status = Status.SteppedOnAMine;
                    break;
                case Field.FieldStatus.Opened:
                    status = Status.AlreadyOpened;
                    break;
                default:
                    field.Value = ScanSurroundingFields(row, column);
                    field.Status = Field.FieldStatus.Opened;
                    status = CheckIfWin() ? Status.AllFieldsAreOpened : Status.SuccessfullyOpened;
                    break;
            }
            return status;
        }

        public void PrintAllFields()
        {
            Console.Write("    ");
            for (var i = 0; i < _columns; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();

            Console.Write("   _");
            for (var i = 0; i < _columns; i++)
            {
                Console.Write("__");
            }
            Console.WriteLine();

            for (var i = 0; i < _rows; i++)
            {
                Console.Write(i);
                Console.Write(" | ");
                for (var j = 0; j < _columns; j++)
                {
                    var currentField = _fields[i][j];
                    switch (currentField.Status)
                    {
                        case Field.FieldStatus.Opened:
                            Console.Write(_fields[i][j].Value + " ");
                            break;
                        case Field.FieldStatus.IsAMine:
                            Console.Write("* ");
                            break;
                        default:
                            currentField.Value = ScanSurroundingFields(i, j);
                            Console.Write(_fields[i][j].Value + " ");
                            break;
                    }
                }
                Console.WriteLine("|");
            }

            Console.Write("   _");
            for (var i = 0; i < _columns; i++)
            {
                Console.Write("__");
            }
            Console.WriteLine();
        }

        public int CountOpenedFields()
        {
            return _fields.Sum(t => t.Count(t1 => t1.Status == Field.FieldStatus.Opened));
        }
    }
}