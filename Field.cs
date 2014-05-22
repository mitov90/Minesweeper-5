namespace Minesweeper
{
    internal class Field
    {
        public enum FieldStatus
        {
            Closed,
            Opened,
            IsAMine
        }

        public Field()
        {
            Value = 0;
            Status = FieldStatus.Closed;
        }

        public int Value { get; set; }

        public FieldStatus Status { get; set; }
    }
}