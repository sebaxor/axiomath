using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public class Equals : IExpression
    {
        public Equals(params IExpression[] expressions)
        {
            Expressions = new List<IExpression>(expressions);
        }
        public List<IExpression> Expressions { get; set; }

        public string? Evaluate()
        {
            return string.Join("=", Expressions.Select(x => $"({x.Evaluate()})"));
        }

        public override string ToString()
        {
            return string.Join("=", Expressions.Select(x => $"({x.ToString()})"));
        }


    }

}
