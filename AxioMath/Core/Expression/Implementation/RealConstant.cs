using AxioMath.Core.Expression.Definition;
using AxioMath.Core.Operations;

namespace AxioMath.Core.Expression.Implementation
{
    public class RealConstant<T> : IExpression, IConstant where T : IMathNumber
    {


        public RealConstant(string name, T value)
        {
            Name = name;
            Value = value;
        }
        public string Name { get; set; }
        public T Value { get; set; }

        public int? SetLevel => Value.SetLevel;

        IMathNumber IConstant.Value => Value;

        public string? Evaluate()
        {
            return Value?.ToString();
        }

        public override string ToString()
        {
            return Name;
        }

    }

}
