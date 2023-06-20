using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxioMath.Core
{
    // Definición de la interfaz Algebra
    public interface IAlgebra<T>
    {
        T Number(T value);
        T Add(T left, T right);
        T Multiply(T left, T right);
    }

    // Clase base abstracta para las expresiones
    public abstract class Expression<T>
    {
        public abstract T Accept(IAlgebra<T> algebra);
    }
     

    // Clase concreta para representar expresiones de suma
    public class AdditionExpression<T> : Expression<T>
    {
        public Expression<T> Left { get; }
        public Expression<T> Right { get; }

        public AdditionExpression(Expression<T> left, Expression<T> right)
        {
            Left = left;
            Right = right;
        }

        public override T Accept(IAlgebra<T> algebra)
        {
            return algebra.Add(Left.Accept(algebra), Right.Accept(algebra));
        }
        public override string ToString() => $"({Left} + {Right})";
    }

    public class MultiplicationExpression<T> : Expression<T>
    {
        public Expression<T> Left { get; }
        public Expression<T> Right { get; }

        public MultiplicationExpression(Expression<T> left, Expression<T> right)
        {
            Left = left;
            Right = right;
        }

        public override T Accept(IAlgebra<T> algebra)
        {
            return algebra.Multiply(Left.Accept(algebra), Right.Accept(algebra));
        }
        public override string ToString() => $"({Left} * {Right})";
    }

    // Ejemplo de uso


    // Implementación personalizada de IAlgebra para enteros


    public class ComplexAlgebra : IAlgebra<ComplexValue>
    {
        public ComplexValue Add(ComplexValue left, ComplexValue right)
        {
            return left + right;
        }

        public ComplexValue Multiply(ComplexValue left, ComplexValue right)
        {
            return left * right;
        }

        public ComplexValue Number(ComplexValue value)
        {
            return value;
        }
    }

    public class ComplexExpressionsAlgebra : IAlgebra<Expression<ComplexValue>>
    {
        private readonly IAlgebra<ComplexValue> complexAlgebra = new ComplexAlgebra();
        public Expression<ComplexValue> Add(Expression<ComplexValue> left, Expression<ComplexValue> right)
        {
            var leftValue = left.Accept(complexAlgebra);
            var rightValue = right.Accept(complexAlgebra);

            if (leftValue != null && !leftValue.IsVariable() && rightValue != null && !rightValue.IsVariable())
                return new ComplexValue(complexAlgebra.Add(leftValue, rightValue));
            else
                return new AdditionExpression<ComplexValue>(left, right);
        }

        public Expression<ComplexValue> Multiply(Expression<ComplexValue> left, Expression<ComplexValue> right)
        {
            var leftValue = left.Accept(complexAlgebra);
            var rightValue = right.Accept(complexAlgebra);

            if (leftValue != null && !leftValue.IsVariable() && rightValue != null && !rightValue.IsVariable())
                return new ComplexValue(complexAlgebra.Multiply(leftValue, rightValue));
            else
                return new MultiplicationExpression<ComplexValue>(left, right);
        }

        public Expression<ComplexValue> Number(Expression<ComplexValue> value)
        {
            return value;
        }
    }




    public class ComplexValue :Expression<ComplexValue>
    {
        public ComplexValue(ComplexValue complexValue)
        {
            this.VariableName = complexValue.VariableName;
            this.RealPart = complexValue.RealPart;
            this.ImaginaryPart = complexValue.ImaginaryPart;
        }
        public ComplexValue(string variableName)
        {
            this.VariableName = variableName;
        }
        public ComplexValue(double realPart)
        {
            this.RealPart = realPart;
            this.ImaginaryPart = 0;
        }
        public ComplexValue(double realPart, double imaginaryPart)
        {
            this.RealPart = realPart;
            this.ImaginaryPart = imaginaryPart;
        }
        public bool IsVariable() => this.VariableName != null;

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

            double real = (complex1.RealPart.Value * complex2.RealPart.Value) - (complex1.ImaginaryPart.Value * complex2.ImaginaryPart.Value);
            double imaginary = (complex1.RealPart.Value * complex2.ImaginaryPart.Value) + (complex1.ImaginaryPart.Value * complex2.RealPart.Value);

            return new ComplexValue(real, imaginary);
        }

        public static ComplexValue operator /(ComplexValue complex1, ComplexValue complex2)
        {
            if (complex1?.RealPart == null || complex2?.RealPart == null || complex1?.ImaginaryPart == null || complex2?.ImaginaryPart == null)
            {
                return null;
            }

            double divisor = (complex2.RealPart.Value * complex2.RealPart.Value) + (complex2.ImaginaryPart.Value * complex2.ImaginaryPart.Value);
            double real = ((complex1.RealPart.Value * complex2.RealPart.Value) + (complex1.ImaginaryPart.Value * complex2.ImaginaryPart.Value)) / divisor;
            double imaginary = ((complex1.ImaginaryPart.Value * complex2.RealPart.Value) - (complex1.RealPart.Value * complex2.ImaginaryPart.Value)) / divisor;

            return new ComplexValue(real, imaginary);
        }


        public override string ToString() => VariableName ?? (ImaginaryPart == 0 && RealPart != null ? RealPart.Value.ToString() : $"{RealPart} + {ImaginaryPart}i");

        public override ComplexValue Accept(IAlgebra<ComplexValue> algebra)
        {
            return algebra.Number(this);
        }
    }
}


