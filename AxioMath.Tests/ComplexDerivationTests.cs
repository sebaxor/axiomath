// Tests/ComplexDerivationTests.cs
using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class ComplexDerivationTests
{
    [Fact]
    public void Should_Derive_P_Imp_S_Through_Chained_HypotheticalSyllogism()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var pImpQ = lang.CreateFormula("(p → q)");
        var qImpR = lang.CreateFormula("(q → r)");
        var rImpS = lang.CreateFormula("(r → s)");

        var axioms = new[] { pImpQ, qImpR, rImpS };
        var rules = new IDeductionRule[] { new HypotheticalSyllogismRule() };

        var system = new FormalSystem(lang, axioms, rules);
        var theory = new FormalTheory(system);

        var pImpS = lang.CreateFormula("(p → s)");

        Assert.Contains(theory.Theorems, t => t.Formula.Equals(pImpS));
    }

    [Fact]
    public void Should_Apply_Conjunction_Then_ModusPonens()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var p = lang.CreateFormula("p");
        var q = lang.CreateFormula("q");
        var conj = lang.CreateFormula("(p ∧ q)");
        var conjImpliesR = lang.CreateFormula("((p ∧ q) → r)");

        var axioms = new[] { p, q, conjImpliesR };
        var rules = new IDeductionRule[]
        {
            new ConjunctionIntroductionRule(),
            new ModusPonensRule()
        };

        var system = new FormalSystem(lang, axioms, rules);
        var theory = new FormalTheory(system);

        var r = lang.CreateFormula("r");
        Assert.Contains(theory.Theorems, t => t.Formula.Equals(r));
    }
}
