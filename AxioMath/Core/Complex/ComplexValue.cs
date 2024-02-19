using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Expression.Implementation;

namespace AxioMath.Core.Complex
{

    public class ComplexValue : Expression<ComplexValue>
    {
        public ComplexValue(ComplexValue complexValue)
        {
            VariableName = complexValue.VariableName;
            RealPart = complexValue.RealPart;
            ImaginaryPart = complexValue.ImaginaryPart;
        }
        public ComplexValue(string variableName)
        {
            VariableName = variableName;
        }
        public ComplexValue(double realPart)
        {
            RealPart = realPart;
            ImaginaryPart = 0;
        }
        public ComplexValue(double realPart, double imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }
        public bool IsVariable() => VariableName != null;

        public double? RealPart { get; set; }
        public double? ImaginaryPart { get; set; }
        public string? VariableName { get; set; }

        public static ComplexValue operator +(ComplexValue complex1, ComplexValue complex2)
        {
            if (complex1?.RealPart == null || complex2?.RealPart == null || complex1?.ImaginaryPart == null || complex2?.ImaginaryPart == null)
            {
                return null;
            }

            double real = complex1.RealPart.Value + complex2.RealPart.Value;
            double imaginary = complex1.ImaginaryPart.Value + complex2.ImaginaryPart.Value;

            return new ComplexValue(real, imaginary);
        }

        public static ComplexValue operator -(ComplexValue complex1, ComplexValue complex2)
        {
            if (complex1?.RealPart == null || complex2?.RealPart == null || complex1?.ImaginaryPart == null || complex2?.ImaginaryPart == null)
            {
                return null;
            }

            double real = complex1.RealPart.Value - complex2.RealPart.Value;
            double imaginary = complex1.ImaginaryPart.Value - complex2.ImaginaryPart.Value;

            return new ComplexValue(real, imaginary);
        }

        public static ComplexValue operator *(ComplexValue complex1, ComplexValue complex2)
        {
            if (complex1?.RealPart == null || complex2?.RealPart == null || complex1?.ImaginaryPart == null || complex2?.ImaginaryPart == null)
            {
                return null;
            }

            double real = complex1.RealPart.Value * complex2.RealPart.Value - complex1.ImaginaryPart.Value * complex2.ImaginaryPart.Value;
            double imaginary = complex1.RealPart.Value * complex2.ImaginaryPart.Value + complex1.ImaginaryPart.Value * complex2.RealPart.Value;

            return new ComplexValue(real, imaginary);
        }

        public static ComplexValue operator /(ComplexValue complex1, ComplexValue complex2)
        {
            if (complex1?.RealPart == null || complex2?.RealPart == null || complex1?.ImaginaryPart == null || complex2?.ImaginaryPart == null)
            {
                return null;
            }

            double divisor = complex2.RealPart.Value * complex2.RealPart.Value + complex2.ImaginaryPart.Value * complex2.ImaginaryPart.Value;
            double real = (complex1.RealPart.Value * complex2.RealPart.Value + complex1.ImaginaryPart.Value * complex2.ImaginaryPart.Value) / divisor;
            double imaginary = (complex1.ImaginaryPart.Value * complex2.RealPart.Value - complex1.RealPart.Value * complex2.ImaginaryPart.Value) / divisor;

            return new ComplexValue(real, imaginary);
        }


        public override string ToString() => VariableName ?? (ImaginaryPart == 0 && RealPart != null ? RealPart.Value.ToString() : $"{RealPart} + {ImaginaryPart}i");

        public override ComplexValue Accept(IAlgebra<ComplexValue> algebra)
        {
            return algebra.Number(this);
        }
    }
}


