

using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class ConjunctionEliminationTests
{

    #region ✅ Conjunction Elimination

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

    #endregion
}