using System.Text.RegularExpressions;
using AxioMath.Core.Syntax;


namespace AxioMath.Core.Syntax;

public class Grammar
{
    public HashSet<Symbol> NonTerminals { get; } = new();
    public HashSet<Symbol> Terminals { get; } = new();
    public List<ProductionRule> Rules { get; } = new();
    public Symbol StartSymbol { get; }

    public Grammar(Symbol startSymbol)
    {
        if (startSymbol.IsTerminal)
            throw new ArgumentException("Start symbol must be non-terminal.");

        StartSymbol = startSymbol;
        NonTerminals.Add(startSymbol);
    }

    public void AddRule(Symbol left, Symbol[] right, RuleInterpretation? interpretation = null, string? op = null)
    {
        var rule = new ProductionRule(left, right, interpretation, op);
        Rules.Add(rule);
        NonTerminals.Add(left);
        foreach (var s in right)
        {
            if (s.IsTerminal) Terminals.Add(s);
            else NonTerminals.Add(s);
        }
    }


    public virtual bool IsValid(string input)
    {
        var tokens = Tokenize(input);
        return TryDerive(tokens, StartSymbol, 0).Any(pos => pos == tokens.Length);
    }

    protected virtual IEnumerable<int> TryDerive(string[] tokens, Symbol current, int position)
    {
        if (current.IsTerminal)
        {
            if (position < tokens.Length && current.Value == tokens[position])
                yield return position + 1;
            yield break;
        }

        foreach (var rule in Rules.Where(r => r.Left.Equals(current)))
        {
            IEnumerable<int> positions = new[] { position };
            foreach (var symbol in rule.Right)
            {
                var nextPositions = new List<int>();
                foreach (var p in positions)
                    nextPositions.AddRange(TryDerive(tokens, symbol, p));
                positions = nextPositions;
                if (!positions.Any()) break;
            }
            foreach (var p in positions)
                yield return p;
        }
    }

    public virtual IEnumerable<string> Generate(int maxDepth = 3)
    {
        return GenerateFrom(StartSymbol, maxDepth)
            .Where(seq => seq.All(s => s.IsTerminal))
            .Select(seq => string.Join(" ", seq.Select(s => s.Value)));
    }

    protected virtual IEnumerable<List<Symbol>> GenerateFrom(Symbol symbol, int depth)
    {
        if (depth == 0)
            yield break;

        if (symbol.IsTerminal)
        {
            yield return new List<Symbol> { symbol };
            yield break;
        }

        foreach (var rule in Rules.Where(r => r.Left.Equals(symbol)))
        {
            var expansions = new List<List<Symbol>> { new() };
            foreach (var rhsSymbol in rule.Right)
            {
                var newExpansions = new List<List<Symbol>>();
                foreach (var partial in expansions)
                {
                    foreach (var part in GenerateFrom(rhsSymbol, depth - 1))
                    {
                        var merged = new List<Symbol>(partial);
                        merged.AddRange(part);
                        newExpansions.Add(merged);
                    }
                }
                expansions = newExpansions;
            }
            foreach (var exp in expansions)
                yield return exp;
        }
    }

    public virtual string[] Tokenize(string input)
    {
        var allTerminals = Terminals.Select(t => Regex.Escape(t.Value)).OrderByDescending(s => s.Length);
        var pattern = string.Join("|", allTerminals);
        var regex = new Regex($"({pattern})");

        var spaced = regex.Replace(input, " $1 ");
        return spaced.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
}
