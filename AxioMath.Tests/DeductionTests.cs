
using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
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
    [Fact]
    public void ConjunctionIntroduction_Should_Derive_And_From_Two_Premises()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var p = lang.CreateFormula("p");
        var q = lang.CreateFormula("q");

        var system = new FormalSystem(lang, new[] { p, q }, new[] { new ConjunctionIntroductionRule() });
        var theory = new FormalTheory(system);

        var expected = lang.CreateFormula("(p ∧ q)");

        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(expected));
        Assert.NotNull(theorem);
        Assert.IsType<ConjunctionIntroductionRule>(theorem!.Rule);
    }

    [Fact]
    public void ConjunctionIntroduction_Should_Derive_Conjunction_From_P_And_Q()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var p = lang.CreateFormula("p");
        var q = lang.CreateFormula("q");

        var system = new FormalSystem(lang, new[] { p, q }, new[] { new ConjunctionIntroductionRule() });
        var theory = new FormalTheory(system);

        var pq = lang.CreateFormula("(p ∧ q)");
        Assert.Contains(theory.Theorems, t => t.Formula.Equals(pq));
    }

    [Theory]
    [InlineData("(p ∧ q)", "p", "q")]
    [InlineData("(a ∧ b)", "a", "b")]
    public void ConjunctionElimination_ProducesExpectedResults(string input, string expected1, string expected2)
    {
        var language = PropositionalLanguageBuilder.Build(new[] { "p", "q", "a", "b" });
        var formula = language.CreateFormula(input);
        var rule = new ConjunctionEliminationRule();

        var results = rule.Apply(new[] { formula }, language).ToList();
        var derived = results.Select(r => r.conclusion.ToString()).ToList();

        Assert.Contains(expected1, derived);
        Assert.Contains(expected2, derived);
        Assert.Equal(2, derived.Count);
    }

    [Fact]
    public void HypotheticalSyllogism_Should_Derive_P_Imp_R()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var pImpQ = lang.CreateFormula("(p → q)");
        var qImpR = lang.CreateFormula("(q → r)");

        var system = new FormalSystem(lang, new[] { pImpQ, qImpR }, new[] { new HypotheticalSyllogismRule() });
        var theory = new FormalTheory(system);

        var pImpR = lang.CreateFormula("(p → r)");

        var theorem = theory.Theorems.FirstOrDefault(t => t.Formula.Equals(pImpR));
        Assert.NotNull(theorem);
        Assert.IsType<HypotheticalSyllogismRule>(theorem!.Rule);
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(pImpQ));
        Assert.Contains(theorem.Premises, th => th.Formula.Equals(qImpR));
    }


}
