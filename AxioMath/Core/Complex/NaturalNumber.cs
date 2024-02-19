using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Complex
{
    public class NaturalNumber : IMathNumber
    {
        public NaturalNumber(ulong value)
        {
            Value = value;

        }
        public ulong Value { get; set; }

        public int SetLevel => 0;

        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public override string ToString() { return Value.ToString(); }
    }




}
