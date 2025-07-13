// Core/Extensions/FormulaExtensions.cs
using AxioMath.Core;

namespace AxioMath.Core.Formulas;

public static class FormulaExtensions
{
    public static string ToInfixString(this FormulaNode node)
    {
        return node switch
        {
            AtomNode a => a.Name,
            UnaryNode u => $"{u.Operator}{ToInfixString(u.Operand)}",
            BinaryNode b => $"({ToInfixString(b.Left)} {b.Operator} {ToInfixString(b.Right)})",
            _ => throw new NotSupportedException()
        };
    }
}