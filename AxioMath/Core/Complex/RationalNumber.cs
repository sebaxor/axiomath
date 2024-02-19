using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Complex
{
    public class RationalNumber : IMathNumber
    {
        public RationalNumber(NaturalNumber value)
        {
            Numerator = value.Value;
            Denominator = 1;

        }
        public RationalNumber(IntegerNumber value)
        {
            Numerator = value.Value;
            Denominator = 1;
        }
        public RationalNumber(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;

        }
        public double Numerator { get; set; }

        public double Denominator { get; set; }

        public double Value => Numerator / Denominator;
        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public int SetLevel => 2;

        public override string ToString() { return $"{Numerator}/{Denominator}"; }
    }




}
