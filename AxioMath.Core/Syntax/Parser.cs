

using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;


namespace AxioMath.Core.Parsers;

public class GrammarParser
{
    private readonly Grammar _grammar;

    public GrammarParser(Grammar grammar)
    {
        _grammar = grammar;
    }

    public FormulaNode Parse(string input)
    {
        var tokens = _grammar.Tokenize(input);
        var (success, node, pos) = ParseSymbol(_grammar.StartSymbol, tokens, 0);
        if (!success || pos != tokens.Length)
            throw new ArgumentException("Not a well-formed formula");

        return node!;
    }

    private (bool success, FormulaNode? node, int nextPos) ParseSymbol(Symbol symbol, string[] tokens, int position)
    {
        if (symbol.IsTerminal)
        {
            if (position < tokens.Length && tokens[position] == symbol.Value)
                return (true, new AtomNode(symbol.Value), position + 1);
            return (false, null, position);
        }

        foreach (var rule in _grammar.Rules.Where(r => r.Left.Equals(symbol)))
        {
            var currentPos = position;
            var children = new List<FormulaNode>();

            var success = true;
            foreach (var part in rule.Right)
            {
                var (matched, node, nextPos) = ParseSymbol(part, tokens, currentPos);
                if (!matched)
                {
                    success = false;
                    break;
                }
                children.Add(node!);
                currentPos = nextPos;
            }

            if (success)
            {
                var node = BuildAst(rule, children);
                return (true, node, currentPos);
            }
        }

        return (false, null, position);
    }

    private FormulaNode BuildAst(ProductionRule rule, List<FormulaNode> children)
    {
        return rule.Interpretation switch
        {
            RuleInterpretation.Atom => children[0],
            RuleInterpretation.Unary => new UnaryNode(rule.Operator!, children[1]),

            // El operador está en children[2], los operandos en 1 y 3
            RuleInterpretation.Binary => new BinaryNode(rule.Operator!, children[1], children[3]),

            _ => throw new NotSupportedException($"Unsupported rule interpretation: {rule.Interpretation}")
        };
    }




}
