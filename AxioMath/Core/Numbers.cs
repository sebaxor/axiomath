using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Core
{

    public interface INumber
    {
        int SetLevel { get; }
        double RealPart { get; }
        double? ImaginaryPart { get; }
    }

  



    public class NaturalNumber : INumber
    {
        public NaturalNumber(UInt64 value)
        {
            this.Value = value;

        }
        public UInt64 Value { get; set; }

        public int SetLevel => 0;

        public double RealPart => Value;

        public double? ImaginaryPart => null;

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

        public double RealPart => Value;

        public double? ImaginaryPart => null;

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
        public RationalNumber(long numerator, long denominator)
        {
            this.Numerator = numerator;
            this.Denominator = denominator;

        }
        public double Numerator { get; set; }

        public double Denominator { get; set; }

        public double Value => Numerator / Denominator;
        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public int SetLevel => 2;

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
        public RealNumber(double value)
        {
            this.Value = value;

        }
        public double Value { get; set; }

        public int SetLevel => 3;
        public double RealPart => Value;

        public double? ImaginaryPart => null;

        public override string ToString() { return Value.ToString(); }
    }

    public class ComplexNumber : INumber
    {
        public ComplexNumber(RealNumber realPart, RealNumber imaginaryPart)
        {
            this.RealPart = realPart;
            this.ImaginaryPart = imaginaryPart;
        }

        public RealNumber RealPart { get; set; }
        public RealNumber ImaginaryPart { get; set; }

        public int SetLevel => 4;

        double INumber.RealPart => RealPart.RealPart;

        double? INumber.ImaginaryPart => throw new NotImplementedException();

        public override string ToString() { return $"{RealPart} + {ImaginaryPart}i"; }
    }


}
