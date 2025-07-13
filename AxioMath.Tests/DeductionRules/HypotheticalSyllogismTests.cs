using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class HypotheticalSyllogismTests
{

    #region ✅ Hypothetical Syllogism

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

    #endregion
}
