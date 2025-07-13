
using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class ConjunctionIntroductionTests
{

    #region ✅ Conjunction Introduction

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

    #endregion
}