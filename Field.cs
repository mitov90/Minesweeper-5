namespace Minesweeper
{
    using System;

    public class Field
    {
        private int value;
        private FieldStatus status;

        public Field()
        {
            this.value = 0;
            this.status = FieldStatus.Closed;
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

        public FieldStatus Status
        {
            get 
            { 
                return this.status;
            }

            set 
            { 
                this.status = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Field))
            {
                return false;
            }
            Field field = (Field)obj;
            if (!Object.Equals(this.Status, field.Status))
            {
                return false;
            }

            if (!Object.Equals(this.Value, field.Value))
            {
                return false;
            }

            return true;
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
