using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Expression.Implementation;
using AxioMath.Core.Operations;

namespace AxioMath.Core.Complex
{
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
                return new SumExpression<ComplexValue>(left, right);
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
}


