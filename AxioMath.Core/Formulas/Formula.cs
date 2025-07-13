
namespace AxioMath.Core.Formulas;
public class Formula
{
    public string Content { get; }
    public FormulaNode Root { get; }

    public Formula(string content, FormulaNode root)
    {
        Content = content;
        Root = root;
    }

    public override string ToString() => Content;

    public override bool Equals(object? obj)
    {
        return obj is Formula other && FormulaNodeEquals(Root, other.Root);
    }

    public override int GetHashCode()
    {
        return GetNodeHashCode(Root);
    }

    private static bool FormulaNodeEquals(FormulaNode a, FormulaNode b)
    {
        if (a is AtomNode atomA && b is AtomNode atomB)
            return atomA.Name == atomB.Name;
        if (a is UnaryNode ua && b is UnaryNode ub)
            return ua.Operator == ub.Operator &&
                   FormulaNodeEquals(ua.Operand, ub.Operand);
        if (a is BinaryNode ba && b is BinaryNode bb)
            return ba.Operator == bb.Operator &&
                   FormulaNodeEquals(ba.Left, bb.Left) &&
                   FormulaNodeEquals(ba.Right, bb.Right);
        return false;
    }

    private static int GetNodeHashCode(FormulaNode node)
    {
        return node switch
        {
            AtomNode a => a.Name.GetHashCode(),
            UnaryNode u => HashCode.Combine(u.Operator, GetNodeHashCode(u.Operand)),
            BinaryNode b => HashCode.Combine(b.Operator, GetNodeHashCode(b.Left), GetNodeHashCode(b.Right)),
            _ => 0
        };
    }

}

