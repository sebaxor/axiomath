using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class DisjunctionEliminationTests

{


    #region ✅ Disjunction Elimination

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
            new IDeductionRule[] {
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

    #endregion
}