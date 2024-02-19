using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Complex
{
    public class RealNumber : IMathNumber
    {

        public RealNumber(NaturalNumber value)
        {
            Value = value.Value;

        }
        public RealNumber(IntegerNumber value)
        {
            Value = value.Value;

        }
        public RealNumber(RationalNumber value)
        {
            Value = value.Value;

        }
        public RealNumber(double value)
        {
            Value = value;

        }
        public double Value { get; set; }

        public int SetLevel => 3;
        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public override string ToString() { return Value.ToString(); }
    }




}
