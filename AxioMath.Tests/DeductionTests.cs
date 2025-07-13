
using AxioMath.Core.Formulas;
using AxioMath.Core.Syntax;
using AxioMath.Logic.DeductionRules.Propositional;


namespace AxioMath.Tests;

public class DeductionTests
{
    private FormalLanguage BuildLanguage()
    {
        var not = new Symbol("¬", true);
        var and = new Symbol("∧", true);
        var or = new Symbol("∨", true);
        var implies = new Symbol("→", true);
        var iff = new Symbol("↔", true);
        var lparen = new Symbol("(", true);
        var rparen = new Symbol(")", true);

        var p = new Symbol("p", true);
        var q = new Symbol("q", true);
        var r = new Symbol("r", true);

        var atom = new Symbol("Atom", false);
        var formula = new Symbol("Formula", false);

        var grammar = new Grammar(formula);
        grammar.AddRule(atom, new[] { p }, RuleInterpretation.Atom);
        grammar.AddRule(atom, new[] { q }, RuleInterpretation.Atom);
        grammar.AddRule(atom, new[] { r }, RuleInterpretation.Atom);

        grammar.AddRule(formula, new[] { atom }, RuleInterpretation.Atom);
        grammar.AddRule(formula, new[] { not, formula }, RuleInterpretation.Unary, "¬");
        grammar.AddRule(formula, new[] { lparen, formula, and, formula, rparen }, RuleInterpretation.Binary, "∧");
        grammar.AddRule(formula, new[] { lparen, formula, or, formula, rparen }, RuleInterpretation.Binary, "∨");
        grammar.AddRule(formula, new[] { lparen, formula, implies, formula, rparen }, RuleInterpretation.Binary, "→");
        grammar.AddRule(formula, new[] { lparen, formula, iff, formula, rparen }, RuleInterpretation.Binary, "↔");

        return new FormalLanguage(grammar);
    }

    [Fact]
    public void Formula_Should_Be_Recognized_As_Valid()
    {
        var lang = BuildLanguage();
        var valid = lang.BelongsToLanguage("(p → q)");
        Assert.True(valid);
    }

    [Fact]
    public void Formula_Should_Parse_Correctly()
    {
        var lang = BuildLanguage();
        var formula = lang.CreateFormula("(p → q)");
        Assert.NotNull(formula);
        Assert.Equal("(p → q)", formula.Content);
    }

    [Fact]
    public void ModusPonens_Should_Derive_Q_From_P_And_Implication()
    {
        var lang = BuildLanguage();
        var p = lang.CreateFormula("p");
        var imp = lang.CreateFormula("(p → q)");
        var system = new FormalSystem(lang, new[] { p, imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = lang.CreateFormula("q");
        Assert.Contains(q, theory.Theorems);
    }

    [Fact]
    public void ModusPonens_Should_Not_Derive_Q_Without_P()
    {
        var lang = BuildLanguage();
        var imp = lang.CreateFormula("(p → q)");
        var system = new FormalSystem(lang, new[] { imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = lang.CreateFormula("q");
        Assert.DoesNotContain(q, theory.Theorems);
    }

    [Fact]
    public void Grammar_Should_Generate_Some_Valid_Formulas()
    {
        var lang = BuildLanguage();
        var formulas = lang.Grammar.Generate(3).ToList();
        Assert.NotEmpty(formulas);
        Assert.All(formulas, f => Assert.True(lang.BelongsToLanguage(f)));
    }
}
