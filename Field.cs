namespace Minesweeper
{
    using System;

    public class Field
    {
        private int value;

        public Field()
        {
            this.value = 0;
            this.Status = FieldStatus.Closed;
        }

        public int Value
        {
            get 
            { 
                return this.value;
            }

            set 
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid field value. It cannot be negative number.");
                }

                this.value = value; 
            }
        }

        public FieldStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Field))
            {
                return false;
            }

            var field = (Field)obj;
            return object.Equals(this.Status, field.Status) 
                   && object.Equals(this.Value, field.Value);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
