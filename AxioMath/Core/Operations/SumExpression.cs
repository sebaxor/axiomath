using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Expression.Implementation;

namespace AxioMath.Core.Operations
{
    // Clase concreta para representar expresiones de suma
    public class SumExpression<T> : Expression<T>
    {
        public Expression<T> Left { get; }
        public Expression<T> Right { get; }

        public SumExpression(Expression<T> left, Expression<T> right)
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
}


