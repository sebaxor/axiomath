using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Core
{

    public interface INumber : IExpression
    {
        int SetLevel { get; }
        decimal RealPart { get; }
        decimal? ImaginaryPart { get; }
    }


    public class NaturalNumber : INumber
    {
        public NaturalNumber(UInt64 value)
        {
            this.Value = value;

        }
        public UInt64 Value { get; set; }

        public int SetLevel => 0;

        public decimal RealPart => Value;

        public decimal? ImaginaryPart => null;

        public string? Evaluate()
        {
            return this.ToString();
        }

        public override string ToString() { return Value.ToString(); }
    }

    public class IntegerNumber : INumber
    {
        public IntegerNumber(NaturalNumber value)
        {
            this.Value = (long)value.Value;

        }
        public IntegerNumber(long value)
        {
            this.Value = value;

        }
        public long Value { get; set; }

        public int SetLevel => 1;

        public decimal RealPart => Value;

        public decimal? ImaginaryPart => null;

        public string? Evaluate()
        {
            return this.ToString();
        }
        public override string ToString() { return Value.ToString(); }
    }

    public class RationalNumber : INumber
    {

        
        public RationalNumber(NaturalNumber value)
        {
            this.Numerator = value.Value;
            this.Denominator = 1;

        }
        public RationalNumber(IntegerNumber value)
        {
            this.Numerator = value.Value;
            this.Denominator = 1;
        }
        public RationalNumber(decimal numerator, decimal denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;

        }
        public decimal Numerator { get; set; }

        public decimal Denominator { get; set; }

        public decimal Value => Numerator / Denominator;
        public decimal RealPart => Value;

        public decimal? ImaginaryPart => null;

        public int SetLevel => 2;

        public string? Evaluate()
        {
            return this.ToString();
        }
        public override string ToString() { return $"{Numerator}/{Denominator}"; }
    }

    public class RealNumber : INumber
    {

        public RealNumber(NaturalNumber value)
        {
            this.Value = value.Value;

        }
        public RealNumber(IntegerNumber value)
        {
            this.Value = value.Value;

        }
        public RealNumber(RationalNumber value)
        {
            this.Value = value.Value;

        }
        public RealNumber(decimal value)
        {
            this.Value = value;

        }
        public decimal Value { get; set; }

        public int SetLevel => 3;
        public decimal RealPart => Value;

        public decimal? ImaginaryPart => null;

        public string? Evaluate()
        {
            return this.ToString();
        }
        public override string ToString() { return Value.ToString(); }
    }

   


}
