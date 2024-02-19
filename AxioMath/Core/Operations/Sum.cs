using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public class Sum : IExpression
    {

        public Sum(params IExpression[] terms)
        {
            Term1 = terms[0];
            var remaining = terms.Skip(1).ToArray();
            Term2 = remaining.Length == 1 ? remaining[0] : new Sum(remaining);
        }
        public Sum(IExpression term1, IExpression term2)
        {
            Term1 = term1;
            Term2 = term2;
        }
        public IExpression Term1 { get; set; }
        public IExpression Term2 { get; set; }

        public string? Evaluate()
        {
            return $"({Term1.Evaluate()})+({Term2.Evaluate()})";
        }

        public override string ToString()
        {
            return $"({Term1})+({Term2})";
        }



    }

}
