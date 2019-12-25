using System;

namespace SimpleValidation.Validation.Validator
{
    public class NumberGuard : Guard
    {
        public enum Comparator { EQUAL , SMALLER , BIGGER }

        private Comparator comparator;
        private double conditionalNumber;

        public NumberGuard(string id, double conditionalNumber) : base(id)
        {
            this.comparator = Comparator.EQUAL;
            this.conditionalNumber = conditionalNumber;
        }

        public NumberGuard(string id, double conditionalNumber, Comparator comparator) : base(id)
        {
            this.comparator = comparator;
            this.conditionalNumber = conditionalNumber;
        }

        protected override bool Validate(object value)
        {
            double number;
            try
            {
                number = double.Parse(value.ToString());
            }
            catch
            {
                return false;
            }

            return Compare(number);
        }

        private bool Compare(double number)
        {
            switch (comparator)
            {
                case Comparator.EQUAL:
                    return number == conditionalNumber;
                case Comparator.SMALLER:
                    return number < conditionalNumber;
                case Comparator.BIGGER:
                    return number > conditionalNumber;
                // case ...
            }
            return false;
        }


    }

}
