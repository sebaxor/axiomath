using AxioMath.Core.Expression.Definition;

namespace AxioMath.Core.Operations
{
    public interface IConstant : IExpression
    {
        IMathNumber Value { get; }
    }

}
