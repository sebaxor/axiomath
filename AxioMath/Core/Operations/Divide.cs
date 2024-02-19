using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public class Divide : IExpression
    {
        public Divide(IExpression numerator, IExpression denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public IExpression Numerator { get; set; }
        public IExpression Denominator { get; set; }

        public string? Evaluate()
        {
            return $"({Numerator.Evaluate()})/({Denominator.Evaluate()})";
        }

        public override string ToString()
        {
            return $"({Numerator})/({Denominator})";
        }


    }

}
