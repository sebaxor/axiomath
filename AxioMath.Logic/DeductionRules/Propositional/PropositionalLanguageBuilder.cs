using AxioMath.Core.Syntax;
using AxioMath.Logic.DeductionRules.Propositional;

public static class PropositionalLanguageBuilder
{
    public static FormalLanguage Build()
    {
        var not = new Symbol(OperatorSymbols.Not, true);
        var and = new Symbol(OperatorSymbols.And, true);
        var or = new Symbol(OperatorSymbols.Or, true);
        var implies = new Symbol(OperatorSymbols.Implies, true);
        var iff = new Symbol(OperatorSymbols.Iff, true);
        var lparen = new Symbol("(", true);
        var rparen = new Symbol(")", true);

        var p = new Symbol("p", true);
        var q = new Symbol("q", true);
        var r = new Symbol("r", true);
        var s = new Symbol("s", true);

        var atom = new Symbol("Atom", false);
        var formula = new Symbol("Formula", false);

        var grammar = new Grammar(formula);

        grammar.AddRule(atom, new[] { p }, RuleInterpretation.Atom);
        grammar.AddRule(atom, new[] { q }, RuleInterpretation.Atom);
        grammar.AddRule(atom, new[] { r }, RuleInterpretation.Atom);
        grammar.AddRule(atom, new[] { s }, RuleInterpretation.Atom);

        grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);
        grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, OperatorSymbols.Not);
        grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, OperatorSymbols.And);
        grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, OperatorSymbols.Or);
        grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, OperatorSymbols.Implies);
        grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, OperatorSymbols.Iff);

        return new FormalLanguage(grammar);
    }
}
