
using AxioMath.Core.Formulas;
using AxioMath.Logic.DeductionRules.Propositional;
using Xunit;



namespace AxioMath.Tests;

public class DeductionTests
{


    [Theory]
    [InlineData("p")]
    [InlineData("¬q")]
    [InlineData("(p → q)")]
    [InlineData("((p ∧ q) → r)")]
    public void Formula_Should_Be_Recognized_As_Valid(string expression)
    {
        var lang = PropositionalLanguageBuilder.Build();
        Assert.True(lang.BelongsToLanguage(expression));
    }


    [Theory]
    [InlineData("p")]
    [InlineData("¬q")]
    [InlineData("(p → q)")]
    public void Formula_Should_Parse_Correctly(string input)
    {
        var lang = PropositionalLanguageBuilder.Build();
        var formula = lang.CreateFormula(input);
        Assert.NotNull(formula);
        Assert.Equal(input, formula.Content);
    }


    [Fact]
    public void ModusPonens_Should_Derive_Q_From_P_And_Implication()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var p = lang.CreateFormula("p");
        var imp = lang.CreateFormula("(p → q)");
        var system = new FormalSystem(lang, new[] { p, imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = lang.CreateFormula("q");
        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(q));
        Assert.NotNull(theorem);
        Assert.IsType<ModusPonensRule>(theorem!.Rule);
        Assert.Equal(2, theorem.Premises.Count);
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(p));
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(imp));
    }

    [Fact]
    public void ModusPonens_Should_Not_Derive_Q_Without_P()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var imp = lang.CreateFormula("(p → q)");
        var system = new FormalSystem(lang, new[] { imp }, new[] { new ModusPonensRule() });
        var theory = new FormalTheory(system);

        var q = lang.CreateFormula("q");
        Assert.DoesNotContain(theory.Theorems, t => t.Formula.Equals(q));
    }

    [Fact]
    public void Grammar_Should_Generate_Some_Valid_Formulas()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var formulas = lang.Grammar.Generate(3).ToList();
        Assert.NotEmpty(formulas);
        Assert.All(formulas, f => Assert.True(lang.BelongsToLanguage(f)));
    }
    [Fact]
    public void ModusTollens_Should_Derive_NotP_From_Implication_And_NotQ()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var imp = lang.CreateFormula("(p → q)");
        var notQ = lang.CreateFormula("¬q");

        var system = new FormalSystem(lang, new[] { imp, notQ }, new[] { new ModusTollensRule() });
        var theory = new FormalTheory(system);

        var notP = lang.CreateFormula("¬p");

        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(notP));
        Assert.NotNull(theorem);
        Assert.IsType<ModusTollensRule>(theorem!.Rule);
    }
    [Fact]
    public void DisjunctionElimination_Should_Derive_Q()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var disj = lang.CreateFormula("(p ∨ r)");
        var pImpliesQ = lang.CreateFormula("(p → q)");
        var rImpliesQ = lang.CreateFormula("(r → q)");

        var system = new FormalSystem(
            lang,
            new[] { disj, pImpliesQ, rImpliesQ },
            new IDeductionRule[] { new DisjunctionEliminationRule() }
        );

        var theory = new FormalTheory(system);
        var q = lang.CreateFormula("q");

        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(q));
        Assert.NotNull(theorem);
        Assert.IsType<DisjunctionEliminationRule>(theorem!.Rule);
    }
    [Fact]
    public void Combined_DisjunctionElimination_And_ModusTollens()
    {
        var lang = PropositionalLanguageBuilder.Build();

        var disj = lang.CreateFormula("(p ∨ r)");
        var pImpliesS = lang.CreateFormula("(p → s)");
        var rImpliesS = lang.CreateFormula("(r → s)");
        var notS = lang.CreateFormula("¬s");

        var system = new FormalSystem(
            lang,
            new[] { disj, pImpliesS, rImpliesS, notS },
            new IDeductionRule[]
            {
            new DisjunctionEliminationRule(),
            new ModusTollensRule()
            }
        );

        var theory = new FormalTheory(system);

        var notP = lang.CreateFormula("¬p");
        var notR = lang.CreateFormula("¬r");

        Assert.Contains(theory.Theorems, t => t.Formula.Equals(notP));
        Assert.Contains(theory.Theorems, t => t.Formula.Equals(notR));
    }
    [Fact]
    public void DisjunctionElimination_Should_Not_Apply_Without_Implications()
    {
        var lang = PropositionalLanguageBuilder.Build();

        var disj = lang.CreateFormula("(p ∨ r)");

        var system = new FormalSystem(
            lang,
            new[] { disj },
            new IDeductionRule[] { new DisjunctionEliminationRule() }
        );

        var theory = new FormalTheory(system);
        var q = lang.CreateFormula("q");

        Assert.DoesNotContain(theory.Theorems, t => t.Formula.Equals(q));
    }



}
