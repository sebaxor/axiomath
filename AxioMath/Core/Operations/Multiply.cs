using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public class Multiply : IExpression
    {

        public Multiply(params IExpression[] factors)
        {
            Factor1 = factors[0];
            var remaining = factors.Skip(1).ToArray();
            Factor2 = remaining.Length == 1 ? remaining[0] : new Multiply(remaining);
        }
        public Multiply(IExpression factor1, IExpression factor2)
        {
            Factor1 = factor1;
            Factor2 = factor2;
        }
        public IExpression Factor1 { get; set; }
        public IExpression Factor2 { get; set; }

        public string? Evaluate()
        {
            return $"({Factor1.Evaluate()})*({Factor2.Evaluate()})";
        }

        public override string ToString()
        {
            return $"({Factor1})*({Factor2})";
        }




    }

}
