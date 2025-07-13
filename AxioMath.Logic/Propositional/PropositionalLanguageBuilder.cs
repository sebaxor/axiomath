using AxioMath.Core.Syntax;
namespace AxioMath.Logic.Propositional;

public static class PropositionalLanguageBuilder
{
    public static FormalLanguage Build(IEnumerable<string>? atomNames = null)
    {
        var atoms = atomNames ?? new[] { "p", "q", "r", "s" };
        var terminalSymbols = atoms.Select(a => new Symbol(a, true)).ToList();
        var not = new Symbol(OperatorSymbols.Not, true);
        var and = new Symbol(OperatorSymbols.And, true);
        var or = new Symbol(OperatorSymbols.Or, true);
        var implies = new Symbol(OperatorSymbols.Implies, true);
        var iff = new Symbol(OperatorSymbols.Iff, true);
        var lparen = new Symbol("(", true);
        var rparen = new Symbol(")", true);

        var atom = new Symbol("Atom", false);
        var formula = new Symbol("Formula", false);

        var grammar = new Grammar(formula);
        foreach (var t in terminalSymbols)
            grammar.AddRule(atom, new[] { t }, RuleInterpretation.Atom);
        grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);
        grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, not.Value);
        grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, and.Value);
        grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, or.Value);
        grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, implies.Value);
        grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, iff.Value);

        return new FormalLanguage(grammar);
    }
}
