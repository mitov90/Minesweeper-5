namespace Minesweeper.Data
{
    using System;

    using Minesweeper.Enums;

    public class Field : FieldPrototype
    {
        private int value;

        public Field(int value, FieldStatus status)
        {
            this.Value = value;
            this.Status = status;
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

        public override Field Clone()
        {
            return this.MemberwiseClone() as Field;
        }
    }
}
