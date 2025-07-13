using AxioMath.Core.Formulas;
using AxioMath.Logic.Propositional;
using AxioMath.Logic.Propositional.DeductionRules;
using Xunit;

namespace AxioMath.Tests;

public class GeneralTests
{

    #region 🔍 Language / Parser / Grammar

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
    public void Grammar_Should_Generate_Some_Valid_Formulas()
    {
        var lang = PropositionalLanguageBuilder.Build();
        var formulas = lang.Grammar.Generate(3).ToList();
        Assert.NotEmpty(formulas);
        Assert.All(formulas, f => Assert.True(lang.BelongsToLanguage(f)));
    }

    #endregion
}
