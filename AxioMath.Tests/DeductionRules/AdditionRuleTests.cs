using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class AdditionRuleTests
{
    [Theory]
    [InlineData("p", "(p ∨ Q)")]
    [InlineData("a", "(a ∨ Q)")]

    public void AdditionRule_ProducesExpectedDisjunction(string input, string expected)
    {
        var language = PropositionalLanguageBuilder.Build(new[] { "p", "q", "a", "b", "Q" });
        var formula = language.CreateFormula(input);
        var rule = new AdditionRule();

        var results = rule.Apply(new[] { formula }, language).ToList();

        Assert.Single(results);
        var result = results[0];
        Assert.Equal(expected, result.conclusion.ToString());
        Assert.Single(result.premises);
        Assert.Equal(input, result.premises[0].ToString());
    }
}
