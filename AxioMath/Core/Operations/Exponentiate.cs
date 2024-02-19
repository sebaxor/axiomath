using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public class Exponentiate : IExpression
    {
        public Exponentiate(IExpression baseExpression, IExpression exponent)
        {
            Base = baseExpression;
            Exponent = exponent;
        }
        public IExpression Base { get; set; }
        public IExpression Exponent { get; set; }

        public string? Evaluate()
        {
            return $"({Base.Evaluate()})^({Exponent.Evaluate()})";
        }

        public override string ToString()
        {
            return $"({Base})^({Exponent})";
        }


    }

}
