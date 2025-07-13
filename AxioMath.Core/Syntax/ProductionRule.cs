
namespace AxioMath.Core.Syntax;

public enum RuleInterpretation
{
    Atom,
    Unary,
    Binary,
    Custom  // para casos extendidos
}
public class ProductionRule
{


    public Symbol Left { get; }
    public IReadOnlyList<Symbol> Right { get; }
    public RuleInterpretation? Interpretation { get; }
    public string? Operator { get; }       // e.g. "¬", "→"

    public ProductionRule(Symbol left, IEnumerable<Symbol> right, RuleInterpretation? interpretation = null, string? @operator = null)
    {
        if (left.IsTerminal)
            throw new ArgumentException("Left-hand side must be a non-terminal symbol.");

        Left = left;
        Right = right.ToList();
        Interpretation = interpretation;
        Operator = @operator;
    }

    public override string ToString() => $"{Left} → {string.Join(" ", Right)}";
}
