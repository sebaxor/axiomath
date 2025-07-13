// Core/Extensions/FormulaExtensions.cs
using AxioMath.Logic.Propositional;

namespace AxioMath.Core.Formulas;

public static class FormulaExtensions
{
    public static bool IsImplication(this Formula formula)
        => formula.Root is BinaryNode b && b.Operator == OperatorSymbols.Implies;

    public static FormulaNode? ImplicationAntecedent(this Formula formula)
        => formula.IsImplication() ? ((BinaryNode)formula.Root).Left : null;

    public static FormulaNode? ImplicationConsequent(this Formula formula)
        => formula.IsImplication() ? ((BinaryNode)formula.Root).Right : null;

    public static bool StructurallyEquals(this FormulaNode a, FormulaNode b)
    {
        return (a, b) switch
        {
            (AtomNode aa, AtomNode bb) => aa.Name == bb.Name,
            (UnaryNode ua, UnaryNode ub) => ua.Operator == ub.Operator && StructurallyEquals(ua.Operand, ub.Operand),
            (BinaryNode ba, BinaryNode bb) => ba.Operator == bb.Operator &&
                                              StructurallyEquals(ba.Left, bb.Left) &&
                                              StructurallyEquals(ba.Right, bb.Right),
            _ => false
        };
    }


}
