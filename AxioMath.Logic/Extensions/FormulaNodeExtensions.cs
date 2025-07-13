

using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;

public static class FormulNodeExtensions
{
    public static bool IsDisjunction(this Formula f) =>
        f.Root is BinaryNode b && b.Operator == OperatorSymbols.Or;

    public static FormulaNode? DisjunctionLeft(this Formula f) =>
        (f.Root as BinaryNode)?.Left;

    public static FormulaNode? DisjunctionRight(this Formula f) =>
        (f.Root as BinaryNode)?.Right;

    public static bool IsNegationOf(this FormulaNode node, FormulaNode target) =>
        node is UnaryNode u && u.Operator == OperatorSymbols.Not && u.Operand.StructurallyEquals(target);



    public static bool IsImplication(this FormulaNode node)
        => node is BinaryNode b && b.Operator == OperatorSymbols.Implies;

    public static (FormulaNode antecedent, FormulaNode consequent)? TryGetImplicationParts(this FormulaNode node)
    {
        if (node is BinaryNode b && b.Operator == OperatorSymbols.Implies)
            return (b.Left, b.Right);
        return null;
    }
}