namespace Minesweeper
{
    public class Field
    {
        private int value;
        private FieldStatus status;

        public Field()
        {
            this.value = 0;
            this.status = FieldStatus.Closed;
        }

        public enum FieldStatus
        {
            Closed, Opened, IsAMine
        }

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public FieldStatus Status
        {
            get { return this.status; }
            set { this.status = value; }
        }
    }
}
