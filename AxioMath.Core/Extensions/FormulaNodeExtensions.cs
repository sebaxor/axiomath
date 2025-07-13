

using AxioMath.Core.Formulas;

public static class FormulNodeExtensions
{
    public static bool IsDisjunction(this Formula f) =>
        f.Root is BinaryNode b && b.Operator == "∨";

    public static FormulaNode? DisjunctionLeft(this Formula f) =>
        (f.Root as BinaryNode)?.Left;

    public static FormulaNode? DisjunctionRight(this Formula f) =>
        (f.Root as BinaryNode)?.Right;

    public static bool IsNegationOf(this FormulaNode node, FormulaNode target) =>
        node is UnaryNode u && u.Operator == "¬" && u.Operand.StructurallyEquals(target);
}