using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class ModusTollensTests
{

    #region ✅ Modus Tollens

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

    #endregion
}