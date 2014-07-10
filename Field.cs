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
    }
}
