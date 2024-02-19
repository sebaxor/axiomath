using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Complex
{
    public class IntegerNumber : IMathNumber
    {
        public IntegerNumber(NaturalNumber value)
        {
            Value = (long)value.Value;

        }
        public IntegerNumber(long value)
        {
            Value = value;

        }
        public long Value { get; set; }

        public int SetLevel => 1;

        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public override string ToString() { return Value.ToString(); }
    }




}
