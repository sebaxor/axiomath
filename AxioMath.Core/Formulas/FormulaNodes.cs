namespace AxioMath.Core.Formulas;

public abstract class FormulaNode
{
    private static string NodeToContent(FormulaNode node)
    {
        return node.ToString() ?? throw new InvalidOperationException("FormulaNode.ToString() returned null");
    }
}
public class AtomNode : FormulaNode
{
    public string Name { get; }
    public AtomNode(string name) => Name = name;
    public override string ToString() => Name;
}

public class UnaryNode : FormulaNode
{
    public string Operator { get; }
    public FormulaNode Operand { get; }
    public UnaryNode(string op, FormulaNode operand) => (Operator, Operand) = (op, operand);
    public override string ToString() => $"{Operator} {Operand}";
}

public class BinaryNode : FormulaNode
{
    public string Operator { get; }
    public FormulaNode Left { get; }
    public FormulaNode Right { get; }
    public BinaryNode(string op, FormulaNode left, FormulaNode right) =>
        (Operator, Left, Right) = (op, left, right);
    public override string ToString() => $"( {Left} {Operator} {Right} )";
}

