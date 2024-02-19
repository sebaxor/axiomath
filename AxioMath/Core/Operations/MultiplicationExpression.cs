using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Expression.Implementation;

namespace AxioMath.Core.Operations
{
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
}


