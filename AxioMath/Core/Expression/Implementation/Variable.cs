using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Expression.Implementation
{
    public class Variable<T> : IExpression, IVariable where T : IMathNumber
    {
        public Variable(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public string? Evaluate()
        {
            return Name;
        }

        public override string ToString()
        {
            return Name;
        }


    }

}
